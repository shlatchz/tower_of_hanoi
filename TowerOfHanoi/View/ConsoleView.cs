using System;
using TowerOfHanoi.Logic;
using TowerOfHanoi.Model;

namespace TowerOfHanoi.View
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConsoleView
    {
        private GameFlow gameFlow;

        public ConsoleView(GameFlow gameFlow)
        {
            this.gameFlow = gameFlow;
        }
        /// <summary>
        /// Run entire game flow.
        /// </summary>
        public void Run()
        {
            GameState gameState = GameState.Unknown;
            try
            {
                bool isValidMove = true;
                // Run until user does an invalid move or 
                // until there is no more input for the game.
                bool canRun = true;
                // Run as long as everything is valid and the input hasn't ended.
                while (canRun)
                {
                    // Player hasn't stopped the game.
                    if (gameFlow.HasMoreTurns())
                    {
                        bool isValidTurn = gameFlow.TakeTurn();
                        // Player's turn was valid.
                        if (isValidTurn)
                        {
                            ShowBoardState();
                        }
                        // User did an invalid move.
                        // (When the game's input isn't automatic, we can ask the user to try again instead
                        //  of failing him)
                        else if (!gameFlow.IsAutomaticFlow)
                        {
                            ShowWarning(GameState.IllegalMove);
                        }
                        else
                        {
                            gameState = GameState.IllegalMove;
                            // Game cannot continue after an illegal move was made.
                            canRun = false;
                        }
                    }
                    else
                    {
                        // Player has no more turns.
                        canRun = false;
                    }
                }
                // Game's state is still unknown.
                if (gameState == GameState.Unknown)
                {
                    // Update to solved or unsolved.
                    gameState = gameFlow.IsSolved() ? GameState.Solved : GameState.Unsolved;
                }
            }
            catch
            {
                // Game has crashed.
                gameState = GameState.Exception;
            }
            finally
            {
                // Always print game's result.
                ShowGameResult(gameState);
            }
        }
        /// <summary>
        /// Show game's result to screen.
        /// </summary>
        /// <param name="gameState">Game's current state</param>
        private void ShowGameResult(GameState gameState)
        {
            if (gameState == GameState.Solved)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
        /// <summary>
        /// Shows board's state.
        /// </summary>
        private void ShowBoardState()
        {
            // TODO: Show game's state.
        }
        /// <summary>
        /// Show a warning to the user's screen.
        /// </summary>
        /// <param name="gameState">Game's current state</param>
        private void ShowWarning(GameState gameState)
        {
            // TODO: Print a warning concerning the player's last move.
        }
    }
}
