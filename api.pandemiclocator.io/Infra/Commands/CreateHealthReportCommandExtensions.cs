using System;
using pandemiclocator.io.database.abstractions;

namespace api.pandemiclocator.io.Infra.Commands
{
    public static class CreateHealthReportCommandExtensions
    {
        public static ReadHealthReportCommand ToReadCommand(this HealthReport model)
        {
            if (model == null)
            {
                return null;
            }

            return new ReadHealthReportCommand
            (
                model.Status,
                model.Quantity,
                model.Source,
                model.Latitude,
                model.Longitude,
                model.When
            );
        }

        public static HealthReport ToModel(this CreateHealthReportCommand command)
        {
            if (command == null)
            {
                return null;
            }

            if (command.Quantity < 1 || command.Quantity > 10)
            {
                command.Quantity = 1;
            }

            return new HealthReport
            (
                command.Identifier,
                command.Status,
                command.Quantity,
                command.Source,
                command.Latitude,
                command.Longitude,
                DateTime.UtcNow
            );
        }
    }
}
