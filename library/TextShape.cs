using System.Linq;

namespace Unimage
{
    public abstract class TextShape
    {
        public int XPos { get; }
        public int YPos { get; }

        public int XSize { get; }
        public int YSize { get; }

        protected TextShape(
            int xPos, int yPos,
            int xSize, int ySize)
        {
            XPos = xPos;
            YPos = yPos;

            XSize = xSize;
            YSize = ySize;
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
            int xPos, int yPos,
            string[] content)
            : base(
                xPos, yPos,
                content.Max(line => line.Length) + 2 * MARGIN_SIZE + 2, content.Length + 2)
        {
            _content = content;
        }

        public override char[,] Illustrate()
        {
            var illustration = new char[XSize, YSize];

            illustration[        0,         0] = TopLeft;
            illustration[        0, YSize - 1] = BottomLeft;
            illustration[XSize - 1,         0] = TopRight;
            illustration[XSize - 1, YSize - 1] = BottomRight;

            for (var x = 1; x < XSize - 1; x++)
            {
                illustration[x,         0] = HLine;
                illustration[x, YSize - 1] = HLine;
            }
            for (var y = 1; y < YSize - 1; y++)
            {
                illustration[        0, y] = VLine;
                illustration[XSize - 1, y] = VLine;

                for (var m = 0; m < MARGIN_SIZE; m++)
                {
                    illustration[        m + 1, y] = ' ';
                    illustration[XSize - m - 2, y] = ' ';
                }
            }

            var contentXStart = MARGIN_SIZE + 1;
            var contentYStart = 1;

            var contentXEnd = XSize - MARGIN_SIZE - 2;
            var contentYEnd = YSize - 2;

            for (var y = contentYStart; y <= contentYEnd; y++)
            {
                var line = _content[y - contentYStart];

                for (var x = contentXStart; x <= contentXEnd; x++)
                {
                    var column = x - contentXStart;
                    illustration[x, y] = column < line.Length ? line[column] : ' ';
                }
            }

            return illustration;
        }
    }
}
