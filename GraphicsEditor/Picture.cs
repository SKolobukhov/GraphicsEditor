using System;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor
{
    public class Picture : CompoundShape
    {
        public event Action Changed;


        public void Add(IShape shape)
        {
            Add(new[] { Shapes.Count }, shape);
        }

        public void Add(int[] indexs, IShape shape)
        {
            var index = indexs[indexs.Length - 1];
            var compoundShape = indexs.Length < 2 ? this : GetShape(indexs.Take(indexs.Length - 1).ToArray()) as CompoundShape;
            if (compoundShape == null || index < 0)
            {
                throw new ArgumentException($"Incorrect index {index}({indexs.Length - 1}) in [{string.Join(",", indexs).Trim(',')}]");
            }
            index = index >= compoundShape.Shapes.Count ? compoundShape.Shapes.Count : index;
            lock (locker)
            {
                compoundShape.Shapes.Insert(index, shape);
            }
            Changed?.Invoke();
        }

        public void RemoveAt(int[] indexs)
        {
            var index = indexs[indexs.Length - 1];
            var compoundShape = indexs.Length < 2 ? this : GetShape(indexs.Take(indexs.Length - 1).ToArray()) as CompoundShape;
            if (compoundShape == null || index < 0 || index >= compoundShape.Shapes.Count)
            {
                throw new ArgumentException($"Incorrect index {index}({indexs.Length - 1}) in [{string.Join(",", indexs).Trim(',')}]");
            }
            lock (locker)
            {
                compoundShape.Shapes.RemoveAt(index);
            }
            Changed?.Invoke();
        }

        private IShape GetShape(int[] indexs)
        {
            CompoundShape compoundShape = this;
            for (var level = 0; level < indexs.Length - 1; level++)
            {
                if (compoundShape == null || indexs[level] < 0 || indexs[level] >= compoundShape.Shapes.Count)
                {
                    throw new ArgumentException($"Incorrect index {indexs[level]}({level}) in [{string.Join(",", indexs).Trim(',')}]");
                }
                compoundShape = compoundShape.Shapes[indexs[level]] as CompoundShape;
            }
            var index = indexs[indexs.Length - 1];
            if (compoundShape == null || index < 0 || index >= compoundShape.Shapes.Count)
            {
                throw new ArgumentException($"Incorrect index {index}({indexs.Length - 1}) in [{string.Join(",", indexs).Trim(',')}]");
            }
            return compoundShape.Shapes[index];
        }

        public new void Draw(IDrawer drawer)
        {
            lock (locker)
            {
                foreach (var shape in Shapes)
                {
                    shape.Draw(drawer);
                }
            }
        }
    }
}