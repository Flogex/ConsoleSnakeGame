namespace Snake
{
    public class Stage
    {
        public Stage()
        {
            var anyPosition = new Position(0, 0);
            this.Snake = new Snake(anyPosition);
        }

        public Snake Snake { get; }
    }
}
