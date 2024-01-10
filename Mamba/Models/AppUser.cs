using Microsoft.AspNetCore.Identity;

namespace MambaProject.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
