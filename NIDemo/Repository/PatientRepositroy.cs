using NIDemo.Data;
using NIDemo.Interfaces;
using NIDemo.Models;

namespace NIDemo.Repository
{
    public class PatientRepositroy : IPatientRepository
    {
        private readonly DataContext _context;
        public PatientRepositroy(DataContext context)
        {
            _context = context;
        }

        public ICollection<Patient> GetPatients()
        {
            return _context.Patient.OrderBy(p => p.ArrivedAt).ToList();
        }

        public Patient GetPatientById(int Id)
        {

            Patient patient = new();
            var query = _context.Patient.Where(p => p.Id == Id).FirstOrDefault();

            if (query == null)
            {
                return patient;
            }
            patient.Id = query.Id;
            patient.Name = query.Name.Trim();
            patient.Address = query.Address.Trim();
            patient.TajNumber = query.TajNumber.Trim();
            patient.Complaint = query.Complaint.Trim();
            patient.Diagnosis = query.Diagnosis != null ? query.Diagnosis.Trim() : string.Empty;
            patient.ArrivedAt = query.ArrivedAt;
            patient.LastModifiedAt = query.LastModifiedAt;


            return patient;
        }

        public bool IsTajExist(string tajNumber, int id)
        {

            var other = _context.Patient.OrderBy(x => x.ArrivedAt).Where(p => p.TajNumber == tajNumber && p.Id != id).ToList();

            bool isExist = other.Count > 0;


            return isExist;
        }

        public bool AddUserIsTajExist(string tajNumber)
        {

            var other = _context.Patient.OrderBy(x => x.ArrivedAt).Where(p => p.TajNumber == tajNumber).ToList();

            bool isExist = other.Count > 0;


            return isExist;
        }
        public void AddPatient(Patient patient)
        {
            patient.Name = patient.Name.Trim();
            patient.Address = patient.Address.Trim();
            patient.TajNumber = patient.TajNumber.Trim();
            patient.Complaint = patient.Complaint.Trim();
            patient.Diagnosis = patient.Diagnosis != null ? patient.Diagnosis.Trim() : string.Empty;
            patient.ArrivedAt = patient.ArrivedAt.ToLocalTime();
            patient.LastModifiedAt = DateTime.UtcNow.AddHours(1);
            _context.Patient.Add(patient);
            _context.SaveChanges();
        }

        public void ModifyPatient(Patient patient)
        {
            var patientData = _context.Patient.Where(p => p.Id == patient.Id).FirstOrDefault();

            if (patientData != null)
            {
                patientData.Name = patient.Name.Trim();
                patientData.Address = patient.Address.Trim();
                patientData.TajNumber = patient.TajNumber.Trim();
                patientData.Complaint = patient.Complaint.Trim();
                patientData.Diagnosis = patient.Diagnosis != null ? patient.Diagnosis.Trim() : string.Empty;
                patientData.ArrivedAt = patient.ArrivedAt;
                patientData.LastModifiedAt = DateTime.UtcNow.AddHours(1);
            }

            _context.SaveChanges();
        }

        public void DeletePatient(int Id)
        {
            var patientData = _context.Patient.Where(p => p.Id == Id).FirstOrDefault();

            if (patientData != null)
            {
                _context.Patient.Remove(patientData);
                _context.SaveChanges();
            }
        }

        public bool IsPatientExist(int id)
        {
            var patient = _context.Patient.Where(p => p.Id == id).FirstOrDefault();

            bool isExist = patient != null;

            return isExist;
        }
    }
}
