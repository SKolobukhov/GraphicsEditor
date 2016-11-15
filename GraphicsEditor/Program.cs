using ConsoleUI;
using DrawablesUI;

namespace GraphicsEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            var picture = new Picture();
            var ui = new DrawableGUI(picture);
            var app = new Application();

            var shapeProvider = new ShapeProvider()
                .Bind<PointShape>("point")
                .Bind<LineShape>("line")
                .Bind<EllipseShape>("ellipse")
                .Bind<CircleShape>("circle");

            app.AddCommand(new ExitCommand(app));
            app.AddCommand(new ExplainCommand(app));
            app.AddCommand(new HelpCommand(app));
            app.AddCommand(new AddShapeCommand(picture, shapeProvider));
            app.AddCommand(new ListShapesCommand(picture));
            app.AddCommand(new RemoveShapeCommand(picture));

            picture.Changed += ui.Refresh;
            ui.Start();
            app.Run();
            ui.Stop();
        }
    }
}
