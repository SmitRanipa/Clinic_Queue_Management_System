using Clinic_Queue_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Queue_Management.Controllers
{
    [Authorize(Roles = "receptionist")]
    public class ReceptionistController : BaseController
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> DailyQueue(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            ViewBag.SelectedDate = date;

            var client = GetClient();
            var response = await client.GetAsync($"/queue?date={date}");

            if (!response.IsSuccessStatusCode)
                return View(new List<ReceptionistQueueViewModel>());

            var data = await response.Content.ReadAsStringAsync();
            var queue = JsonConvert.DeserializeObject<List<ReceptionistQueueViewModel>>(data);

            return View(queue);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status, string date)
        {
            var client = GetClient();
            var body = new { status = status };
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"/queue/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to update status.";
            }
            else
            {
                TempData["Success"] = "Status updated successfully!";
            }

            return RedirectToAction("DailyQueue", new { date = date });
        }
    }
}
