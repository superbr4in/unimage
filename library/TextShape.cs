using System.Linq;

namespace Unimage
{
    public abstract class TextShape
    {
        public Vector Position { get; }

        public Vector Size { get; }

        protected TextShape(Vector position, Vector size)
        {
            Position = position;

            Size = size;
        }

        public abstract char[,] Illustrate();
    }

    public class TextRectangle : TextShape
    {
        private const int MARGIN_SIZE = 1;

        private readonly string[] _content;

        public char TopLeft  { get; set; }
        public char TopRight { get; set; }

        public char BottomLeft  { get; set; }
        public char BottomRight { get; set; }

        public char HLine { get; set; }
        public char VLine { get; set; }

        public TextRectangle(
            Vector position,
            string[] content)
            : base(
                position,
                new Vector(
                    content.Max(line => line.Length) + 2 * MARGIN_SIZE + 2,
                    content.Length + 2))
        {
            _content = content;
        }

        public override char[,] Illustrate()
        {
            var illustration = new char[Size.X, Size.Y];

            illustration[         0,          0] = TopLeft;
            illustration[         0, Size.Y - 1] = BottomLeft;
            illustration[Size.X - 1,          0] = TopRight;
            illustration[Size.X - 1, Size.Y - 1] = BottomRight;

            for (var x = 1; x < Size.X - 1; x++)
            {
                illustration[x,          0] = HLine;
                illustration[x, Size.Y - 1] = HLine;
            }
            for (var y = 1; y < Size.Y - 1; y++)
            {
                illustration[         0, y] = VLine;
                illustration[Size.X - 1, y] = VLine;

                for (var m = 0; m < MARGIN_SIZE; m++)
                {
                    illustration[         m + 1, y] = ' ';
                    illustration[Size.X - m - 2, y] = ' ';
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

            return illustration;
        }
    }
}
