using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.About;
using Services.AboutSvc;

namespace Controller.AboutCtrl
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class AboutController : ControllerBase
    {
        private readonly AboutService _aboutService;

        public AboutController(AboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet("GetListOfRestaurants")]
        public ActionResult<List<About>> GetListOfRestaurants()
        {
            try
            {
                var listOfRestaurant = _aboutService.GetListOfRestaurants();
                if(listOfRestaurant != null)
                {
                    return Ok(listOfRestaurant);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetRestaurantById")]
        public ActionResult<About> GetRestaurantById(int id)
        {
            try
            {
                var restaurant = _aboutService.GetRestaurantById(id);
                if(restaurant != null)
                {
                    return Ok(restaurant);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddRestaurant")]
        public ActionResult<About> AddRestaurant(About about)
        {
            try
            {
                var addedRestaurant = _aboutService.AddRestaurant(about);
                if(addedRestaurant != null)
                {
                    return Ok(addedRestaurant);
                }
                return StatusCode(403, "Could Not Add");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}