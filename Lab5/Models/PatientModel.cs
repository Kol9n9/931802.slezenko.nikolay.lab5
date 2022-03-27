using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class PatientModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public GenderType Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

    }
}
