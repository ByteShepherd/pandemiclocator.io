﻿using System;
using System.ComponentModel.DataAnnotations;
using api.pandemiclocator.io.Infra.Data.Documents;
using api.pandemiclocator.io.Infra.Data.Enums;

namespace api.pandemiclocator.io.Infra.Commands
{
    public class CreateHealthReportCommand
    {
        [Required]
        [MinLength(1)]
        public string Identifier { get; set; }

        [Required]
        public HealthStatus Status { get; set; }

        [Required]
        public ReportSource Source { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }

    public static class CreateHealthReportCommandExtensions
    {
        public static HealthReport ToModel(this CreateHealthReportCommand command)
        {
            if (command == null)
            {
                return null;
            }

            return new HealthReport
            (
                command.Identifier,
                command.Status,
                command.Source,
                command.Latitude, 
                command.Longitude,
                DateTime.UtcNow
            );
        }
    }
}
