using System;
using System.Linq;

namespace Unimage
{
    public abstract class TextShape
    {
        public Vector Position { get; set; }

        public Vector Size { get; }

        protected TextShape(Vector position, Vector size)
        {
            Position = position;

            Size = size;
        }

        protected abstract void Illustrate(char[,] illustration);

        internal char[,] Illustrate()
        {
            var illustration = new char[Size.X, Size.Y];
            Illustrate(illustration);

            return illustration;
        }
    }

    public class TextDot : TextShape
    {
        public char ChBdy { get; set; }

        public TextDot(Vector position)
            : base (position, new Vector(1, 1)) { }

        protected override void Illustrate(char[,] illustration)
        {
            illustration[0, 0] = ChBdy;
        }
    }

    public class TextLineH : TextShape
    {
        public char ChLft { get; set; }
        public char ChRgt { get; set; }

        public char ChHrz { get; set; }

        public TextLineH(Vector position, int length)
            : base(
                position + (length < 0 ? new Vector(length + 1, 0) : Vector.Zero),
                new Vector(Math.Abs(length), 1)) { }

        protected override void Illustrate(char[,] illustration)
        {
            for (var x = 1; x < Size.X - 1; x++)
                illustration[x, 0] = ChHrz;

            illustration[Size.X - 1, 0] = ChRgt;
            illustration[         0, 0] = ChLft;
        }
    }
    public class TextLineV : TextShape
    {
        public char ChTop { get; set; }
        public char ChBot { get; set; }

        public char ChVrt { get; set; }

        public TextLineV(Vector position, int length)
            : base(
                position + (length < 0 ? new Vector(0, length + 1) : Vector.Zero),
                new Vector(1, Math.Abs(length))) { }

        protected override void Illustrate(char[,] illustration)
        {
            for (var y = 1; y < Size.Y - 1; y++)
                illustration[0, y] = ChVrt;

            illustration[0, Size.Y - 1] = ChBot;
            illustration[0,          0] = ChTop;
        }
    }

    public class TextRectangle : TextShape
    {
        private const int MARGIN_SIZE = 1;

        private readonly string[] _content;

        public char ChTopLft { get; set; }
        public char ChTopRgt { get; set; }

        public char ChBotLft { get; set; }
        public char ChBotRgt { get; set; }

        public char ChHrz { get; set; }
        public char ChVrt { get; set; }

        public TextRectangle(Vector position, string[] content)
            : base(
                position,
                new Vector(
                    content.Max(line => line.Length) + 2 * MARGIN_SIZE + 2,
                    content.Length + 2))
        {
            _content = content;
        }

        protected override void Illustrate(char[,] illustration)
        {
            illustration[         0,          0] = ChTopLft;
            illustration[         0, Size.Y - 1] = ChBotLft;
            illustration[Size.X - 1,          0] = ChTopRgt;
            illustration[Size.X - 1, Size.Y - 1] = ChBotRgt;

            for (var x = 1; x < Size.X - 1; x++)
            {
                illustration[x,          0] = ChHrz;
                illustration[x, Size.Y - 1] = ChHrz;
            }
            for (var y = 1; y < Size.Y - 1; y++)
            {
                illustration[         0, y] = ChVrt;
                illustration[Size.X - 1, y] = ChVrt;

                for (var m = 1; m <= MARGIN_SIZE; m++)
                {
                    illustration[             m, y] = ' ';
                    illustration[Size.X - 1 - m, y] = ' ';
                }
            }

            var contentStart = new Vector(MARGIN_SIZE + 1, 1);
            var contentEnd = new Vector(Size.X - MARGIN_SIZE - 2, Size.Y - 2);

            for (var y = contentStart.Y; y <= contentEnd.Y; y++)
            {
                var line = _content[y - contentStart.Y];

                for (var x = contentStart.X; x <= contentEnd.X; x++)
                {
                    var column = x - contentStart.X;
                    illustration[x, y] = column < line.Length ? line[column] : ' ';
                }
            }
        }
    }
}
