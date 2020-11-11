using static Snake.Direction;

namespace Snake
{
    public class Snake
    {
        public Snake(int x, int y) : this(new Position(x, y)) { }

        public Snake(Position initialHeadPosition)
        {
            this.Head = initialHeadPosition;
        }

        public Position Head { get; private set; }

        public int Length { get; private set; } = 1;

        public void Move(Direction direction)
        {
            switch(direction)
            {
                case Left: MoveLeft(); break;
                case Right: MoveRight(); break;
                case Up: MoveUp(); break;
                case Down: MoveDown(); break;
            };
        }

        private void MoveLeft() =>
            this.Head = this.Head.WithX(this.Head.X - 1);

        private void MoveRight() =>
            this.Head = this.Head.WithX(this.Head.X + 1);

        private void MoveUp() =>
            this.Head = this.Head.WithY(this.Head.Y - 1);

        private void MoveDown() =>
            this.Head = this.Head.WithY(this.Head.Y + 1);
    }
}
