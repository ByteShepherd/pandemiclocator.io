using System;

namespace pandemiclocator.io.abstractions.Queue
{
    public class QueuePublishResult
    {
        public bool Success { get; }
        public Exception Error { get; }

        public QueuePublishResult(bool success, Exception error = null)
        {
            Success = success;
            Error = error;
        }
    }
}