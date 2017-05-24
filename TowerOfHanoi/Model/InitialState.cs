using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfHanoi.Model
{
    /// <summary>
    /// Initial state consists of number of disks and rods.
    /// (Used instead of named tuples that were added in C# 7.0)
    /// </summary>
    public class InitialState
    {
        private const int DEFAULT_NUM_RODS = 3;

        public int NumRods { get; }
        public int NumDisks { get; }
        public InitialState(int numDisks, int numRods = DEFAULT_NUM_RODS)
        {
            NumRods = numRods;
            NumDisks = numDisks;
        }
    }
}
