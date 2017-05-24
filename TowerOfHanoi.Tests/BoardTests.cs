using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerOfHanoi.Model;
using Moq;

namespace TowerOfHanoi.Tests
{
    [TestClass]
    public class BoardTests
    {
        private sealed class Fixture
        {
            private Mock<Rod> noDisksRodMoq;
            public Rod NoDisksRod { get { return noDisksRodMoq.Object; } }
            private Mock<Rod> oneDiskRodMoq;
            public Rod OneDiskRod { get { return oneDiskRodMoq.Object; } }
            private Mock<Rod> twoDisksRodMoq;
            public Rod TwoDisksRod { get { return twoDisksRodMoq.Object; } }
            private Mock<Rod> threeDisksRodMoq;
            public Rod ThreeDisksRod { get { return threeDisksRodMoq.Object; } }
            private Mock<Disk> diskMoqA;
            private Mock<Disk> diskMoqB;
            private Mock<Disk> diskMoqC;

            public Fixture()
            {
                diskMoqA = new Mock<Disk>(1);
                diskMoqA.Setup(s => s.CompareTo(It.IsAny<Disk>())).Returns(-1).Verifiable();

                diskMoqC = new Mock<Disk>(3);
                diskMoqC.Setup(s => s.CompareTo(It.IsAny<Disk>())).Returns(1).Verifiable();

                diskMoqB = new Mock<Disk>(2);
                diskMoqB.Setup(s => s.CompareTo(diskMoqA.Object)).Returns(1).Verifiable();
                diskMoqB.Setup(s => s.CompareTo(diskMoqC.Object)).Returns(-1).Verifiable();

                // Holds no disks.
                noDisksRodMoq = new Mock<Rod>(2);
                noDisksRodMoq.Setup(s => s.NumDisks()).Returns(0).Verifiable();
                noDisksRodMoq.Setup(s => s.RemoveTopDisk()).Returns((Disk)null).Verifiable();
                noDisksRodMoq.Setup(s => s.GetTopDisk()).Returns((Disk)null).Verifiable();
                noDisksRodMoq.Setup(s => s.TryAddTopDisk(diskMoqA.Object)).Returns(true).Verifiable();
                noDisksRodMoq.Setup(s => s.TryAddTopDisk(diskMoqB.Object)).Returns(true).Verifiable();
                noDisksRodMoq.Setup(s => s.TryAddTopDisk(diskMoqC.Object)).Returns(true).Verifiable();
                // Holds disk [Top] C.
                oneDiskRodMoq = new Mock<Rod>(2);
                oneDiskRodMoq.Setup(s => s.NumDisks()).Returns(1).Verifiable();
                oneDiskRodMoq.Setup(s => s.RemoveTopDisk()).Returns(diskMoqC.Object).Verifiable();
                oneDiskRodMoq.Setup(s => s.GetTopDisk()).Returns(diskMoqC.Object).Verifiable();
                oneDiskRodMoq.Setup(s => s.TryAddTopDisk(diskMoqB.Object)).Returns(true).Verifiable();
                oneDiskRodMoq.Setup(s => s.TryAddTopDisk(diskMoqA.Object)).Returns(true).Verifiable();
                // Holds disks B and [Top] A.
                twoDisksRodMoq = new Mock<Rod>(3);
                twoDisksRodMoq.Setup(s => s.NumDisks()).Returns(2).Verifiable();
                twoDisksRodMoq.Setup(s => s.RemoveTopDisk()).Returns(diskMoqA.Object).Verifiable();
                twoDisksRodMoq.Setup(s => s.GetTopDisk()).Returns(diskMoqA.Object).Verifiable();
                twoDisksRodMoq.Setup(s => s.TryAddTopDisk(diskMoqC.Object)).Returns(false).Verifiable();
                // Holds disks C, B and [Top] A.
                threeDisksRodMoq = new Mock<Rod>(4);
                threeDisksRodMoq.Setup(s => s.NumDisks()).Returns(3).Verifiable();
                threeDisksRodMoq.Setup(s => s.RemoveTopDisk()).Returns(diskMoqA.Object).Verifiable();
                threeDisksRodMoq.Setup(s => s.GetTopDisk()).Returns(diskMoqA.Object).Verifiable();
            }
        }

        private static Fixture fixture;
        private Board unsolvedBoard;
        private Board solvedBoard;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            fixture = new Fixture();
        }
        [TestInitialize]
        public void SetUp()
        {
            unsolvedBoard = new Board(new Rod[] { fixture.NoDisksRod, fixture.OneDiskRod, fixture.TwoDisksRod });
            solvedBoard = new Board(new Rod[] { fixture.NoDisksRod, fixture.NoDisksRod, fixture.ThreeDisksRod });
        }
        [TestMethod]
        public void Board_IsSolved_Success()
        {
            bool isSolved = solvedBoard.IsSolved();
            Assert.IsTrue(isSolved, "Board should be solved");
        }
        [TestMethod]
        public void Board_IsSolved_Fail()
        {
            bool isSolved = unsolvedBoard.IsSolved();
            Assert.IsFalse(isSolved, "Board shouldn't be solved");
        }
        [TestMethod]
        public void Board_MoveDisk_SameRod_Fail()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(2, 2);
            Assert.IsFalse(isSolved, "Move from same source and destination rod shouldn't be successful");
        }
        [TestMethod]
        public void Board_MoveDisk_SourceRodOutOfBounds1_Fail()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(4, 2);
            Assert.IsFalse(isSolved, "Move from source rod that is out of bounds shouldn't be successful");
        }
        [TestMethod]
        public void Board_MoveDisk_SourceRodOutOfBounds2_Fail()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(0, 2);
            Assert.IsFalse(isSolved, "Move from source rod that is out of bounds shouldn't be successful");
        }
        [TestMethod]
        public void Board_MoveDisk_DestRodOutOfBounds1_Fail()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(2, 4);
            Assert.IsFalse(isSolved, "Move from destination rod that is out of bounds shouldn't be successful");
        }
        [TestMethod]
        public void Board_MoveDisk_DestRodOutOfBounds2_Fail()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(2, 0);
            Assert.IsFalse(isSolved, "Move from destination rod that is out of bounds shouldn't be successful");
        }
        [TestMethod]
        public void Board_MoveDisk_SourceRodIsEmpty_Fail()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(1, 2);
            Assert.IsFalse(isSolved, "Source rod has no disks and the move should fail");
        }
        [TestMethod]
        public void Board_MoveDisk_IllegalMove_Fail()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(2, 3);
            Assert.IsFalse(isSolved, "Disk has index #3 which is greater then destination rod's top disk's index #1");
        }
        [TestMethod]
        public void Board_MoveDisk_Success()
        {
            bool isSolved = unsolvedBoard.TryMoveTopDisk(3, 2);
            Assert.IsTrue(isSolved, "Disk has index #1 which is less then destination rod's top disk's index #3");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Index must be greater than 0")]
        public void Board_Initialize_InvalidNumDisks_Exception()
        {
            Board board = new Board(0, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "Index must be greater than 0")]
        public void Board_Initialize_InvalidNumRods_Exception()
        {
            Board board = new Board(3, 1);
        }
        [TestMethod]
        public void Board_Initialize_Success()
        {
            Board board = new Board(3, 3);
            Assert.IsNotNull(board, "Board shouldn't be null");
        }
    }
}
