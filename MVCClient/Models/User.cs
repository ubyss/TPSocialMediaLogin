using Microsoft.Build.Framework;

namespace MVCClient.Models
{
    public class User
    {
        public string username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
    }
}
