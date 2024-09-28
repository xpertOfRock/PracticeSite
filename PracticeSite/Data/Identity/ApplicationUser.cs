using Microsoft.AspNetCore.Identity;
using PracticeSite.Models.Entities;

namespace PracticeSite.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string UserRole { get; set; } = UserRoles.User;
        public ICollection<ApplicationForm> Applications { get; set; } = new List<ApplicationForm>();
    }
}
