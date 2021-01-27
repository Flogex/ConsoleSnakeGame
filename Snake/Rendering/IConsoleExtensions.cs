namespace ConsoleSnakeGame.Rendering
{
    public static class IConsoleExtensions
    {
        /// <summary>
        /// Writes the given char to the <see cref="IConsole"/>at the given row and column.
        /// </summary>
        public static void WriteAt(this IConsole console, char value, int row, int column) =>
            console.WriteAt(value, new CursorPosition(row, column));

        /// <summary>
        /// Writes the given string to the <see cref="IConsole"/>, starting at the given row and column.
        /// </summary>
        public static void WriteAt(this IConsole console, string value, int row, int column) =>
            console.WriteAt(value, new CursorPosition(row, column));

        /// <summary>
        /// Removes the character at the given row and column from the <see cref="IConsole"/>.
        /// </summary>
        public static void RemoveCharAt(this IConsole console, int row, int column) =>
            console.RemoveCharAt(new CursorPosition(row, column));
    }
}
