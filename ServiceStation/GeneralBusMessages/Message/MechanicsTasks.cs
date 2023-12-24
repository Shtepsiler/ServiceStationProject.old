namespace GeneralBusMessages.Message
{
    public class MechanicsTasks
    {
        public int Id { get; set; }
        public int MechanicId { get; set; }

        public int? JobId { get; set; }

        public string Task { get; set; }
        public string Status { get; set; }


    }

}
