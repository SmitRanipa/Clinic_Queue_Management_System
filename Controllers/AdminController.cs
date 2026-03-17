using Clinic_Queue_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Clinic_Queue_Management.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : BaseController
    {

        // Deshbord 
        public async Task<IActionResult> Dashboard()
        {
            var client = GetClient();

            var response = await client.GetAsync("/admin/clinic");

            if (!response.IsSuccessStatusCode)
                return View();

            var data = await response.Content.ReadAsStringAsync();
            ViewBag.Clinic = JsonConvert.DeserializeObject(data);

            return View();
        }

        // disply Users
        public async Task<IActionResult> UserList()
        {
            var client = GetClient();

            var response = await client.GetAsync("/admin/users");

            if (!response.IsSuccessStatusCode)
                return View(new List<AdminUserViewModel>());

            var data = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<AdminUserViewModel>>(data);

            return View(users);
        }

        // add user form and post methd
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = GetClient();

            var body = new
            {
                name = model.Name,
                email = model.Email,
                password = model.Password,
                role = model.Role,
                phone = model.Phone
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/admin/users", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = error;
                return View(model);
            }

            return RedirectToAction("UserList");
        }
    }
}