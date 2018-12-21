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
                if (!TryGetValue(key, out shapes))
                    Add(key, shapes = new List<TextShape>());

                return shapes;
            }
        }

        public string[] Illustrate()
        {
            var width = Values.Max(shapes => shapes.Max(shape => shape.XPos + shape.XSize));
            var height = Values.Max(shapes => shapes.Max(shape => shape.YPos + shape.YSize));

            var illustration = new StringBuilder[height];
            for (var y = 0; y < height; y++)
                illustration[y] = new StringBuilder(new string(' ', width));

            foreach (var shapes in Values)
            {
                foreach (var shape in shapes)
                {
                    var shapeIllustration = shape.Illustrate();

                    for (var x = 0; x < shape.XSize; x++)
                    for (var y = 0; y < shape.YSize; y++)
                        illustration[shape.YPos + y][shape.XPos + x] = shapeIllustration[x, y];
                }
            }

            return illustration.Select(sb => sb.ToString()).ToArray();
        }
    }
}
