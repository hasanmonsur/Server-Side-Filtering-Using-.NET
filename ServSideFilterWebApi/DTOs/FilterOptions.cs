namespace ServSideFilterWebApi.DTOs
{
    public class FilterOptions
    {
        public string? SearchTerm { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
