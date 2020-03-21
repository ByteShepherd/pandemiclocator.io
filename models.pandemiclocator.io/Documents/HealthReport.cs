using System;
using System.Collections.Generic;
using System.Text;
using GeoAPI.Geometries;
using models.pandemiclocator.io.Documents.Enums;

namespace models.pandemiclocator.io.Documents
{
    public class HealthReport
    {
        public string Identifier { get; }
        public HealthStatus Status { get; }
        public ReportSource Source { get; }
        public IPoint Coordinate { get; }
        public DateTime When { get; }

        public HealthReport(string identifier, HealthStatus status, ReportSource source, IPoint coordinate, DateTime when)
        {
            Identifier = identifier;
            Status = status;
            Source = source;
            Coordinate = coordinate;
            When = when;
        }
    }
}
