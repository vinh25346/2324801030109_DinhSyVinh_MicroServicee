namespace DashboardService.Api.Queries.Dtos
{
    public class SalesSummaryDto
    {
        public decimal PremiumAmount { get; set; }
        public long PoliciesCount { get; set; }
    }

    public enum TimeUnit
    {
        Day,
        Week,
        Month,
        Year
    }
}

namespace DashboardService.Api.Queries
{
    using DashboardService.Api.Queries.Dtos;

    public class GetTotalSalesQuery
    {
        public string? ProductCode { get; set; }
        public DateTime SalesDateFrom { get; set; }
        public DateTime SalesDateTo { get; set; }
    }

    public class GetTotalSalesResult
    {
        public SalesSummaryDto Total { get; set; } = new();
        public Dictionary<string, SalesSummaryDto> PerProductTotal { get; set; } = new();
    }

    public class GetAgentsSalesQuery
    {
        public DateTime SalesDateFrom { get; set; }
        public DateTime SalesDateTo { get; set; }
    }

    public class GetAgentsSalesResult
    {
        public Dictionary<string, SalesSummaryDto> PerAgentTotal { get; set; } = new();
    }

    public class GetSalesTrendsQuery
    {
        public DateTime SalesDateFrom { get; set; }
        public DateTime SalesDateTo { get; set; }
        public TimeUnit Unit { get; set; }
    }

    public class GetSalesTrendsResult
    {
        public List<SalesTrendPeriodDto> PeriodsSales { get; set; } = new();
    }

    public class SalesTrendPeriodDto
    {
        public string Period { get; set; } = string.Empty;
        public SalesSummaryDto Sales { get; set; } = new();
    }
}
