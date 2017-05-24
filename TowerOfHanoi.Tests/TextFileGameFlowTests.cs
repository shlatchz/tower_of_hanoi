using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TowerOfHanoi.Model;
using TowerOfHanoi.Logic;
using TowerOfHanoi.Tests.Logic;
using Moq;

namespace TowerOfHanoi.Tests
{
    [TestClass]
    public sealed class TextFileGameFlowTests
    {
        private static class Fixture
        {
            public const string INVALID_INITIAL_STATE_FILE_PATH = @"Logic\GameFlow_InvalidInitialState_NaN.txt";
            public const string INVALID_TURN_NAN_FILE_PATH = @"Logic\GameFlow_InvalidTurn_NaN.txt";
            public const string INVALID_TURN_NOT_2_CHARS_FILE_PATH = @"Logic\GameFlow_InvalidTurn_Not2Chars.txt";
            public const string VALID_INITIAL_STATE_AND_TURN_FILE_PATH = @"Logic\GameFlow_ValidInitialStateAndTurn.txt";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Initial state must be a number")]
        public void TextFileGameFlow_GetInitialState_Exception()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.INVALID_INITIAL_STATE_FILE_PATH);
        }
        [TestMethod]
        public void TextFileGameFlow_GetInitialState_Success()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.VALID_INITIAL_STATE_AND_TURN_FILE_PATH);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Turn must be a number")]
        public void TextFileGameFlow_GetNextTurn_NotANumber_Exception()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.INVALID_TURN_NAN_FILE_PATH);
            flow.TakeTurn();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Turn can only have 2 characters")]
        public void TextFileGameFlow_GetNextTurn_Not2Chars_Exception()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.INVALID_TURN_NOT_2_CHARS_FILE_PATH);
            flow.TakeTurn();
        }
        [TestMethod]
        public void TextFileGameFlow_GetNextTurn_ReachedEOF_Success()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.VALID_INITIAL_STATE_AND_TURN_FILE_PATH);
            flow.TakeTurn();
            bool isValidTurn = flow.TakeTurn();
            Assert.IsFalse(isValidTurn, "No turns left thus the turn is invalid");
        }
        [TestMethod]
        public void TextFileGameFlow_GetNextTurn_ValidTurn_Success()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.VALID_INITIAL_STATE_AND_TURN_FILE_PATH);
            bool isValidTurn = flow.TakeTurn();
            Assert.IsTrue(isValidTurn, "Turn should be valid");
        }
        [TestMethod]
        public void TextFileGameFlow_HasMoreTurns_Yes_Success()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.VALID_INITIAL_STATE_AND_TURN_FILE_PATH);
            bool hasMoreTurns = flow.HasMoreTurns();
            Assert.IsTrue(hasMoreTurns, "One turn is still remaining in the text file");
        }
        [TestMethod]
        public void TextFileGameFlow_HasMoreTurns_No_Success()
        {
            GameFlow flow = new TextFileGameFlow(Fixture.VALID_INITIAL_STATE_AND_TURN_FILE_PATH);
            flow.TakeTurn();
            bool hasMoreTurns = flow.HasMoreTurns();
            Assert.IsFalse(hasMoreTurns, "All turns were taken");
        }
    }
}
