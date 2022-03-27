using System.ComponentModel.DataAnnotations;

namespace Lab5.ApiModels.Lab
{
    public class EditLabApi
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
