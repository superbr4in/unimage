using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unimage
{
    public class TextCanvas : SortedDictionary<double, List<TextShape>>
    {
        public new List<TextShape> this[double key]
        {
            get
            {
                List<TextShape> shapes;
                if (!base.TryGetValue(key, out shapes))
                    base.Add(key, shapes = new List<TextShape>());

                return shapes;
            }
        }

        public void Add(double layer, IEnumerable<TextShape> shapes)
        {
            base.Add(layer, shapes.ToList());
        }

        public string[] Illustrate()
        {
            var size = new Vector(
                base.Values.Max(shapes => shapes.Max(shape => shape.Position.X + shape.Size.X)),
                base.Values.Max(shapes => shapes.Max(shape => shape.Position.Y + shape.Size.Y)));

            var illustration = new StringBuilder[size.Y];
            for (var y = 0; y < size.Y; y++)
                illustration[y] = new StringBuilder(new string(' ', size.X));

            foreach (var shapes in base.Values)
            {
                foreach (var shape in shapes)
                {
                    var shapeIllustration = shape.Illustrate();

                    for (var x = 0; x < shape.Size.X; x++)
                    for (var y = 0; y < shape.Size.Y; y++)
                        illustration[shape.Position.Y + y][shape.Position.X + x] = shapeIllustration[x, y];
                }
            }

            return illustration.Select(sb => sb.ToString()).ToArray();
        }
    }
}
