using System;
using ConsoleUI;

namespace GraphicsEditor
{
	public class AddAliasCommand: ICommand
	{
		private readonly Picture picture;
		
		public string Name => "alias";
		public string Help => "Задает фигуре псевдоним";
		public string Description => "alias shape alias";
		public string[] Synonyms => new string[0];


		public AddAliasCommand(Picture picture)
		{
			this.picture = picture;
		}

		public void Execute(params string[] parameters)
		{
			if (parameters.Length != 2)
			{
				Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
				return;
			}

			picture.Alias(parameters[0], parameters[1]);
		}
	}
}