﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScixingTetrisCore.Interface;
namespace ScixingTetrisCore.Rule
{
    public class RotationSystem : IRotationSystem
    {
        public static readonly RotationSystem SRS = new()
        {
            KickTable = new Dictionary<MinoType, (int y, int x)[][]>
            {
                [MinoType.SC_I] = new[]
                {
                    new [] { (0, 0), (-2, 0), (+1, 0), (-2, -1), (+1, +2) },
                    new [] { (0, 0), (-1, 0), (+2, 0), (-1, +2), (+2, -1) },
                    new [] { (0, 0), (+2, 0), (-1, 0), (+2, +1), (-1, -2) },
                    new [] { (0, 0), (+1, 0), (-2, 0), (+1, -2), (-2, +1) },
                },
                [MinoType.SC_O] = new[]
                {
                    new []{ (0, 0) },
                    new []{ (0, 0) },
                    new []{ (0, 0) },
                    new []{ (0, 0) },
                },
                [MinoType.SC_T] = new[]
                {
                    new []{ (0, 0), (-1, 0), (-1, +1), (0, -2), (-1, -2) },
                    new []{ (0, 0), (+1, 0), (+1, -1), (0, +2), (+1, +2) },
                    new []{ (0, 0), (+1, 0), (+1, +1), (0, -2), (+1, -2) },
                    new []{ (0, 0), (-1, 0), (-1, -1), (0, +2), (-1, +2) },
                },
                [MinoType.SC_Z] = new[]
                {
                    new []{ (0, 0), (-1, 0), (-1, +1), (0, -2), (-1, -2) },
                    new []{ (0, 0), (+1, 0), (+1, -1), (0, +2), (+1, +2) },
                    new []{ (0, 0), (+1, 0), (+1, +1), (0, -2), (+1, -2) },
                    new []{ (0, 0), (-1, 0), (-1, -1), (0, +2), (-1, +2) },
                },
                [MinoType.SC_J] = new[]
                {
                    new []{ (0, 0), (-1, 0), (-1, +1), (0, -2), (-1, -2) },
                    new []{ (0, 0), (+1, 0), (+1, -1), (0, +2), (+1, +2) },
                    new []{ (0, 0), (+1, 0), (+1, +1), (0, -2), (+1, -2) },
                    new []{ (0, 0), (-1, 0), (-1, -1), (0, +2), (-1, +2) },
                },
                [MinoType.SC_L] = new[]
                {
                    new []{ (0, 0), (-1, 0), (-1, +1), (0, -2), (-1, -2) },
                    new []{ (0, 0), (+1, 0), (+1, -1), (0, +2), (+1, +2) },
                    new []{ (0, 0), (+1, 0), (+1, +1), (0, -2), (+1, -2) },
                    new []{ (0, 0), (-1, 0), (-1, -1), (0, +2), (-1, +2) },
                },
                [MinoType.SC_S] = new[]
                {
                    new []{ (0, 0), (-1, 0), (-1, +1), (0, -2), (-1, -2) },
                    new []{ (0, 0), (+1, 0), (+1, -1), (0, +2), (+1, +2) },
                    new []{ (0, 0), (+1, 0), (+1, +1), (0, -2), (+1, -2) },
                    new []{ (0, 0), (-1, 0), (-1, -1), (0, +2), (-1, +2) },
                },
                [MinoType.SC_Z] = new[]
                {
                    new []{ (0, 0), (-1, 0), (-1, +1), (0, -2), (-1, -2) },
                    new []{ (0, 0), (+1, 0), (+1, -1), (0, +2), (+1, +2) },
                    new []{ (0, 0), (+1, 0), (+1, +1), (0, -2), (+1, -2) },
                    new []{ (0, 0), (-1, 0), (-1, -1), (0, +2), (-1, +2) },
                },

            },
             _180KickTable = new Dictionary<MinoType, (int y, int x)[][]>
            {
                {
                    MinoType.SC_I, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_J, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_L, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_O, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_S, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_T, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_Z, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                }
            }
        };
        public static readonly RotationSystem Geek = new()
        {
            //KickTable = (minoType) => minoType switch
            //{
            //    _  => new[]
            //    {
            //        new []{ (0, 0) },
            //        new []{ (0, 0) },
            //        new []{ (0, 0) },
            //        new []{ (0, 0) },
            //    },
            //},
            KickTable = new Dictionary<MinoType, (int y, int x)[][]>
            {
                {
                    MinoType.SC_I, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_J, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_L, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_O, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_S, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_T, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_Z, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                }

            },
            _180KickTable = new Dictionary<MinoType, (int y, int x)[][]>
            {
                {
                    MinoType.SC_I, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_J, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_L, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_O, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_S, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_T, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                },
                {
                    MinoType.SC_Z, new[]
                    {
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                        new []{ (0, 0) },
                    }
                }
            }
        };

