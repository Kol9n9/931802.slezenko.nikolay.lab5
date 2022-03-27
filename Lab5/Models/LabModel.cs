using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class LablModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
