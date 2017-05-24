using System;
using TowerOfHanoi.Logic;
using TowerOfHanoi.Model;

namespace TowerOfHanoi.Tests.Logic
{
    /// <summary>
    /// Implements a <see cref="GameController"/> for testing.
    /// </summary>
    public class TestGameFlow : GameFlow
    {
        private InitialState initState;
        private Turn turn;
        private bool hasMoreTurns;

        public TestGameFlow(InitialState initState, Turn turn, bool hasMoreTurns, bool isAutomaticFlow) :
            base(isAutomaticFlow)
        {
            if (initState == null)
            {
                throw new ArgumentNullException("initState");
            }
            this.initState = initState;
            this.turn = turn;
            this.hasMoreTurns = hasMoreTurns;
            Initialize();
        }
        protected override InitialState GetInitialState()
        {
            return initState;
        }
        protected override Turn GetNextTurn()
        {
            return turn;
        }
        public override bool HasMoreTurns() => hasMoreTurns;
    }
}