        // 竟然要交换变量位置（
        public Dictionary<MinoType, (int y, int x)[][]> KickTable;
        public Dictionary<MinoType, (int y, int x)[][]> _180KickTable;

        public (bool isSuccess, int kickCnt) LeftRotation(ITetrisRuleBoard tetrisGameBoard, ITetrisMinoStatus tetrisMinoStatus)
        {
            var kickTable = KickTable[tetrisMinoStatus.TetrisMino.MinoType][(tetrisMinoStatus.Stage + 3) % 4];
            var temp = tetrisMinoStatus.Position;
            tetrisMinoStatus.LeftRoll();
            for (int i = 0; i < kickTable.Length; ++i)
            {
                tetrisMinoStatus.Position = (temp.X - kickTable[i].x, temp.Y - kickTable[i].y);
                if (tetrisGameBoard.TetrisRule.CheckMinoOk(tetrisGameBoard, tetrisMinoStatus))
                {
                    tetrisMinoStatus.LastRotation = true;
                    return (true, i);
                }
            }
            tetrisMinoStatus.RightRoll();
            tetrisMinoStatus.Position = temp;
            return (false, -1);
        }

        public (bool isSuccess, int kickCnt) RightRotation(ITetrisRuleBoard tetrisGameBoard, ITetrisMinoStatus tetrisMinoStatus)
        {
            var kickTable = KickTable[tetrisMinoStatus.TetrisMino.MinoType][tetrisMinoStatus.Stage];
            var temp = tetrisMinoStatus.Position;
            tetrisMinoStatus.RightRoll();
            for (int i = 0; i < kickTable.Length; ++i)
            {
                tetrisMinoStatus.Position = (temp.X + kickTable[i].x, temp.Y + kickTable[i].y);
                if (tetrisGameBoard.TetrisRule.CheckMinoOk(tetrisGameBoard, tetrisMinoStatus))
                {
                    tetrisMinoStatus.LastRotation = true;
                    return (true, i);
                }
            }
            tetrisMinoStatus.LeftRoll();
            tetrisMinoStatus.Position = temp;
            return (false, -1);
        }

        public (bool isSuccess, int kickCnt) _180Rotation(ITetrisRuleBoard tetrisGameBoard, ITetrisMinoStatus tetrisMinoStatus)
        {
            var kickTable = _180KickTable[tetrisMinoStatus.TetrisMino.MinoType][tetrisMinoStatus.Stage];
            var temp = tetrisMinoStatus.Position;
            tetrisMinoStatus._180Roll();
            for (int i = 0; i < kickTable.Length; ++i)
            {
                tetrisMinoStatus.Position = (temp.X + kickTable[i].x, temp.Y + kickTable[i].y);
                if (tetrisGameBoard.TetrisRule.CheckMinoOk(tetrisGameBoard, tetrisMinoStatus))
                {
                    tetrisMinoStatus.LastRotation = true;
                    return (true, i);
                }
            }
            tetrisMinoStatus._180Roll();
            tetrisMinoStatus.Position = temp;
            return (false, -1);
        }
    }
}

