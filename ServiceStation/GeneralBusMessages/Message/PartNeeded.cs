namespace GeneralBusMessages.Message
{
    public class PartNeeded
    {
        public int Id { get; set; }
        public int JobId { get; set; }

        public int PartId { get; set; }

        public int Quantity { get; set; }
    }
}
