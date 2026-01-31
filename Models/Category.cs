using System.ComponentModel.DataAnnotations;

namespace FinWise.API.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string TypeAllowed { get; set; } = "ambos";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
