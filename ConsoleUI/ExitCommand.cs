namespace ConsoleUI
{
    public class ExitCommand : ICommand
    {
        private readonly Application app;

        public string Name => "exit";
        public string Help => "Выход из программы";
        public string[] Synonyms => new[] { "quit", "bye" };
        public string Description => "Длинное и подробное описание команды выхода ";

        public ExitCommand(Application app)
        {
            this.app = app;
        }

        public void Execute(params string[] parameters)
        {
            app.Exit();
        }
    }
}
