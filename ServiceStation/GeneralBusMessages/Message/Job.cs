namespace GeneralBusMessages.Message
{
    public class Job
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }

        public int ModelId { get; set; }

        public string? Status { get; set; }
        public int ClientId { get; set; }

        public int? MechanicId { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

    }
}
