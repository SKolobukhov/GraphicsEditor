using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using ConsoleUI;
using DrawablesUI;

namespace GraphicsEditor
{
    public class GroupCommand : ICommand
    {
        private readonly Picture picture;

        public GroupCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "group";
        public string Help => "Группировка фигур";
        public string Description => "group shape1 [shape2 ...]";
        public string[] Synonyms => new string[0];

        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            try
            {
                var shapes = parameters
					.Select(picture.GetShapeWithParent)
					.ToList();
	            if (HasShapeWithParent(shapes))
				{
					throw new ApplicationException("Can not group shape twice or with parent");
				}
                var group = new CompoundShape(shapes.Select(shape => shape.Shape).ToList());
                picture.Remove(parameters);
                picture.Add(group);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно сгруппировать: {exception.Message}");
            }
        }

	    private bool HasShapeWithParent(List<ShapeWrapper> shapes)
	    {
		    foreach (var shape in shapes)
		    {
			    if (shapes.Any(parent => shape != parent && IsParent(shape, parent.Shape)))
			    {
				    return true;
			    }
		    }
		    return false;
	    }

	    private bool IsParent(ShapeWrapper shape, IShape parent)
	    {
		    while (shape != null)
		    {
			    if (shape.Shape == parent)
			    {
				    return true;
			    }
			    shape = shape.Parent;
		    }
		    return false;
	    }
    }
}