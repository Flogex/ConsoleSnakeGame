namespace Snake.Rendering
{
    public interface IConsole
    {
        /// <summary>
        /// Writes the given char at the given cursor position.
        /// </summary>
        /// <param name="value">The character to write to the console.</param>
        /// <param name="position">The position at which to write the character.</param>
        public void WriteAt(char value, CursorPosition position);

        /// <summary>
        /// Writes the given string to the <see cref="IConsole"/>, starting at the given <see cref="CursorPosition"/>.
        /// </summary>
        /// <param name="value">The value to write to the console.</param>
        /// <param name="position">The position at which to start writing the string.</param>
        public void WriteAt(string value, CursorPosition position);

        /// <summary>
        /// Removes the character at the given <see cref="CursorPosition"/>
        /// </summary>
        public void RemoveCharAt(CursorPosition position);

        public int Width { get; }

        public int Height { get; }
    }
}
