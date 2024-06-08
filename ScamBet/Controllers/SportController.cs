using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

public class SportController : Controller
{
    private readonly HttpClient _httpClient;

    public SportController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Schedule()
    {
        string apiUrl = "https://localhost:5001/api/sports/schedule"; // Adjust the base URL as needed
        var response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return View("Schedule", data);
        }
        else
        {
            return View("Error");
        }
    }
}
