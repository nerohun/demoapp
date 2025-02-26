namespace NIDemo.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TajNumber { get; set; }
        public string Complaint { get; set; }
        public string? Diagnosis { get; set; }
        public DateTime ArrivedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
