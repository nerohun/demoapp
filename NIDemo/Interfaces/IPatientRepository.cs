using NIDemo.Models;

namespace NIDemo.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatientById(int Id);

        bool IsTajExist(string tajNumber, int id);
        bool AddUserIsTajExist(string tajNumber);
        void AddPatient(Patient patient);
        void ModifyPatient(Patient patient);
        void DeletePatient(int Id);
        bool IsPatientExist(int id);
    }
}
