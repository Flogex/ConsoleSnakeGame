namespace Snake.Rendering
{
    public interface IConsole
    {
        /// <summary>
        /// Sets the cursor to a new position.
        /// </summary>
        /// <param name="row">The new zero-based column position of the cursor.</param>
        /// <param name="column">The new zero-based row position of the cursor.</param>
        public void SetCursorPosition(int row, int column);

        /// <summary>
        /// Writes the given char at the current cursor position and moves the cursor to the following position.
        /// </summary>
        public void Write(char value);

        /// <summary>
        /// Writes the given string to the IConsole, starting at the current cursor position.
        /// The cursor is moved to the next position right behind the printed string.
        /// </summary>
        public void Write(string value);

        /// <summary>
        /// Removes the char at the current cursor position and moves the cursor to the preceding position.
        /// </summary>
        public void RemoveCurrentChar();

        public int Width { get; }

        public int Height { get; }
    }
}
