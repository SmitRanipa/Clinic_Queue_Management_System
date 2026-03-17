using Clinic_Queue_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic_Queue_Management.Controllers
{
    [Authorize(Roles = "doctor")]
    public class DoctorController : BaseController
    {
        // Deshbord
        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> TodayQueue()
        {
            var client = GetClient();
            var response = await client.GetAsync("/doctor/queue");

            if (!response.IsSuccessStatusCode)
                return View(new List<DoctorQueueViewModel>());

            var data = await response.Content.ReadAsStringAsync();
            var queue = JsonConvert.DeserializeObject<List<DoctorQueueViewModel>>(data);

            return View(queue);
        }

        public IActionResult AddPrescription(int id)
        {
            return View(new AddPrescriptionViewModel
            {
                AppointmentId = id,
                Medicines = new List<PrescriptionMedicine>
                {
                    new PrescriptionMedicine(),
                    new PrescriptionMedicine(),
                    new PrescriptionMedicine()
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(AddPrescriptionViewModel model)
        {
            var client = GetClient();

            var validMedicines = model.Medicines?.Where(m => !string.IsNullOrWhiteSpace(m.Name)).ToList() ?? new List<PrescriptionMedicine>();

            var body = new
            {
                medicines = validMedicines,
                notes = model.Notes
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/prescriptions/{model.AppointmentId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = error;
                if (model.Medicines == null || !model.Medicines.Any()) {
                     model.Medicines = new List<PrescriptionMedicine> { new PrescriptionMedicine(), new PrescriptionMedicine(), new PrescriptionMedicine() };
                }
                return View(model);
            }

            TempData["Success"] = "Prescription added successfully!";
            return RedirectToAction("TodayQueue");
        }

        public IActionResult AddReport(int id)
        {
            return View(new AddReportViewModel { AppointmentId = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(AddReportViewModel model)
        {
            var client = GetClient();
            var body = new
            {
                diagnosis = model.Diagnosis,
                testRecommended = model.TestRecommended,
                remarks = model.Remarks
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/reports/{model.AppointmentId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = error;
                return View(model);
            }

            TempData["Success"] = "Report added successfully!";
            return RedirectToAction("TodayQueue");
        }
    }
}
