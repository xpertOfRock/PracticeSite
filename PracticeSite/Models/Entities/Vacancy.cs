using PracticeSite.Data.Identity;
using PracticeSite.Models.Enums;
using PracticeSite.Models.ValueObjects;

namespace PracticeSite.Models.Entities
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public VacancyStatus Status { get; set; } = VacancyStatus.Active;
        public Address Address { get; set; }
        public ICollection<ApplicationUser>? RespondedUsers { get; set; } = new List<ApplicationUser>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ClosedAt { get; set; }
    }
}
