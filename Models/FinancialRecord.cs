using System.ComponentModel.DataAnnotations;

namespace FinWise.API.Models
{
    public class FinancialRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public string Type { get; set; } = "saida";

        [Required]
        public string Title { get; set; } = default!;

        public decimal Amount { get; set; } 
        
        public Guid CategoryId { get; set; }

        public DateTime Date {  get; set; }

        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
