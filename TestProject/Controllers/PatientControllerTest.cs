using Azure;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NIDemo.Controllers;
using NIDemo.Interfaces;
using NIDemo.Models;
using NuGet.Protocol;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestProject.Controllers
{
    public class PatientControllerTest
    {

        private readonly IPatientRepository _patinetRepository;
        public PatientControllerTest()
        {
            _patinetRepository = A.Fake<IPatientRepository>();

        }

        [Fact]
        public void PatientController_GetPatients_ReturnsOkResult()
        {
            // Arrange
            var patient = A.Fake<ICollection<Patient>>();
            A.CallTo(() => _patinetRepository.GetPatients()).Returns(patient);
            var controller = new PatientController(_patinetRepository);

            // Act
            var result = controller.GetPatients();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void PatientController_AddPatinet_ReturnsOkResult()
        {

            var patient = new Patient();

            patient.Name = "Teszt Ernő";
            patient.Address = "Teszt utca";
            patient.TajNumber = "111-222-333";
            patient.Complaint = "Teszt Panasz";
            patient.Diagnosis = "Teszt Diagnózis";
            patient.ArrivedAt = DateTime.Now;
            patient.LastModifiedAt = DateTime.Now;

            A.CallTo(() => _patinetRepository.AddPatient(patient));
            var controller = new PatientController(_patinetRepository);

            var result = controller.AddPatient(patient);

            result.Should().BeOfType(typeof(OkResult));

        }

        [Fact]
        public void PatientController_AddPatinet_Name_Returns_BadRequest()
        {
            var patient = new Patient();

            patient.Name = "";
            patient.Address = "Teszt utca";
            patient.TajNumber = "111-222-333";
            patient.Complaint = "Teszt Panasz";
            patient.Diagnosis = "Teszt Diagnózis";
            patient.ArrivedAt = DateTime.Now;
            patient.LastModifiedAt = DateTime.Now;

            A.CallTo(() => _patinetRepository.AddPatient(patient));
            var controller = new PatientController(_patinetRepository);

            var result = controller.AddPatient(patient);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = badRequestResult.Value as SerializableError;
            error.Should().ContainKey("Name");
            error["Name"].Should().BeEquivalentTo(new[] { "Name is invalid" });
        }

        [Fact]
        public void PatientController_AddPatinet_TajNmuber_Returns_BadRequest()
        {

            var patient = new Patient();

            patient.Name = "Teszt Ernő";
            patient.Address = "Teszt utca";
            patient.TajNumber = "111-222";
            patient.Complaint = "Teszt Panasz";
            patient.Diagnosis = "Teszt Diagnózis";
            patient.ArrivedAt = DateTime.Now;
            patient.LastModifiedAt = DateTime.Now;

            var patientList = A.Fake<ICollection<List<Patient>>>();
            A.CallTo(() => _patinetRepository.AddPatient(patient));
            var controller = new PatientController(_patinetRepository);

            controller.ModelState.Should().BeEmpty();
            var result = controller.AddPatient(patient);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = badRequestResult.Value as SerializableError;
            error.Should().ContainKey("TajNumber");
            error["TajNumber"].Should().BeEquivalentTo(new[] { "Taj number is invalid" });
        }

        [Fact]
        public void PatientController_ModifyPatinet_TajNmuber_Returns_BadRequest()
        {

            var controller = new PatientController(_patinetRepository);
            var patient = new Patient();

            patient.Id = 1;
            patient.Name = "Teszt Ernő";
            patient.Address = "Teszt utca";
            patient.TajNumber = "111-222-333";
            patient.Complaint = "Teszt Panasz";
            patient.Diagnosis = "Teszt Diagnózis";
            patient.ArrivedAt = DateTime.Now;
            patient.LastModifiedAt = DateTime.Now;

            A.CallTo(() => _patinetRepository.AddPatient(patient));
            var result = controller.AddPatient(patient);

            patient.Id = 1;
            patient.Name = "Teszt Ernő";
            patient.Address = "Teszt utca";
            patient.TajNumber = "111-222";
            patient.Complaint = "Teszt Panasz";
            patient.Diagnosis = "Teszt Diagnózis";
            patient.ArrivedAt = DateTime.Now;
            patient.LastModifiedAt = DateTime.Now;

            A.CallTo(() => _patinetRepository.ModifyPatient(patient));

            var result2 = controller.ModifyPatient(patient);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result2);
            var error = badRequestResult.Value as SerializableError;
            error.Should().ContainKey("TajNumber");
            error["TajNumber"].Should().BeEquivalentTo(new[] { "Taj number is invalid" });
        }

        [Fact]
        public void PatientController_ModifyPatinet_Name()
        {

            var controller = new PatientController(_patinetRepository);
            var patient = new Patient();

            patient.Id = 1;
            patient.Name = "Teszt Ernő";
            patient.Address = "Teszt utca";
            patient.TajNumber = "111-222-333";
            patient.Complaint = "Teszt Panasz";
            patient.Diagnosis = "Teszt Diagnózis";
            patient.ArrivedAt = DateTime.Now;
            patient.LastModifiedAt = DateTime.Now;

            A.CallTo(() => _patinetRepository.AddPatient(patient));
            var result = controller.AddPatient(patient);

            patient.Id = 1;
            patient.Name = "";
            patient.Address = "Teszt utca";
            patient.TajNumber = "111-222-333";
            patient.Complaint = "Teszt Panasz";
            patient.Diagnosis = "Teszt Diagnózis";
            patient.ArrivedAt = DateTime.Now;
            patient.LastModifiedAt = DateTime.Now;

            A.CallTo(() => _patinetRepository.ModifyPatient(patient));
            

            var result2 = controller.ModifyPatient(patient);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result2);
            var error = badRequestResult.Value as SerializableError;
            error.Should().ContainKey("Name");
            error["Name"].Should().BeEquivalentTo(new[] { "Name is invalid" });
        }
    }
}
