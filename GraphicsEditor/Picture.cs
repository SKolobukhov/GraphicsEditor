using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor
{
    public sealed class Picture : CompoundShape
    {
        public event Action Changed;

        public void Add(params Shape[] shapes)
        {
            Add(shapes.Select(shape => new Tuple<string, Shape>(string.Empty, shape)).ToArray());
        }

        public void Add(params Tuple<string, Shape>[] shapes)
        {
            foreach (var shape in shapes)
            {
                var compoundShape = GetShape(shape.Item1) as CompoundShape;
                if (compoundShape == null)
                {
                    throw new ArgumentException($"The shape({shape.Item1}) is not compound");
                }
                compoundShape.Shapes.Add(shape.Item2);
            }
            Changed?.Invoke();
        }

        public void Remove(params string[] shapeNames)
        {
            var shapesMap = GetShapesMap();
            foreach (var shapeName in shapeNames)
            {
                if (!shapeName.StartsWith("[") || !shapeName.EndsWith("]"))
                {
                    throw new ApplicationException($"Incorrect format: \"{shapeName}\"");
                }
                var path = shapeName.Substring(1, shapeName.Length - 2);
                if (!shapesMap.ContainsKey(path))
                {
                    throw new ApplicationException($"Shape by path \"{shapeName}\" not found");
                }
                var shapeToDelete = shapesMap[path];
                var separatorIndex = path.LastIndexOf(":", StringComparison.OrdinalIgnoreCase);
                path = string.Empty;
                if (separatorIndex != -1)
                {
                    path = path.Substring(0, separatorIndex - 1);
                }
                var compoundShape = (CompoundShape)shapesMap[path];
                compoundShape.Shapes.Remove(shapeToDelete);
            }
            Changed?.Invoke();
        }

        public void Move(string shapeName, int offset)
        {
            var shape = GetShape(shapeName);
            var path = shapeName.Substring(1, shapeName.Length - 2);
            var separatorIndex = path.LastIndexOf(":", StringComparison.OrdinalIgnoreCase);
            path = string.Empty;
            if (separatorIndex != -1)
            {
                path = path.Substring(0, separatorIndex - 1);
            }
            var compoundShape = (CompoundShape)GetShape(string.IsNullOrEmpty(path) ? string.Empty : "[" + path + "]");
            Remove(shapeName);
            var index = compoundShape.Shapes.IndexOf(shape) + offset;
            index = Math.Max(0, Math.Min(compoundShape.Shapes.Count, index));
            compoundShape.Shapes.Insert(index, shape);
            Changed?.Invoke();
        }

        private Dictionary<string, Shape> GetShapesMap()
        {
            var result = new Dictionary<string, Shape> { { string.Empty, this } };
            GetShapesMap(result, string.Empty, this);
            return result;
        }

        private void GetShapesMap(Dictionary<string, Shape> map, string prefix, CompoundShape compoundShape)
        {
            for (var index = 0; index < compoundShape.Shapes.Count; index++)
            {
                var path = prefix + (string.IsNullOrEmpty(prefix) ? string.Empty : ":") + index;
                var shape = compoundShape.Shapes[index];
                map.Add(path, shape);
                var cShape = shape as CompoundShape;
                if (cShape != null)
                {
                    GetShapesMap(map, path, cShape);
                }
            }
        }

        private int[] GetIndexs(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new int[0];
            }
            var args = path.TrimStart('[').TrimEnd(']').Split(':');
            var indexs = new List<int>();
            foreach (var arg in args)
            {
                int index;
                if (!int.TryParse(arg, out index))
                {
                    throw new ApplicationException($"Failed to parse path \"{path}\"");
                }
                indexs.Add(index);
            }
            return indexs.ToArray();
        }

        public Shape GetShape(string path)
        {
            var indexs = GetIndexs(path);
            return GetShape(indexs);
        }

        public Shape GetShape(int[] indexs)
        {
            if (indexs == null || !indexs.Any())
            {
                return this;
            }
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

        protected override void DrawShape(IDrawer drawer)
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