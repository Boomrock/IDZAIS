namespace AuthApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string LoginHash { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
