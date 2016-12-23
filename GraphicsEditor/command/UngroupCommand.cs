using System;
using System.Linq;
using ConsoleUI;

namespace GraphicsEditor
{
    public class UngroupCommand : ICommand
    {
        private readonly Picture picture;

        public UngroupCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "ungroup";
        public string Help => "Разгруппировка фигуры";
        public string Description => "ungroup group1 [group2 ...]";
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
					.Select(shape =>
		            {
			            var shapeWithParent = picture.GetShapeWithParent(shape);
						var compoundShape = shapeWithParent.Shape as CompoundShape;
						if (compoundShape == null)
						{
							throw new ApplicationException($"Выбранная фигура({shape}) - не составная");
						}
			            return new CompoundShapeWrapper(compoundShape, shapeWithParent.Parent);
		            })
					.OrderByDescending(shape =>
					{
						var index = shape.GetIndex();
						return index.Substring(1, index.Length - 2);
					})
					.ToArray();
	            foreach (var shapeWithParent in shapes)
	            {
		            shapeWithParent.Parent.Shapes.Remove(shapeWithParent.Shape);
		            shapeWithParent.Parent.Shapes.AddRange(shapeWithParent.Shapes.ToArray());
	            }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно разгруппировать: {exception.Message}");
            }
        }
    }
}