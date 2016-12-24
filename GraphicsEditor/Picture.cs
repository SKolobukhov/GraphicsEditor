using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor
{
	public sealed class Picture : CompoundShape
	{
		public event Action Changed;
		private readonly Dictionary<string, IShape> aliasMap = new Dictionary<string, IShape>();
		private readonly Dictionary<string, string> aliasCache = new Dictionary<string, string>();


		public void Print()
		{
			var writer = Console.Out;
			var map = GetShapesMap();
			foreach (var pair in map)
			{
				var aliases = aliasMap.Where(p => p.Value == pair.Value).Select(p => p.Key).ToArray();
				if (pair.Value is CompoundShape)
				{
					new TextWriterDrawer(pair.Key, aliases, writer).DrawCompoundShape();
				}
				else
				{
					pair.Value.Draw(new TextWriterDrawer(pair.Key, aliases, writer));
				}
			}
		}

		public void Alias(string shape, string alias)
		{
			if (aliasMap.ContainsKey(alias))
			{
				throw new ApplicationException($"For name {alias} shape was binded");
			}
			aliasCache[alias] = shape;
			aliasMap[alias] = GetShape(shape);
		}

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
				throw new ApplicationException($"Shape \"{shape}\" not found");
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
				if (!(shape.StartsWith("[") && shape.EndsWith("]")))
				{
					if (!aliasMap.ContainsKey(shape))
					{
						return null;
					}
					var index = aliasCache[shape];
					IShape currentShape;
					try
					{
						currentShape = GetShape(index);
					}
					catch (Exception)
					{
						currentShape = null;
					}
					if (aliasMap[shape].Equals(currentShape))
					{
						shape = index;
					}
					else
					{
						currentShape = aliasMap[shape];
						var map = GetShapesMap();
						if (!map.ContainsValue(currentShape))
						{
							aliasMap.Remove(shape);
							aliasCache.Remove(shape);
							return null;
						}
						var pair = map.First(p => p.Value.Equals(currentShape));
						aliasCache[shape] = pair.Key;
						shape = pair.Key;
					}
				}

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

		private Dictionary<string, IShape> GetShapesMap()
		{
			var result = new Dictionary<string, IShape>();
			GetShapesMap(this, string.Empty, result);
			return result.ToDictionary(pair => '[' + pair.Key.TrimStart(':') + ']', pair => pair.Value);
		}

		private string GetShapesMap(CompoundShape compoundShape, string prefix, Dictionary<string, IShape> shapesMap)
		{
			var i = -1;
			foreach (var shape in compoundShape.Shapes)
			{
				i++;
				shapesMap[prefix + ":" + i] = shape;
				var cShape = shape as CompoundShape;
				if (cShape != null)
				{
					var result = GetShapesMap(cShape, prefix + ":" + i, shapesMap);
					if (!string.IsNullOrEmpty(result))
					{
						return result;
					}
				}
			}
			return null;
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