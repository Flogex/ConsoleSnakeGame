using System;
using Snake.Rendering;

namespace Snake.RealWorld
{
    public class SystemsConsole : IConsole
    {
        public SystemsConsole()
        {
            Console.CursorVisible = false;
            Console.Title = "Snaky";
        }

        public int Width => Console.WindowWidth;

        public int Height => Console.WindowHeight;

        public void WriteAt(char value, CursorPosition position)
        {
            Console.SetCursorPosition(position.Column, position.Row);
            Console.Write(value);
        }

        public void WriteAt(string value, CursorPosition position)
        {
            Console.SetCursorPosition(position.Column, position.Row);
            Console.Write(value);
        }

        public void RemoveCharAt(CursorPosition position)
        {
            Console.SetCursorPosition(position.Column, position.Row);
            Console.Write(" \b");
        }
    }
}
