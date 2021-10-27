public struct XZCoordinate
{
    public int X;
    public int Z;

    public XZCoordinate(int x, int z)
    {
        X = x;
        Z = z;
    }

    public override string ToString()
    {
        return X + " , " + Z;
    }
}