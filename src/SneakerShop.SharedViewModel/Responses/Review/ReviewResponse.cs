namespace SneakerShop.SharedViewModel.Responses.Review
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}