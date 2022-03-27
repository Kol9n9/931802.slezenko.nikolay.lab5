using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class DoctorModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public GenderType Gender { get; set; }

        [Required]
        public string Speciality { get; set; }


        public Guid? HospitalId { get; set; }
        public HospitalModel Hospital { get; set; }
    }
}
