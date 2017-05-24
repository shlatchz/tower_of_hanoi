using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerOfHanoi.Model;
using TowerOfHanoi.Logic;
using TowerOfHanoi.Tests.Logic;
using Moq;

namespace TowerOfHanoi.Tests
{
    [TestClass]
    public class GameFlowTests
    {
        private sealed class Fixture
        {
            private Mock<Board> unsolvedBoardMoq;
            public Board UnsolvedBoard { get { return unsolvedBoardMoq.Object; } }
            private Mock<Board> solvedBoardMoq;
            public Board SolvedBoard { get { return solvedBoardMoq.Object; } }

            public Fixture()
            {
                unsolvedBoardMoq = new Mock<Board>(3, 3);
                unsolvedBoardMoq.Setup(s => s.IsSolved()).Returns(false).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(1, 1)).Returns(false).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(1, 2)).Returns(false).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(1, 3)).Returns(false).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(2, 1)).Returns(true).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(2, 2)).Returns(false).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(2, 3)).Returns(true).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(3, 1)).Returns(true).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(3, 2)).Returns(false).Verifiable();
                unsolvedBoardMoq.Setup(s => s.TryMoveTopDisk(3, 3)).Returns(false).Verifiable();

                solvedBoardMoq = new Mock<Board>(3, 3);
                solvedBoardMoq.Setup(s => s.IsSolved()).Returns(true).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(1, 1)).Returns(false).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(1, 2)).Returns(false).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(1, 3)).Returns(false).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(2, 1)).Returns(false).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(2, 2)).Returns(false).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(2, 3)).Returns(false).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(3, 1)).Returns(true).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(3, 2)).Returns(true).Verifiable();
                solvedBoardMoq.Setup(s => s.TryMoveTopDisk(3, 3)).Returns(false).Verifiable();
            }
        }

        private static Fixture fixture;
        private GameFlow unsolvedFlow;
        private GameFlow solvedFlow;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            fixture = new Fixture();
        }
        [TestInitialize]
        public void SetUp()
        {
            unsolvedFlow = new TestGameFlow(new InitialState(3, 3), new Turn(2, 3), true, true);
            unsolvedFlow.Initialize(fixture.UnsolvedBoard);

            solvedFlow = new TestGameFlow(new InitialState(3, 3), null, false, true);
            solvedFlow.Initialize(fixture.SolvedBoard);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Initial state cannot be null")]
        public void GameFlow_Initialize_Exception()
        {
            GameFlow flow = new TestGameFlow(null, new Turn(2, 2), false, true);
        }
        [TestMethod]
        public void GameFlow_Initialize_Success()
        {
            GameFlow flow = new TestGameFlow(new InitialState(3, 3), new Turn(2, 2), false, true);
        }
        [TestMethod]
        public void GameFlow_TakeTurn_NoTurn_Fail()
        {
            GameFlow flow = new TestGameFlow(new InitialState(3, 3), null, false, true);
            bool isValidTurn = flow.TakeTurn();
            Assert.IsFalse(isValidTurn, "Turn shouldn't be carried out because it was null");
        }
        [TestMethod]
        public void GameFlow_TakeTurn_TurnInvalid_Fail()
        {
            GameFlow flow = new TestGameFlow(new InitialState(3, 3), new Turn(1, 1), false, true);
            bool isValidTurn = flow.TakeTurn();
            Assert.IsFalse(isValidTurn, "Turn shouldn't be carried out because it was invalid");
        }
        [TestMethod]
        public void GameFlow_TakeTurn_Success()
        {
            bool isValidTurn = unsolvedFlow.TakeTurn();
            Assert.IsTrue(isValidTurn, "Turn moves disk #1 on top of disk #2 and should be valid");
        }
        [TestMethod]
        public void GameFlow_HasMoreTurns_No_Success()
        {
            bool hasTurns = solvedFlow.HasMoreTurns();
            Assert.IsFalse(hasTurns, "Board shouldn't have more turns left");
        }
        [TestMethod]
        public void GameFlow_HasMoreTurns_Yes_Success()
        {
            bool hasTurns = unsolvedFlow.HasMoreTurns();
            Assert.IsTrue(hasTurns, "Board should have more turns left");
        }
        [TestMethod]
        public void GameFlow_IsSolved_No_Success()
        {
            bool isSolved = solvedFlow.IsSolved();
            Assert.IsTrue(isSolved, "Board should be solved");
        }
        [TestMethod]
        public void GameFlow_IsSolved_Yes_Success()
        {
            bool isSolved = unsolvedFlow.IsSolved();
            Assert.IsFalse(isSolved, "Board shouldn't be solved");
        }
    }
}

