using System;
using System.Collections.Generic;
using System.Text;
using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.queue.abstractions
{
    public delegate (bool IsSuccess, Exception Error) HandleNewHealthReportEventCallback(HealthReport healthReport);
}
