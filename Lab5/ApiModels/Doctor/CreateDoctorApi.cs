using Lab5.Models;
using System.ComponentModel.DataAnnotations;

namespace Lab5.ApiModels.Doctor
{
    public class CreateDoctorApi
    {
        [Required]
        public string Name { get; set; }

        public GenderType Gender { get; set; }
        [Required]
        public string Speciality { get; set; }
        public Guid? HospitalId { get; set; }

        public IEnumerable<(Guid Id, string Name)> Hospitals { get; set; } = new List<(Guid Id, string Name)>();
    }
}
