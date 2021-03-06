using Lab5.Models;
using System.ComponentModel.DataAnnotations;

namespace Lab5.ApiModels.Patient
{
    public class CreatePatientApi
    {
        [Required]
        public string Name { get; set; }
        public GenderType Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
