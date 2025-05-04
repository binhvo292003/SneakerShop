using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.SharedViewModel.Requests.Auth;

namespace SneakerShop.CustomerUI.Pages.Auth
{
    public class Register : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        [BindProperty]
        public RegisterRequest Input { get; set; }

        public Register(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpFactory.CreateClient("API");
            var loginUri = new Uri("http://localhost:8000/api/auth/register");
            var json = JsonSerializer.Serialize(Input);
            var resp = await client.PostAsync(loginUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (resp.IsSuccessStatusCode)
            {
                return RedirectToPage("Login");
            }

            var content = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError("", content);
            return Page();
        }
    }
}