namespace DrawablesUI
{
    public interface IShape: IDrawable
    {
        ///Maybe, need to round up number to nearest, e.g. "1,99999999F". Something, like that:
        ///var tmp_ = 1.99999F;
        ///tmp_ = (float)System.Math.Round(tmp_, 3, System.MidpointRounding.AwayFromZero);
        void Transform(Transformation transformation);
    }
}