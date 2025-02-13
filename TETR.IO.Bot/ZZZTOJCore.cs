﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TETR.IO.Bot
{
    public static class ZZZTOJCore
    {
        [DllImport("zzztoj_io_dll.dll")]
        public static extern IntPtr TetrisAI(int[] field, int b2b, int combo,
            char[] next, char hold, bool curCanHold, char active, int x, int y, int spin,
            bool canhold, bool can180spin, int upcomeAtt, int maxDepth, double pps, bool burst, bool pc, bool isEnded, int player);
    }
}
