using SneakerShop.SharedViewModel.Responses.Category;

namespace SneakerShop.CustomerUI.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:8000/api";

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>($"{_apiBaseUrl}/category");
                return categories ?? new List<CategoryResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching categories: {ex.Message}");
                return new List<CategoryResponse>();
            }
        }
    }
}