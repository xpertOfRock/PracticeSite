using PracticeSite.Models.Enums;

namespace PracticeSite.Models.ViewModels
{

    public class VacancyViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public VacancyStatus Status { get; set; } = VacancyStatus.Active;

    }
}
