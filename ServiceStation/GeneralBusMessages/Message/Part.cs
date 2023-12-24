namespace GeneralBusMessages.Message
{
    public class Part
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int VendorId { get; set; }

        public int StockQty { get; set; }


    }
}
