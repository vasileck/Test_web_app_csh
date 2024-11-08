using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым.")]
        public string SenderCity { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым.")]
        public string SenderAddress { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым.")]
        public string RecipientCity { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым.")]
        public string RecipientAddress { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым.")]
        [Range(1, 999, ErrorMessage = "Вес груза не может превышать 1000 кг.")]
        public decimal CargoWeight { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Order), nameof(ValidatePickupDate), ErrorMessage = "Дата забора не может быть прошедшей.")]
        public DateTime PickupDate { get; set; }

        public static ValidationResult ValidatePickupDate(DateTime pickupDate, ValidationContext context)
        {
            if (pickupDate.Date < DateTime.Now.Date)
            {
                return new ValidationResult("Дата забора не может быть прошедшей.");
            }
            return ValidationResult.Success;
        }

        public string? OrderNumber { get; set; }

    }
}
