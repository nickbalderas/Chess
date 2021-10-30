namespace structs
{
    public readonly struct XZCoordinate
    {
        public readonly int X;
        public readonly int Z;

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
}