using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakerShop.SharedViewModel.Requests.Auth;
using SneakerShop.SharedViewModel.Responses.Auth;

namespace SneakerShop.CustomerUI.Pages.Auth
{
    public class Login : PageModel
    {
        private readonly IHttpClientFactory _httpFactory;

        public Login(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        [BindProperty]
        public LoginRequest Input { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpFactory.CreateClient("API");
            var loginUri = new Uri("http://localhost:8000/api/auth/login");
            var json = JsonSerializer.Serialize(Input);
            var resp = await client.PostAsync(loginUri,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }

            var content = await resp.Content.ReadAsStringAsync();
            var loginResp = JsonSerializer.Deserialize<LoginResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Add to cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,                    
                Secure = true,                   
                SameSite = SameSiteMode.None,     
            };
            Response.Cookies.Append("AccessToken", loginResp.Token, cookieOptions);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginResp.Email),
                new Claim("AccessToken", loginResp.Token)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true });

            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                returnUrl = "/";
            return LocalRedirect(returnUrl);
        }
    }
}