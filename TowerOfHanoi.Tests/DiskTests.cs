using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerOfHanoi.Model;

namespace TowerOfHanoi.Tests
{
    [TestClass]
    public class DiskTests
    {
        [TestMethod]
        public void Disk_CompareLessThan_Success()
        {
            Disk diskA = new Disk(1);
            Disk diskB = new Disk(2);
            int expected = -1;
            int actual = diskA.CompareTo(diskB);
            Assert.AreEqual(expected, actual, "Compare between disk with lower index and disk with higher index should return -1 [< 0]");
        }
        [TestMethod]
        public void Disk_CompareGreaterThan_Success()
        {
            Disk diskA = new Disk(1);
            Disk diskB = new Disk(2);
            int expected = 1;
            int actual = diskB.CompareTo(diskA);
            Assert.AreEqual(expected, actual, "Compare between disk with higher index and disk with lower index should return 1 [> 0]");
        }
        [TestMethod]
        public void Disk_CompareEqual_Success()
        {
            Disk diskA = new Disk(1);
            Disk diskB = new Disk(1);
            int expected = 0;
            int actual = diskA.CompareTo(diskB);
            Assert.AreEqual(expected, actual, "Compare between disk with higher index and disk with lower index should return 0 [== 0]");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Cannot compare with a null object")]
        public void Disk_CompareLessThanWithNull_Exception()
        {
            Disk diskA = new Disk(1);
            Disk diskB = null;
            bool comp = diskA < diskB;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Cannot compare with a null object")]
        public void Disk_CompareGreaterThanWithNull_Exception()
        {
            Disk diskA = new Disk(1);
            Disk diskB = null;
            bool comp = diskA > diskB;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Index must be greater than 0")]
        public void Disk_Initialize_InvalidIndex_Exception()
        {
            Disk disk = new Disk(0);
        }
        [TestMethod]
        public void Disk_Initialize_Success()
        {
            Disk disk = new Disk(1);
            Assert.IsNotNull(disk, "Disk shouldn't be null");
        }
    }
}
