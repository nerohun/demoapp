using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Patient
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Taj Number Should be XXX-XXX-XXX")]
        public string TajNumber { get; set; }
        [Required]
        public string Complaint { get; set; }
        public string? Diagnosis { get; set; }
        [Required]
        public DateTimeOffset ArrivedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
