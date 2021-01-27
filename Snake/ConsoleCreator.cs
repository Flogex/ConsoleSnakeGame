using ConsoleSnakeGame.RealWorld;
using ConsoleSnakeGame.Rendering;
using System;

namespace ConsoleSnakeGame
{
    internal static class ConsoleCreator
    {
        public static IConsole Create()
        {
            var systemsConsole = new SystemsConsole();
            EnsureConsoleBigEnough(systemsConsole);

            return
                new ConsoleFrameDecorator(
                    new ConsoleShrinkDecorator(
                        Math.Min(20, systemsConsole.Width),
                        Math.Min(20, systemsConsole.Height),
                        systemsConsole));
        }

        private static void EnsureConsoleBigEnough(IConsole console)
        {
            // Needed for RandomPosition.GetNext

            if (console.Height < 5)
                throw new Exception("Console must at least have a height of 5.");

            if (console.Width < 5)
                throw new Exception("Console must at least have a width of 5.");
        }
    }
}
