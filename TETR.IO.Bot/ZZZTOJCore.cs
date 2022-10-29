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
        public static extern IntPtr TetrisAI(int[] field, int field_w, int field_h, int b2b, int combo,
            char[] next, char hold, bool curCanHold, char active, int x, int y, int spin,
            bool canhold, bool can180spin, int upcomeAtt, int[] comboTable, int maxDepth, double pps, bool isEnded, int boardfill, int player);
    }
}
