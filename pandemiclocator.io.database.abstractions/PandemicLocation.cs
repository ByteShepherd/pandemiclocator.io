using System;

namespace pandemiclocator.io.database.abstractions
{
    public sealed class PandemicLocation : Tuple<double, double>
    {
        public PandemicLocation(double latitude, double longitude) : base(latitude, longitude)
        {
        }

        public double Latitude => base.Item1;
        public double Longitude => base.Item2;
    }
}