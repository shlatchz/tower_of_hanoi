using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfHanoi.Model
{
    /// <summary>
    /// Turn consists of the source rod and the target rod 
    /// for the disk's movement.
    /// (Used instead of named tuples that were added in C# 7.0)
    /// </summary>
    public class Turn
    {
        public int SrcRodIndex { get; }
        public int DstRodIndex { get; }
        public Turn(int srcRodIndex, int dstRodIndex)
        {
            SrcRodIndex = srcRodIndex;
            DstRodIndex = dstRodIndex;
        }
    }
}
