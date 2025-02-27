using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Doctor.Models;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        readonly Uri baseAddress = new Uri("https://localhost:7294/api");
        private readonly HttpClient _client;
        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Patient/GetPatients").Result;

            if (response.IsSuccessStatusCode)
            {
                if (ModelState.IsValid)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    patientList = JsonConvert.DeserializeObject<List<PatientViewModel>>(data);
                }
                else
                {
                    TempData["errorMessage"] = "Something went wrong.";
                }
            }
            return View(patientList);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PatientViewModel pat = new PatientViewModel();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Patient/GetPateintById/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                if (ModelState.IsValid)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    pat = JsonConvert.DeserializeObject<PatientViewModel>(data);
                }
                else
                {
                    TempData["errorMessage"] = "Something went wrong.";
                }
            }
            return View(pat);

        }

        [HttpPost]
        public IActionResult Edit(PatientViewModel patient)
        {
            try
            {
                HttpResponseMessage tajResponse = _client.GetAsync(_client.BaseAddress + "/Patient/IsTajExist/" + patient.TajNumber + "," + patient.Id).Result;


                if (tajResponse.IsSuccessStatusCode)
                {
                    var data = tajResponse.Content.ReadAsStringAsync().Result;

                    if (data.Equals("true"))
                    {
                        ModelState.AddModelError("TajNumber", "Taj Number is already exist");
                    }
                }

                if (!string.IsNullOrWhiteSpace(patient.Name) && Regex.IsMatch(patient.Name, @"^^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
                {
                    ModelState.AddModelError("Name", "Name is invalid");
                }

                if (!Regex.IsMatch(patient.TajNumber, @"^\d{3}-\d{3}-\d{3}$"))
                {
                    ModelState.AddModelError("TajNumber", "Taj number is invalid");
                }

                if (ModelState.IsValid)
                {
                    string data = JsonConvert.SerializeObject(patient);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Patient/ModifyPatient", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["succesMessage"] = " Patient data modified!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var error = response.Content.ReadAsStringAsync().Result;
                        TempData["errorMessage"] = error;
                    }
                }
                else
                {
                    return View(patient);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = " Patient data modify error:" + ex;
            }
            return View();
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            PatientViewModel pat = new PatientViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Patient/GetPateintById/" + id).Result;

            if (response.IsSuccessStatusCode)
            {

                if (ModelState.IsValid)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    pat = JsonConvert.DeserializeObject<PatientViewModel>(data);
                }
                else
                {
                    TempData["errorMessage"] = "Something went wrong";
                }
            }
            return View(pat);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                PatientViewModel patient = new PatientViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Patient/GetPateintById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    if (ModelState.IsValid)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        patient = JsonConvert.DeserializeObject<PatientViewModel>(data);
                        return View(patient);
                    }
                    else
                    {
                        TempData["errorMessage"] = "Something went wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Patient could not be deleted:" + ex;
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Patient/DeletePatient/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    if (ModelState.IsValid)
                    {
                        TempData["succesMessage"] = "Patient deleted successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Something went wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Patient could not be deleted:" + ex;
            }
            return View();
        }
    }
}
