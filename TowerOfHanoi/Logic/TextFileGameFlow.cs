using System;
using System.IO;
using TowerOfHanoi.Model;

namespace TowerOfHanoi.Logic
{
    /// <summary>
    /// Implements a <see cref="GameController"/> for an automatic game
    /// which comes from a text file.
    /// </summary>
    public sealed class TextFileGameFlow : GameFlow
    {
        private StreamReader fileStream;
        
        public TextFileGameFlow(string filePath) :
            base(true)
        {
            fileStream = new StreamReader(filePath);
            Initialize();
        }
        protected override InitialState GetInitialState()
        {
            string input = fileStream.ReadLine();
            int numDisks = 0;
            InitialState initState = null;
            if (int.TryParse(input, out numDisks))
            {
                initState = new InitialState(numDisks);
                return initState;
            }
            else
            {
                throw new ArgumentException("Initial state isn't a valid number");
            }
        }
        protected override Turn GetNextTurn()
        {
            string input = fileStream.ReadLine();
            // Didn't reach EOF.
            if (input != null)
            {
                Turn turn = null;
                // Turns consists of 2 digits.
                if (input.Length == 2)
                { 
                    int rodsIndices = 0;
                    if (int.TryParse(input, out rodsIndices))
                    {
                        // First digit.
                        int srcRodIndex = rodsIndices / 10;
                        // Second digit.
                        int dstRodIndex = rodsIndices % 10;
                        turn = new Turn(srcRodIndex, dstRodIndex);
                    }
                    else
                    {
                        throw new ArgumentException("Line isn't a valid number");
                    }
                }
                else
                {
                    throw new ArgumentException("Line has too many characters");
                }
                return turn;
            }
            else
            {
                return null;
            }
        }
        public override bool HasMoreTurns() => !fileStream.EndOfStream;
    }
}
