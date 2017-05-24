using System.Collections.Generic;
using System;

namespace TowerOfHanoi.Model
{
    /// <summary>
    /// A rod which holds disks.
    /// </summary>
    public class Rod
    {
        /// <summary>
        /// Rod's index.
        /// </summary>
        public int Index { get; }
        private Stack<Disk> disks;

        public Rod(int index)
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException("index", $"Rod's index \"{index}\" must be greater than 0");
            }
            Index = index;
            disks = new Stack<Disk>();
        }
        public Rod(int index, Stack<Disk> disks) :
            this(index)
        {
            if (disks == null)
            {
                throw new ArgumentNullException("disks");
            }
            this.disks = disks;
        }
        /// <summary>
        /// Returns amount of disks on the rod.
        /// </summary>
        /// <returns>Number of disks.</returns>
        public virtual int NumDisks() => disks.Count;
        /// <summary>
        /// Removes the disk on top and returns it.
        /// </summary>
        /// <returns>The top disk or null if the rod doesn't hold any disks.</returns>
        public virtual Disk RemoveTopDisk() => NumDisks() == 0 ? null : disks.Pop();
        /// <summary>
        /// Returns the disk on top.
        /// </summary>
        /// <returns>The top disk or null if the rod doesn't hold any disks.</returns>
        public virtual Disk GetTopDisk() => NumDisks() == 0 ? null : disks.Peek();
        /// <summary>
        /// Try adding a new disk on top of the other disks on the rod.
        /// </summary>
        /// <param name="newDisk">New disk to add.</param>
        /// <returns>True if the disk could be added and False otherwise.</returns>
        public virtual bool TryAddTopDisk(Disk newDisk)
        {
            if (newDisk == null)
            {
                throw new ArgumentNullException("newDisk");
            }
            Disk topDisk = GetTopDisk();
            // Rod has no disks or the new disk can be on top.
            if (topDisk == null || topDisk > newDisk)
            {
                disks.Push(newDisk);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
