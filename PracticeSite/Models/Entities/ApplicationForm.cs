using PracticeSite.Models.Enums;
using PracticeSite.Models.ValueObjects;

namespace PracticeSite.Models.Entities
{
    public class ApplicationForm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public double LandArea { get; set; }
        public decimal PricePerHectare { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.InProgress;
        public Address Address { get; set; }
    }
}
