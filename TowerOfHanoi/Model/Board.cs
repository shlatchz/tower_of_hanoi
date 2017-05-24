using System;

namespace TowerOfHanoi.Model
{
    /// <summary>
    /// The board which holds the rods.
    /// </summary>
    public class Board
    {
        private Rod[] rods;

        public Board(int numDisks, int numRods)
        {
            if (numDisks <= 0)
            {
                throw new ArgumentOutOfRangeException("numDisks", $"Number of disks \"{numDisks}\" must be greater than 0");
            }
            if (numRods <= 1)
            {
                throw new ArgumentOutOfRangeException("numDisks", $"Number of rods \"{numRods}\" must be greater than 1");
            }
            rods = new Rod[numRods];
            for (int i = 0; i < rods.Length; i++)
            {
                rods[i] = new Rod(i + 1);
            }
            // Add disks to first rod.
            for (int i = numDisks; i > 0; i--)
            {
                rods[0].TryAddTopDisk(new Disk(i));
            }
        }
        public Board(Rod[] rods)
        {
            if (rods == null)
            {
                throw new ArgumentNullException("rods");
            }
            if (rods.Length <= 1)
            {
                throw new ArgumentOutOfRangeException("numDisks", $"Number of rods \"{rods.Length}\" must be greater than 1");
            }
            bool hasAtLeastOneDisk = false;
            for (int i = 0; i < rods.Length; i++)
            {
                if (rods[i].NumDisks() > 0)
                {
                    hasAtLeastOneDisk = true;
                }
            }
            if (!hasAtLeastOneDisk)
            {
                throw new ArgumentOutOfRangeException("numDisks", $"Number of disks must be greater than 0");
            }
            this.rods = rods;
        }
        /// <summary>
        /// Try moving top disk from one rod to another.
        /// Rods are indexed from 1 to N, when N is the number of rods.
        /// </summary>
        /// <param name="srcRodIndex">Source rod's index.</param>
        /// <param name="dstRodIndex">Destination rod's index.</param>
        /// <returns>True if move was valid and False otherwise.</returns>
        public virtual bool TryMoveTopDisk(int srcRodIndex, int dstRodIndex)
        {
            // Source and destination rods are the same or 
            // either the source or the destination rod is out of bounds.
            if (srcRodIndex == dstRodIndex || 
                srcRodIndex < 1 || srcRodIndex > rods.Length || 
                dstRodIndex < 1 || dstRodIndex > rods.Length)
            {
                return false;
            }
            Rod srcRod = rods[srcRodIndex - 1];
            Rod dstRod = rods[dstRodIndex - 1];
            Disk srcDisk = srcRod.GetTopDisk();
            Disk dstDisk = dstRod.GetTopDisk();
            // Source rod has no top disk.
            if (srcDisk == null)
            {
                return false;
            }
            // Disk was moved successfuly.
            if (dstRod.TryAddTopDisk(srcDisk))
            {
                srcRod.RemoveTopDisk();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check if board's state represents a solution or not.
        /// </summary>
        /// <returns>True if so and False otherwise.</returns>
        public virtual bool IsSolved()
        {
            bool isEmpty = true;
            // Run on N - 1 rods and check that they're empty.
            for (int i = 0; i < (rods.Length - 1) && isEmpty; i++)
            {
                // Rod #i is empty and the top disk is null.
                isEmpty = rods[i].NumDisks() == 0;
            }
            // If all N - 1 rods are empty, then rod #N holds all the disks
            // and the game is solved.
            return isEmpty;
        }
    }
}
