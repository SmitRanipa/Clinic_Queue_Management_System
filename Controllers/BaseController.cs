using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

public class BaseController : Controller
{
    protected HttpClient GetClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud");

        // Attach JWT token from session
        var token = HttpContext.Session.GetString("JWToken");
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return client;
    }
}