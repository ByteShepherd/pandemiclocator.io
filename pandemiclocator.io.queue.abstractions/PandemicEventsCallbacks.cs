using System;
using System.Collections.Generic;
using System.Text;

namespace pandemiclocator.io.queue.abstractions
{
    public delegate (bool IsSuccess, Exception Error) HandleNewHealthReportEventCallback(string eventData);
}
