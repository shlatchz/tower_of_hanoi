using System;
using TowerOfHanoi.View;
using TowerOfHanoi.Logic;

namespace TowerOfHanoi
{
    class Program
    {
        /// <summary>
        /// Program's entry point.
        /// </summary>
        /// <param name="args">User should pass one argument which holds the input file's path.</param>
        static void Main(string[] args)
        {
            try
            {
                // Game flow's file path should be passed as an argument.
                if (args.Length != 1)
                {
                    throw new ArgumentOutOfRangeException("args");
                }
                string filePath = args[0];
                GameFlow gameFlow = new TextFileGameFlow(filePath);
                ConsoleView gameView = new ConsoleView(gameFlow);
                gameView.Run();
            }
            catch
            {
                Console.WriteLine("No");
            }
        }
    }
}
