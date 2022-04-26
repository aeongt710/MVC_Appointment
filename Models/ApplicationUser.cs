using Microsoft.AspNetCore.Identity;

namespace MVC_Appointment.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
