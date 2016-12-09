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
        public string Help => "�������������� ������";
        public string Description => string.Empty;
        public string[] Synonyms => new string[0];

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 1)
            {
                Console.WriteLine($"�������� ���������� ����������: {parameters.Length}");
                return;
            }

            try
            {
                var compoundShape = picture.GetShapeByIndex(parameters[0]) as CompoundShape;
                if (compoundShape == null)
                {
                    throw new ApplicationException($"��������� ������({parameters[0]}) - �� ���������");
                }
                picture.Remove(parameters[0]);
                picture.Add(compoundShape.Shapes.ToArray());
            }
            catch (Exception exception)
            {
                Console.WriteLine($"���������� ���������������: {exception.Message}");
            }
        }
    }
}