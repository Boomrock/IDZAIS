using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models
{
    public class Client
    {
        [Key] 
        public Guid Guid { get; set; }
        public DateTime TokenExpirationDate { get; set; } = DateTime.MinValue;

    }
}
