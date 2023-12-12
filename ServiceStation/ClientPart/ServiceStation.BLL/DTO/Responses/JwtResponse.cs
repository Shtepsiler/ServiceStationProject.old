namespace ServiceStation.BLL.DTO.Responses
{
    public class JwtResponse
    {
        public int Id { get; set; }
        public string ClientName { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
