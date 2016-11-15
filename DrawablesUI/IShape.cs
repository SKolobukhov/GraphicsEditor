namespace DrawablesUI
{
    public interface IShape: IDrawable
    {
        void Transform(Transformation transformation);
    }
}