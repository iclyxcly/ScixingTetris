using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScixingTetrisCore.Interface
{
    public interface IGarbageGenerator
    {
        List<int> GetBitGarbage(List<int> GarbageList);
        List<byte[]> GetGarbage(List<int> GarbageList);
    }
}
