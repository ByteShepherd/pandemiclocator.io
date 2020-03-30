namespace pandemiclocator.io.database.abstractions.Models
{
    public struct PandemicReport
    {
        public int Quantity { get; set; }
        public HealthStatus Status { get; set; }
        public ReportSource Source { get; set; }
        public ReportLocation Location { get; set; }
    }
}