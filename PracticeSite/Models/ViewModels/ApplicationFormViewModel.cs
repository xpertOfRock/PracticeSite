using System.ComponentModel.DataAnnotations;

namespace PracticeSite.Models.ViewModels
{
    public class ApplicationFormViewModel
    {
        [Required(ErrorMessage = "Введіть зоголовок заяви")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введіть опис заявки")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введіть овне ім'я")]
        public string FullName { get; set; }

        [Required]
        [Range(18, 100, ErrorMessage = "Вік має бути між 18 та 100 роками")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Номер телефону")]
        [Phone(ErrorMessage = "Невірний формат номеру")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Електронна пошта")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть площу землі, яку ви хочете здати в аренду")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Площадь має бути більше за 0")]
        public double LandArea { get; set; }

        [Required(ErrorMessage = "Введіть ціну за гектар")]
        [Range(1, double.MaxValue, ErrorMessage = "Ціна має бути більше 0")]
        public decimal PricePerHectare { get; set; }

        [Required(ErrorMessage = "Область")]
        public string State { get; set; }

        [Required(ErrorMessage = "Населений пункт")]
        public string City { get; set; }

        [Required(ErrorMessage = "Влиця")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Поштовий індекс")]
        public string PostalCode { get; set; }
    }

}
