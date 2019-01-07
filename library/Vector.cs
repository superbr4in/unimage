namespace Unimage
{
    public struct Vector
    {
        public int X;
        public int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Vector Zero
        {
            get { return new Vector(0, 0); }
        }

        public static Vector operator+(Vector vector1, Vector vector2)
        {
            return new Vector(
                vector1.X + vector2.X,
                vector1.Y + vector2.Y);
        }
        public static Vector operator-(Vector vector1, Vector vector2)
        {
            return new Vector(
                vector1.X - vector2.X,
                vector1.Y - vector2.Y);
        }
    }
}
