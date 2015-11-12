namespace eOne.Common.Connectors.SalesForce.Models.Metadata
{
    public class SalesForceLimits
    {

        public SalesForceLimit ConcurrentAsyncGetReportInstances { get; set; }
        public SalesForceLimit ConcurrentSyncReportRuns { get; set; }
        public SalesForceLimit DailyApiRequests { get; set; }
        public SalesForceLimit DailyAsyncApexExecutions { get; set; }
        public SalesForceLimit DailyBulkApiRequests { get; set; }
        public SalesForceLimit DailyGenericStreamingApiEvents { get; set; }
        public SalesForceLimit DailyStreamingApiEvents { get; set; }
        public SalesForceLimit DailyWorkflowEmails { get; set; }
        public SalesForceLimit DataStorageMB { get; set; }
        public SalesForceLimit FileStorageMB { get; set; }
        public SalesForceLimit HourlyAsyncReportRuns { get; set; }
        public SalesForceLimit HourlyDashboardRefreshes { get; set; }
        public SalesForceLimit HourlyDashboardResults { get; set; }
        public SalesForceLimit HourlyDashboardStatuses { get; set; }
        public SalesForceLimit HourlySyncReportRuns { get; set; }
        public SalesForceLimit HourlyTimeBasedWorkflow { get; set; }
        public SalesForceLimit MassEmail { get; set; }
        public SalesForceLimit SingleEmail { get; set; }
        public SalesForceLimit StreamingApiConcurrentClients { get; set; }

    }
}
