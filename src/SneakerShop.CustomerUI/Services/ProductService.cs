using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SneakerShop.CustomerUI.Models;

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

        public async Task<ProductItem?> GetProductByIdAsync(long id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProductItem>($"{_apiBaseUrl}/products/{id}");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error fetching product {id}: {ex.Message}");
                return null;
            }
        }

    }
}