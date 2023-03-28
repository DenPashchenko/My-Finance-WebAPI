using MyFinance.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Domain
{
    public class Transaction
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name length can't be more then 50 and less then 2.")]
        public string Name { get; set; } = null!;

        [StringLength(250, MinimumLength = 2, ErrorMessage = "Description length can't be more then 250 and less then 2.")]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The value must be equal or greater than 0.01")]
        public decimal Sum { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime? DateOfEditing { get; set; }
    }
}
