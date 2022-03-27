using System.ComponentModel.DataAnnotations;

namespace Lab5.ApiModels.Hospital
{
    public class CreateHospitalApi
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Phones { get; set; }
    }
}
