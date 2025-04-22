using Microsoft.AspNetCore.Mvc;
using SneakerShop.Application.Common.Services;
using SneakerShop.Domain.Repositories;
using SneakerShop.SharedViewModel.Requests.Review;

namespace SneakerShop.API.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _service;

        public ReviewController(ReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviewsByProductId(long productId)
        {
            if (productId <= 0)
            {
                return BadRequest("Invalid product ID.");
            }

            var result = await _service.GetAllReviewsByProductId(productId);
            return result is null ? BadRequest() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request)
        {
            try
            {

                if (request == null)
                {
                    return BadRequest("Request cannot be null.");
                }

                var result = await _service.CreateReview(request);
                return result ? Ok("Review created successfully.") : BadRequest("Failed to create review.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}