using System.ComponentModel.DataAnnotations;

namespace MyFinance.Domain
{
    public class Category
    {
        [Key]
        public int CategoryId { get; private set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name length can't be more then 50 and less then 2.")]
        public string Name { get; set; } = null!;
    }
}
