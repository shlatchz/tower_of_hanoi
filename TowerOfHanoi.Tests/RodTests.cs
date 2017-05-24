using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerOfHanoi.Model;
using System.Collections.Generic;
using Moq;

namespace TowerOfHanoi.Tests
{
    [TestClass]
    public sealed class RodTests
    {
        private sealed class Fixture
        {
            private Mock<Disk> diskMoqA;
            public Disk DiskA { get { return diskMoqA.Object; } }
            private Mock<Disk> diskMoqB;
            public Disk DiskB { get { return diskMoqB.Object; } }
            private Mock<Disk> diskMoqC;
            public Disk DiskC { get { return diskMoqC.Object; } }

            public Fixture()
            {
                diskMoqA = new Mock<Disk>(1);
                diskMoqA.Setup(s => s.CompareTo(It.IsAny<Disk>())).Returns(-1).Verifiable();

                diskMoqC = new Mock<Disk>(3);
                diskMoqC.Setup(s => s.CompareTo(It.IsAny<Disk>())).Returns(1).Verifiable();

                diskMoqB = new Mock<Disk>(2);
                diskMoqB.Setup(s => s.CompareTo(DiskA)).Returns(1).Verifiable();
                diskMoqB.Setup(s => s.CompareTo(DiskC)).Returns(-1).Verifiable();
            }
        }

        private static Fixture fixture;
        private Rod oneDiskRod;
        private Rod twoDisksRod;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            fixture = new Fixture();
        }
        [TestInitialize]
        public void SetUp()
        {
            oneDiskRod = new Rod(1, new Stack<Disk>(new Disk[] { fixture.DiskC }));
            twoDisksRod = new Rod(2, new Stack<Disk>(new Disk[] { fixture.DiskC, fixture.DiskA }));
        }
        [TestMethod]
        public void Rod_AddDisk_Empty_Success()
        {
            Rod emptyRod = new Rod(3);
            bool isAdded = emptyRod.TryAddTopDisk(fixture.DiskB);
            Assert.IsTrue(isAdded, "Disk wasn't added successfuly");
        }
        [TestMethod]
        public void Rod_AddDisk_NonEmpty_Success()
        {
            bool isAdded = oneDiskRod.TryAddTopDisk(fixture.DiskB);
            Assert.IsTrue(isAdded, "Disk wasn't added successfuly");
        }
        [TestMethod]
        public void Rod_AddDisk_NonEmpty_Fail()
        {
            bool isAdded = twoDisksRod.TryAddTopDisk(fixture.DiskB);
            Assert.IsFalse(isAdded, "Disk was added even though its index is higher than top's index");
        }
        [TestMethod]
        public void Rod_RemoveDisk_NoDisks_Success()
        {
            Rod emptyRod = new Rod(3);
            Disk actualDisk = emptyRod.RemoveTopDisk();
            Assert.IsNull(actualDisk, "Disk isn't null");
        }
        [TestMethod]
        public void Rod_RemoveDisk_OneDisk_Success()
        {
            Disk actualDisk = oneDiskRod.RemoveTopDisk();
            Assert.AreEqual(fixture.DiskC, actualDisk, "Wrong disk was removed");
        }
        [TestMethod]
        public void Rod_RemoveDisk_TwoDisks_Success()
        {
            Disk actualDisk = twoDisksRod.RemoveTopDisk();
            Assert.AreEqual(fixture.DiskA, actualDisk, "Wrong disk was removed");
        }
        [TestMethod]
        public void Rod_GetNumDisks_Success()
        {
            int numDisks = twoDisksRod.NumDisks();
            int expected = 2;
            Assert.AreEqual(expected, numDisks, "Number of disks should be 2");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Index must be greater than 0")]
        public void Rod_Initialize_InvalidIndex_Exception()
        {
            Rod rod = new Rod(0);
        }
        [TestMethod]
        public void Rod_Initialize_Success()
        {
            Rod rod = new Rod(1);
            Assert.IsNotNull(rod, "Rod shouldn't be null");
        }
    }
}
