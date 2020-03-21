﻿using System;
using System.Collections.Generic;
using System.Text;
using models.pandemiclocator.io.Documents.Enums;

namespace models.pandemiclocator.io.Documents
{
    public class HealthReport
    {
        public string Identifier { get; }

        [Required]
        public HealthStatus Status { get; }

        [Required]
        public ReportSource Source { get; }
        public (double latidude, double longitude) Coordinate { get; }
        public DateTime When { get; }

        public HealthReport(string identifier, HealthStatus status, ReportSource source, (double latidude, double longitude) coordinate, DateTime when)
        {
            Identifier = identifier;
            Status = status;
            Source = source;
            Coordinate = coordinate;
            When = when;
        }
    }
}
