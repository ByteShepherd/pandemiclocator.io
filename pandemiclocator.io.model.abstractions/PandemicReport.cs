namespace pandemiclocator.io.model.abstractions
{
    public struct PandemicReport
    {
        public int Quantity { get; set; }
        public HealthStatus Status { get; set; }
        public ReportSource Source { get; set; }
        public ReportLocation Location { get; set; }
    }
}