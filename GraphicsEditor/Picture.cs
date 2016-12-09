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
            shapes.ForEach(shape => Shapes.Add(shape));
            Changed?.Invoke();
        }

        public void Transform(string shapeIndex, Transformation transformation)
        {
            if (!shapeIndex.StartsWith("[") || !shapeIndex.EndsWith("]"))
            {
                throw new ApplicationException($"Incorrect format: \"{shapeIndex}\"");
            }
            shapeIndex = shapeIndex.Substring(1, shapeIndex.Length - 2);
            var shape = GetShapeByIndex(shapeIndex);
            shape.Transform(transformation);
            Changed?.Invoke();
        }

        public void Remove(params string[] shapeIndexs)
        {
            var shapeIndexsMap = GetShapeIndexsMap();
            foreach (var shapeIndex in shapeIndexs)
            {
                if (!shapeIndex.StartsWith("[") || !shapeIndex.EndsWith("]"))
                {
                    throw new ApplicationException($"Incorrect format: \"{shapeIndex}\"");
                }
                var index = shapeIndex.Substring(1, shapeIndex.Length - 2);
                if (!shapeIndexsMap.ContainsKey(index))
                {
                    throw new ApplicationException($"Shape by index \"{shapeIndex}\" not found");
                }
                var shapeToDelete = shapeIndexsMap[index];
                var lastSeparatorIndex = index.LastIndexOf(":", StringComparison.OrdinalIgnoreCase);
                index = lastSeparatorIndex != -1 ? index.Substring(0, lastSeparatorIndex) : string.Empty;
                var compoundShape = (CompoundShape)shapeIndexsMap[index];
                compoundShape.Shapes.Remove(shapeToDelete);
            }
            Changed?.Invoke();
        }

        public void Move(string shapeIndex, int offset)
        {
            if (!shapeIndex.StartsWith("[") || !shapeIndex.EndsWith("]"))
            {
                throw new ApplicationException($"Incorrect format: \"{shapeIndex}\"");
            }
            shapeIndex = shapeIndex.Substring(1, shapeIndex.Length - 2);
            var shape = GetShapeByIndex(shapeIndex);
            var lastSeparatorIndex = shapeIndex.LastIndexOf(":", StringComparison.OrdinalIgnoreCase);
            shapeIndex = lastSeparatorIndex != -1 ? shapeIndex.Substring(0, lastSeparatorIndex) : string.Empty;
            var compoundShape = (CompoundShape)GetShapeByIndex(shapeIndex);
            compoundShape.Shapes.Remove(shape);
            var index = compoundShape.Shapes.IndexOf(shape) + offset;
            index = Math.Max(0, Math.Min(compoundShape.Shapes.Count, index));
            compoundShape.Shapes.Insert(index, shape);
            Changed?.Invoke();
        }

        private Dictionary<string, Shape> GetShapeIndexsMap()
        {
            var result = new Dictionary<string, Shape> { { string.Empty, this } };
            GetShapeIndexsMap(result, string.Empty, this);
            return result;
        }

        private void GetShapeIndexsMap(Dictionary<string, Shape> map, string prefix, CompoundShape compoundShape)
        {
            for (var index = 0; index < compoundShape.Shapes.Count; index++)
            {
                var path = prefix + (string.IsNullOrEmpty(prefix) ? string.Empty : ":") + index;
                var shape = compoundShape.Shapes[index];
                map.Add(path, shape);
                var cShape = shape as CompoundShape;
                if (cShape != null)
                {
                    GetShapeIndexsMap(map, path, cShape);
                }
            }
        }

        private int[] GetIndexs(string shapeIndex)
        {
            if (string.IsNullOrEmpty(shapeIndex) || shapeIndex == "[]")
            {
                return new int[0];
            }
            var args = shapeIndex.TrimStart('[').TrimEnd(']').Split(':');
            var indexs = new List<int>();
            foreach (var arg in args)
            {
                int index;
                if (!int.TryParse(arg, out index))
                {
                    throw new ApplicationException($"Failed to parse index \"{shapeIndex}\"");
                }
                indexs.Add(index);
            }
            return indexs.ToArray();
        }

        public Shape GetShapeByIndex(string shapeIndex)
        {
            var indexs = GetIndexs(shapeIndex);
            if (indexs == null || !indexs.Any())
            {
                return this;
            }
            CompoundShape compoundShape = this;
            for (var level = 0; level < indexs.Length - 1; level++)
            {
                if (compoundShape == null || indexs[level] < 0 || indexs[level] >= compoundShape.Shapes.Count)
                {
                    throw new ApplicationException($"Shape by index \"{shapeIndex}\" not found");
                }
                compoundShape = compoundShape.Shapes[indexs[level]] as CompoundShape;
            }
            var index = indexs[indexs.Length - 1];
            if (compoundShape == null || index < 0 || index >= compoundShape.Shapes.Count)
            {
                throw new ApplicationException($"Shape by index \"{shapeIndex}\" not found");
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