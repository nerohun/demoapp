using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NurseClient.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        [Required]
        [DisplayName("Taj Number")]
        public string TajNumber { get; set; }
        [Required]
        [DisplayName("Complaint")]
        public string Complaint { get; set; }
        public string? Diagnosis { get; set; }
        [Required]
        [DisplayName("Arrived")]
        public DateTime ArrivedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
