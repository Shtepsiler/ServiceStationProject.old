namespace ServiceStation.BLL.DTO.Responses
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? ClientName { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
