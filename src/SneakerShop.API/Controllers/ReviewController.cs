using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Services;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Review;

namespace SneakerShop.API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController:ControllerBase
    {
        private readonly ReviewService _service;

        public ReviewController(ReviewService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            var result = await _service.CreateReview(request);
            return result ? Ok("Review created successfully.") : BadRequest("Failed to create review.");
        }
        
    }
}