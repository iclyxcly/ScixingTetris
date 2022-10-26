﻿using ScixingTetrisCore.Interface;
using ScixingTetrisCore.Rule;
using ScixingTetrisCore.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScixingTetrisCore
{
    // 生成位置确定一下
    public class TetrisGameBoard : ITetrisGameBoard
    {
        public byte[][] Field { get; private set; }
        public int Height { get => Field.Length; }
        public int Width { get => Field[0].Length; }
        public int ShowHeight { get; set; }
        public int[] ColHeight { get; set; }
        public ITetrisRule TetrisRule { get; private set; }
        public static int count;
        public static bool IsMinoHeightIncreased;
        //public Queue<ITetrisMino> NextQueue => throw new NotImplementedException();
        public Queue<ITetrisMino> NextQueue { get; } = new();

        public bool IsDead => throw new NotImplementedException();

        // 对于单方块场地
        public ITetrisMinoStatus TetrisMinoStatus;

        // hold
        public ITetrisMino HoldMino;
        /// <summary>
        /// 生成器 要在这里吗（
        /// </summary>
        public ITetrisMinoGenerator TetrisMinoGenerator;

        //public IFieldCheck FieldCheck => throw new NotImplementedException();


        // 此处还欠考虑
        public int B2B { get; set; }
        public int Combo { get; set; }

        public (int X, int Y) DefaultPos = (20, 3); // known issue
        // 此处还欠考虑
        public TetrisGameBoard(int Width = 10, int Height = 40, int ShowHeight = 20, ITetrisRule tetrisRule = null, ITetrisMinoGenerator tetrisMinoGenerator = null)
        {
            // 赋予规则
            TetrisRule = tetrisRule ?? GuildLineRule.Rule;
            TetrisMinoGenerator = tetrisMinoGenerator ?? new Bag7Generator<TetrisMino>();
            Field = new byte[Height][];
            for (int i = 0; i < Height; ++i)
            {
                Field[i] = new byte[Width];
            }
            // ? 也许不用现在取
            this.ShowHeight = Math.Min(ShowHeight, Height);
            ColHeight = new int[Width];
            // 别spawn
            //SpawnNewPiece();
        }

        // 加入接口
        public void GameStart()
        {
            SpawnNewPiece();
        }
        public int TryClearLines()
        {
            int cnt = 0;
            count = 0;
            // 限制一下搜索高度
            //List<int> clearidx = new List<int>();
            bool[] clearFlag = new bool[Height];
            /*if (Field[20][4] != 0) IsMinoHeightIncreased = true; // might be the resolve of known issue but untested
            else IsMinoHeightIncreased = false;*/
            for (int i = 0; i < Height; ++i)
            {
                bool flag = true;
                for (int j = 0; j < Width; ++j)
                {
                    count += Field[i][j];
                    if (Field[i][j] == 0)
                    {
                        flag = false;
                    }
                }
                if (flag) { cnt++; clearFlag[i] = true; }
            }
            
            if (cnt > 0) Combo++;
            else Combo = 0;
            bool isTspin = false;
            // 可能要改一下
            if (cnt > 0 && TetrisMinoStatus.LastRotation && TetrisMinoStatus.TetrisMino.MinoType == MinoType.SC_T)
            {
                int spinCnt = 0;
                spinCnt += TetrisRule.CheckPostionOk(this, TetrisMinoStatus.Position.X, TetrisMinoStatus.Position.Y) ? 0 : 1;
                spinCnt += TetrisRule.CheckPostionOk(this, TetrisMinoStatus.Position.X + 2, TetrisMinoStatus.Position.Y) ? 0 : 1;
                spinCnt += TetrisRule.CheckPostionOk(this, TetrisMinoStatus.Position.X, TetrisMinoStatus.Position.Y + 2) ? 0 : 1;
                spinCnt += TetrisRule.CheckPostionOk(this, TetrisMinoStatus.Position.X + 2, TetrisMinoStatus.Position.Y + 2) ? 0 : 1;
                if (spinCnt >= 3) isTspin = true;
                //if (spinCnt >= 3) Console.WriteLine("Tspin");
            }
            if (cnt == 4 || isTspin) B2B++;
            else if (cnt != 0) B2B = 0;
            for (int i = 0, j = 0; i < Height; ++i, ++j)
            {
                while (j < Height && clearFlag[j])
                {
                    ++j;
                }
                if (j >= Height)
                {
                    Field[i] = new byte[Width];
                }
                else
                {
                    Field[i] = Field[j];
                }

            }
            return cnt;
        }

        /// <summary>
        /// 输出场地
        /// </summary>
        /// <param name="printLeft"></param>
        /// <param name="printTop"></param>
        public void PrintBoard(bool WithMino = false, int printLeft = 0, int printTop = 0)
        {
            int tempTop = printTop;
            Console.SetCursorPosition(printLeft, printTop); ;
            for (int i = 0; i < Width + 1; ++i) Console.Write("--");
            Console.Write('\n');
            for (int i = 0; i < ShowHeight; ++i)
            {
                Console.SetCursorPosition(printLeft, ++printTop);
                int pi = ShowHeight - 1 - i;
                Console.Write('|');
                for (int j = 0; j < Width; ++j)
                {
                    if (Field[pi][j] != 0)
                    {

                        Console.Write("[]");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.Write('|');
                Console.Write('\n');
            }
            Console.SetCursorPosition(printLeft, ++printTop);
            for (int i = 0; i < Width + 1; ++i) Console.Write("--");
            Console.Write('\n');

            if (WithMino && TetrisMinoStatus != null)
            {
                foreach (var pos in TetrisMinoStatus?.GetMinoFieldListInBoard())
                {
                    // pos 要在显示区域内
                    // 肯定有问题.jpg
                    Console.SetCursorPosition(printLeft + 1 + pos.Y * 2, tempTop + (ShowHeight - pos.X));
                    Console.Write("[]");
                }

            }
        }

        public bool LockMino()
        {
            //if(TetrisRule)
            var minoList = TetrisMinoStatus.GetMinoFieldListInBoard();
            // 要不不检查了（？
            // 断言此时的场地和方块是ok的
            foreach (var pos in minoList)
            {
                Field[pos.X][pos.Y] = 1;
            }
            TryClearLines();
            SpawnNewPiece();
            return true;
        }

        public bool IsCellFree(int x, int y)
        {
            if (x >= 0 && x < Height && y >= 0 && y < Width)
            {
                return Field[x][y] == 0;
            }
            return false;
        }

        public bool LeftRotation()
        {
            return TetrisRule.RotationSystem.LeftRotation(this, TetrisMinoStatus).isSuccess;
        }

        public bool RightRotation()
        {
            return TetrisRule.RotationSystem.RightRotation(this, TetrisMinoStatus).isSuccess;
        }

        public bool _180Rotation()
        {
            return TetrisRule.RotationSystem._180Rotation(this, TetrisMinoStatus).isSuccess;
        }

        public bool MoveLeft()
        {
            TetrisMinoStatus.MoveLeft();
            if (TetrisRule.CheckMinoOk(this, TetrisMinoStatus))
            {
                TetrisMinoStatus.LastRotation = false;
                return true;
            }
            TetrisMinoStatus.MoveRight();
            return false;
        }

        public bool MoveRight()
        {
            TetrisMinoStatus.MoveRight();
            if (TetrisRule.CheckMinoOk(this, TetrisMinoStatus))
            {
                TetrisMinoStatus.LastRotation = false;
                return true;
            }
            TetrisMinoStatus.MoveLeft();
            return false;
        }

        public bool SoftDrop()
        {
            TetrisMinoStatus.MoveBottom();
            if (TetrisRule.CheckMinoOk(this, TetrisMinoStatus))
            {
                TetrisMinoStatus.LastRotation = false;
                return true;
            }
            TetrisMinoStatus.MoveTop();
            return false;
        }

        public bool SonicDrop()
        {
            while (SoftDrop()) ;
            return true;
        }
        /// <summary>
        /// 硬降 需要确认方块位置没问题吗
        /// </summary>
        /// <returns></returns>
        public bool HardDrop()
        {
            SonicDrop();
            LockMino();
            return true;
        }

        public bool OnHold()
        {
            // if 允许hold

            if (HoldMino == null)
            {
                HoldMino = TetrisMinoStatus.TetrisMino;
                SpawnNewPiece();
            }
            else
            {
                (HoldMino, TetrisMinoStatus.TetrisMino) = (TetrisMinoStatus.TetrisMino, HoldMino);
                //TetrisMinoStatus.Position = (19, 3);
                TetrisMinoStatus.Position = DefaultPos;
                TetrisMinoStatus.Stage = 0;
            }
            return true;
        }

        public bool SpawnNewPiece()
        {
            // 先简略来一个（ 后续要改 要考虑方块用什么 需不需要接口 要看看成不成功
            //TetrisMinoStatus = new TetrisMinoStatus { Position = (19, 3), Stage = 0, TetrisMino = TetrisMinoGenerator.GetNextMino() };
            //TetrisMinoStatus = new TetrisMinoStatus { Position = (19, 3), Stage = 0, TetrisMino = NextQueue.Dequeue() };
            TetrisMinoStatus = new TetrisMinoStatus { Position = DefaultPos, Stage = 0, TetrisMino = NextQueue.Dequeue() };

            // 针对io 立即下降一格
            SoftDrop();
            return true;
        }
    }
}
