namespace SneakerShop.SharedViewModel.Requests.Category
{
    public class UpdateCategoryRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}