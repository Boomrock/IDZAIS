namespace AuthApi.Models
{
    public class AccessToken
    {
        public Guid Id { get; set; }
        public DateTime TokenExpirationDate { get; set; } = DateTime.MinValue;
    }
}
