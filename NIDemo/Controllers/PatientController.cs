using Microsoft.AspNetCore.Mvc;
using NIDemo.Interfaces;
using NIDemo.Models;
using System.Text.RegularExpressions;

namespace NIDemo.Controllers
{


    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PatientController : ControllerBase
    {

        private readonly IPatientRepository _patinetRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patinetRepository = patientRepository;
        }

        [HttpGet(Name = "GetAllPatient")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patient>))]
        public ActionResult GetPatients()
        {
            var data = _patinetRepository.GetPatients().ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = _patinetRepository.GetPatients().ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        public ActionResult GetPateintById(int id)
        {
            if (!_patinetRepository.IsPatientExist(id))
                return NotFound();

            var data = _patinetRepository.GetPatientById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{tajNumber},{id}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        public ActionResult IsTajExist(string tajNumber, int id)
        {
            var data = _patinetRepository.IsTajExist(tajNumber, id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{tajNumber}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        public ActionResult AddUserIsTajExist(string tajNumber)
        {
            var data = _patinetRepository.AddUserIsTajExist(tajNumber);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpPost(Name = "AddPatient")]
        public ActionResult AddPatient([FromBody] Patient patient)
        {
            if (!Regex.IsMatch(patient.Name, @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
            {
                ModelState.AddModelError("Name", "Name is invalid");
            }

            if (!Regex.IsMatch(patient.TajNumber, @"^\d{3}-\d{3}-\d{3}$"))
            {
                ModelState.AddModelError("TajNumber", "Taj number is invalid");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {

                _patinetRepository.AddPatient(patient);
                return Ok();
            }
        }

        [HttpPost(Name = "ModifyPatient")]
        public ActionResult ModifyPatient([FromBody] Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name) || (string.IsNullOrWhiteSpace(patient.Name) && Regex.IsMatch(patient.Name, @"^[a-zA-Z\s]+$")))
            {
                ModelState.AddModelError("Name", "Name is invalid");
            }

            if (!Regex.IsMatch(patient.TajNumber, @"^\d{3}-\d{3}-\d{3}$"))
            {
                ModelState.AddModelError("TajNumber", "Taj number is invalid");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {

                _patinetRepository.ModifyPatient(patient);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePatient(int id)
        {
            _patinetRepository.DeletePatient(id);
            return Ok();
        }

    }
}
