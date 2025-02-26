using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Doctor.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string TajNumber { get; set; }
        public string Complaint { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ArrivedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}

