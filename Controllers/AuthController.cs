using Clinic_Queue_Management.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Clinic_Queue_Management.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");
        }

        // Login Page
        public IActionResult Login()
        {
            return View();
        }

        // Login Post Method 

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var body = new
            {
                email = model.Email,
                password = model.Password
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid login!";
                return View();
            }

            var data = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(data);

            string token = result.token;
            string role = result.user.role;
            string name = result.user.name;

            // Save in Sesion
            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("Role", role);

            // Create Cokie Authenticatoin
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim("JWToken", token)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            
            if (role == "admin")
                return RedirectToAction("Dashboard", "Admin");

            if (role == "doctor")
                return RedirectToAction("Dashboard", "Doctor");

            if (role == "receptionist")
                return RedirectToAction("Dashboard", "Receptionist");

            if (role == "patient")
                return RedirectToAction("Dashboard", "Patient");

            return RedirectToAction("Login");
        }

        // Lgoout 
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login");
        }
            
    }
}
