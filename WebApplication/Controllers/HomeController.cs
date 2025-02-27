using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using NurseClient.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;

namespace NurseClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly Uri baseAddress = new Uri("https://localhost:7294/api");
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(PatientViewModel patient)
        {
            try
            {
                if (patient.TajNumber != null)
                {
                    HttpResponseMessage tajResponse = _client.GetAsync(_client.BaseAddress + "/Patient/IsTajExist/" + patient.TajNumber + "," + patient.Id).Result;


                    if (tajResponse.IsSuccessStatusCode)
                    {
                        var data = tajResponse.Content.ReadAsStringAsync().Result;

                        if (data.Equals("true"))
                        {
                            ModelState.AddModelError("TajNumber", "Taj is already exist");
                        }
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Patient TajNumber is null";

                }


                if (!Regex.IsMatch(patient.Name, @"^([a-zA-Z]{2,}\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\s?([a-zA-Z]{1,})?)"))
                {
                    ModelState.AddModelError("Name", "Name is invalid");
                }

                if (patient.TajNumber != null && !Regex.IsMatch(patient.TajNumber, @"^\d{3}-\d{3}-\d{3}$"))
                {
                    ModelState.AddModelError("TajNumber", "Taj number is invalid");
                }

                if (ModelState.IsValid)
                {

                    string data = JsonConvert.SerializeObject(patient);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Patient/AddPatient", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["succesMessage"] = " Patient added successfully";
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
                TempData["errorMessage"] = " Patient could not be added" + ex;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
