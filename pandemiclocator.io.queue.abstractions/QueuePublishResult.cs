using System;

namespace pandemiclocator.io.queue.abstractions
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