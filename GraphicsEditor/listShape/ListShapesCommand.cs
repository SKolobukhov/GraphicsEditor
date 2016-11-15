using ConsoleUI;

namespace GraphicsEditor
{
    public class ListShapesCommand : ICommand
    {
        private readonly Picture picture;

        public ListShapesCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "list";
        public string Help => "Выводит список фигур на картинке.";
        public string Description => string.Empty;
        public string[] Synonyms => new[] { "ls" };

        public void Execute(params string[] parameters)
        {
            picture.Draw(new TextWriterDrawer());
        }
    }
}