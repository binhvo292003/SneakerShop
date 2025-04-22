namespace SneakerShop.SharedViewModel.Requests.Review
{
    public class CreateReviewRequest
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}