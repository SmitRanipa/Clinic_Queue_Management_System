using Clinic_Queue_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Clinic_Queue_Management.Controllers
{
    [Authorize(Roles = "patient")]
    public class PatientController : BaseController
    {
        public async Task<IActionResult> Dashboard()
        {
            // Optional: show upcoming appointments or stats
            return View();
        }

        public async Task<IActionResult> MyAppointments()
        {
            var client = GetClient();
            var response = await client.GetAsync("/appointments/my");

            if (!response.IsSuccessStatusCode)
                return View(new List<AppointmentViewModel>());

            var data = await response.Content.ReadAsStringAsync();
            var appointments = JsonConvert.DeserializeObject<List<AppointmentViewModel>>(data);

            return View(appointments);
        }

        public IActionResult BookAppointment()
        {
            return View(new BookAppointmentViewModel
            {
                AppointmentDate = DateTime.Now.ToString("yyyy-MM-dd")
            });
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(BookAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();
            var body = new
            {
                appointmentDate = model.AppointmentDate,
                timeSlot = model.TimeSlot
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/appointments", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = error;
                return View(model);
            }

            TempData["Success"] = "Appointment booked successfully!";
            return RedirectToAction("MyAppointments");
        }

        public async Task<IActionResult> AppointmentDetails(int id)
        {
            var client = GetClient();
            var response = await client.GetAsync($"/appointments/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("MyAppointments");

            var data = await response.Content.ReadAsStringAsync();
            var appointment = JsonConvert.DeserializeObject<AppointmentDetailsViewModel>(data);

            return View(appointment);
        }

        public async Task<IActionResult> MyReports()
        {
            var client = GetClient();
            var response = await client.GetAsync("/reports/my");

            if (!response.IsSuccessStatusCode)
                return View(new List<ReportViewModel>());

            var data = await response.Content.ReadAsStringAsync();
            var reports = JsonConvert.DeserializeObject<List<ReportViewModel>>(data);

            return View(reports);
        }

        public async Task<IActionResult> MyPrescriptions()
        {
            var client = GetClient();
            var response = await client.GetAsync("/prescriptions/my");

            if (!response.IsSuccessStatusCode)
                return View(new List<PrescriptionViewModel>());

            var data = await response.Content.ReadAsStringAsync();
            var prescriptions = JsonConvert.DeserializeObject<List<PrescriptionViewModel>>(data);

            return View(prescriptions);
        }
    }
}