using System;
using System.Collections.Generic;
using ConsoleUI;

namespace GraphicsEditor
{
    public class CreateGroupCommand: ICommand
    {
        private readonly Picture picture;

        public CreateGroupCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "group";
        public string Help => "Группировка фигур";
        public string Description => string.Empty;
        public string[] Synonyms => new string[0];

        public void Execute(params string[] parameters)
        {
            if (parameters == null || parameters.Length < 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters?.Length ?? 0}");
                return;
            }

            List<int> indexs = new List<int>();
            var sIndexs = parameters[0].TrimStart('[').TrimEnd(']').Split(':');
            foreach (var sIndex in sIndexs)
            {
                int index;
                if (!int.TryParse(sIndex, out index))
                {
                    Console.WriteLine($"Неверный параметр \"{sIndex}\"");
                    return;
                }
                indexs.Add(index);
            }

            try
            {
                picture.RemoveAt(indexs.ToArray());
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно удалить: {exception.Message}");
            }
        }
    }
}