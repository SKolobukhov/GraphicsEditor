using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor
{
    public sealed class Picture : CompoundShape
    {
        public event Action Changed;

        public void Add(params IShape[] shapes)
        {
            Shapes.AddRange(shapes);
            Changed?.Invoke();
        }

        public void Transform(Transformation transformation, params string[] shapes)
        {
            shapes
                .Select(GetShape)
                .ToArray()
                .ForEach(shape => shape.Transform(transformation));
            Changed?.Invoke();
        }

        public void Remove(params string[] shapes)
        {
            var shapesToDelete = shapes.Select(GetShapeWithParent).ToArray();
            foreach (var shapeWithParent in shapesToDelete)
            {
                var parent = shapeWithParent.Parent;
                var shapeToDelete = shapeWithParent.Shape;
                parent.Shapes.Remove(shapeToDelete);
                while (parent.Parent != null && !parent.Shapes.Any())
                {
                    shapeToDelete = parent.Shape;
                    parent = parent.Parent;
                    parent.Shapes.Remove(shapeToDelete);
                }
            }
            Changed?.Invoke();
        }

        public void Move(int offset, params string[] shapes)
        {
            var shapesToMove = shapes.Select(GetShapeWithParent);
	        if (offset <= 0)
	        {
		        shapesToMove = shapesToMove.OrderBy(shape =>
		        {
			        var index = shape.GetIndex();
			        return index.Substring(1, index.Length - 2);
		        });
	        }
	        else
	        {
		        shapesToMove = shapesToMove.OrderByDescending(shape =>
		        {
			        var index = shape.GetIndex();
			        return index.Substring(1, index.Length - 2);
		        });
	        }
            foreach (var shapeWithParent in shapesToMove.ToArray())
            {
                var index = shapeWithParent.Parent.Shapes.IndexOf(shapeWithParent.Shape) + offset;
                shapeWithParent.Parent.Shapes.Remove(shapeWithParent.Shape);
                index = Math.Max(0, Math.Min(shapeWithParent.Parent.Shapes.Count, index));
                if (shapeWithParent.Parent.Shapes.Count == index)
                {
                    shapeWithParent.Parent.Shapes.Add(shapeWithParent.Shape);
                }
                else
                {
                    shapeWithParent.Parent.Shapes.Insert(index, shapeWithParent.Shape);
                }
            }
            Changed?.Invoke();
        }

        public IShape GetShape(string shape)
        {
            return GetShapeWithParent(shape).Shape;
        }

        public ShapeWrapper GetShapeWithParent(string shape)
        {
            var index = GetIndex(shape);
            if (index == null || !index.Any())
            {
                return null;
            }
            var parent = new CompoundShapeWrapper(this, null);
            for (var level = 0; level < index.Length - 1; level++)
            {
                if (parent.Shape == null || index[level] < 0 || index[level] >= parent.Shapes.Count)
                {
                    throw new ApplicationException($"Shape \"{shape}\" not found");
                }
                parent = new CompoundShapeWrapper(parent.Shapes[index[level]] as CompoundShape, parent);
            }
            if (parent.Shape == null || index[index.Length - 1] < 0 || index[index.Length - 1] >= parent.Shapes.Count)
            {
                throw new ApplicationException($"Shape \"{shape}\" not found");
            }
            return new ShapeWrapper(parent.Shapes[index[index.Length - 1]], parent);
        }

        private int[] GetIndex(string shape)
        {
            if (string.IsNullOrWhiteSpace(shape) || shape == "[]")
            {
                return null;
            }
            try
            {
                return shape
                    .Substring(1, shape.Length - 2)
                    .Split(':')
                    .Select(int.Parse)
                    .ToArray();
            }
            catch (Exception exception)
            {
                throw new ApplicationException($"Failed to parse index \"{shape}\": {exception.Message}", exception);
            }
        }

        public override void Draw(IDrawer drawer)
        {
            lock (locker)
            {
                foreach (var shape in Shapes)
                {
                    shape.Draw(drawer);
                }
            }
        }

        public Picture() 
            : base(new List<IShape>())
        { }
    }
}