using SneakerShop.CustomerUI.Models;
using SneakerShop.SharedViewModel.Requests.Review;
using SneakerShop.SharedViewModel.Responses.Product;
using SneakerShop.SharedViewModel.Responses.Review;

namespace SneakerShop.CustomerUI.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:8000/api"; // Use your API base URL

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductItem>> GetAllProductsAsync()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductItem>>($"{_apiBaseUrl}/products");
                return products ?? new List<ProductItem>();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return new List<ProductItem>();
            }
        }

        public async Task<ProductDetailResponse> GetProductByIdAsync(long id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProductDetailResponse>($"{_apiBaseUrl}/products/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching product {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProductItem>> GetProductsByFilterAsync(List<long> categoryIds)
        {
            try
            {
                var query = string.Join(",", categoryIds);
                var products = await _httpClient.GetFromJsonAsync<List<ProductItem>>($"{_apiBaseUrl}/products/filter?categoryQuery={query}");
                return products ?? new List<ProductItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching filtered products: {ex.Message}");
                return new List<ProductItem>();
            }
        }

        public async Task<HttpResponseMessage> SubmitReviewAsync(CreateReviewRequest review)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"{_apiBaseUrl}/reviews",
                review);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Review submitted successfully.");
            }
            else
            {
                Console.WriteLine($"Error submitting review: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
            return response;
        }

        public async Task<List<ReviewResponse>> GetReviewsByProductIdAsync(long productId)
        {
            try
            {
            return await _httpClient.GetFromJsonAsync<List<ReviewResponse>>($"{_apiBaseUrl}/reviews?productId={productId}");
            }
            catch (Exception ex)
            {
            Console.WriteLine($"Error fetching reviews for product {productId}: {ex.Message}");
            return new List<ReviewResponse>();
            }
        }
    }
}