using System;
using TowerOfHanoi.Model;

namespace TowerOfHanoi.Logic
{
    /// <summary>
    /// Controls the flow of the game.
    /// Must be implemented for a specific game input.
    /// </summary>
    public abstract class GameFlow
    {
        protected Board board;
        /// <summary>
        /// Whether the game is automatic (from an input that holds the entire gameplay)
        /// or receives a step-by-step input that depands on the output.
        /// </summary>
        public bool IsAutomaticFlow { get; }

        public GameFlow(bool isAutomaticFlow = false)
        {
            IsAutomaticFlow = isAutomaticFlow;
        }
        /// <summary>
        /// Gets number of rods and disks from the input.
        /// </summary>
        /// <returns>Number of rods and disks.</returns>
        protected abstract InitialState GetInitialState();
        /// <summary>
        /// Initializes board with rods and disks.
        /// </summary>
        protected void Initialize()
        {
            InitialState initState = GetInitialState();
            // No initial state.
            if (initState == null)
            {
                throw new ArgumentNullException("initState");
            }
            board = new Board(initState.NumDisks, initState.NumRods);
        }
        /// <summary>
        /// Initialize existing board.
        /// </summary>
        /// <param name="board">Board.</param>
        public void Initialize(Board board)
        {
            if (board == null)
            {
                throw new ArgumentNullException("board");
            }
            this.board = board;
        }
        /// <summary>
        /// Gets player's turn from the input.
        /// </summary>
        /// <returns>Turn's parameters.</returns>
        protected abstract Turn GetNextTurn();
        /// <summary>
        /// Takes player's next turn.
        /// </summary>
        /// <returns>True if turn was valid and False otherwise.</returns>
        public bool TakeTurn()
        {
            Turn turn = GetNextTurn();
            // No new turn.
            if (turn == null)
            {
                return false;
            }
            return board.TryMoveTopDisk(turn.SrcRodIndex, turn.DstRodIndex);
        }
        /// <summary>
        /// Checks if there are any more moves.
        /// </summary>
        /// <returns>True if there are more moves and False otherwise.</returns>
        public abstract bool HasMoreTurns();
        /// <summary>
        /// Checks if the current state is a solution.
        /// </summary>
        /// <returns>True if so and False otherwise.</returns>
        public bool IsSolved() => board.IsSolved();
    }
}
