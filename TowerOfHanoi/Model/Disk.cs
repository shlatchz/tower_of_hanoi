using System;

namespace TowerOfHanoi.Model
{
    /// <summary>
    /// A disk.
    /// </summary>
    public class Disk : IComparable<Disk>
    {
        /// <summary>
        /// Disk's index.
        /// </summary>
        public int Index { get; }

        public Disk(int index)
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException("index", $"Disk's index \"{index}\" must be greater than 0");
            }
            Index = index;
        }
        /// <summary>
        /// Compares one disk to another by comparing their indices.
        /// </summary>
        /// <param name="otherDisk">Other disk.</param>
        /// <returns>
        /// Less than zero: This disk's index is less than other disk's index.
        /// Zero: This disk's index is equal to other disk's index.
        /// Greater than zero: This disk's index is greater than other disk's index.
        /// </returns>
        public virtual int CompareTo(Disk otherDisk)
        {
            if (otherDisk == null)
            {
                throw new ArgumentNullException("otherDisk");
            }
            return Index.CompareTo(otherDisk.Index);
        }
        /// <summary>
        /// Overload less than operator.
        /// </summary>
        /// <param name="disk1">Left disk.</param>
        /// <param name="disk2">Right disk.</param>
        /// <returns>True if left disk's index is less than right disk's index.</returns>
        public static bool operator <(Disk disk1, Disk disk2)
        {
            if (disk1 == null)
            {
                throw new ArgumentNullException("disk1");
            }
            if (disk2 == null)
            {
                throw new ArgumentNullException("disk2");
            }
            return disk1.CompareTo(disk2) < 0;
        }
        /// <summary>
        /// Overload greater than operator.
        /// </summary>
        /// <param name="disk1">Left disk.</param>
        /// <param name="disk2">Right disk.</param>
        /// <returns>True if left disk's index is greater than right disk's index.</returns>
        public static bool operator >(Disk disk1, Disk disk2)
        {
            if (disk1 == null)
            {
                throw new ArgumentNullException("disk1");
            }
            if (disk2 == null)
            {
                throw new ArgumentNullException("disk2");
            }
            return disk1.CompareTo(disk2) > 0;
        }
    }
}
