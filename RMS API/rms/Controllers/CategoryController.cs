using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Category;
using Services.Category;

namespace Controller.Category
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllItems")]
        public ActionResult<List<MenuCategory>> GetAllItems()
        {
            try
            {
                var category = _categoryService.GetAllItems();
                return category;
            }
            catch
            {
                return StatusCode(404, "Not Found");
            }
            
        }

        [HttpGet("GetMenuCategoryById")]
        public ActionResult<MenuCategory> GetMenuCategoryById(int Id)
        {
            try
            {
                var category = _categoryService.GetMenuCategoryById(Id);
                return category;
            }
            catch
            {
                return NotFound("Couldn't Find what you're looking for");
            }
        }

        [HttpPut("UpdateMenu")]
        public ActionResult<MenuCategory> UpdateMenu(int Id, [FromBody] MenuCategory menuCategory)
        {
            try
            {
                var updateMenu = _categoryService.UpdateMenu(Id, menuCategory);
                return Ok("Updated Succesfully");
            }
            catch
            {
                return StatusCode(304, "Couldn't Update!");
            }
        }

        [HttpPost("UpdateCategoryImage")]
        public ActionResult<bool> UpdateCategoryImage(IFormFile formFile, [FromServices] RMSDbContext dbContext, int Id)
        {
            try
            {
                var categoryImg = _categoryService.UpdateCategoryImage(formFile, dbContext, Id);
                if(categoryImg == true)
                {
                    return Ok(true);
                }
                return false;
            }
            catch
            {
                return StatusCode(304, "Couldn't Upload");
            }
        }

        [HttpPost("AddMenu")]
        public ActionResult<MenuCategory> AddMenu([FromBody] MenuCategory menuCategory)
        {
            try
            {
                var addMenu = _categoryService.AddMenu(menuCategory);
                if(addMenu != null)
                {
                    return Ok(addMenu);
                }
                return BadRequest("Couldnot Add");
            }
            catch
            {
                return BadRequest("Couldnot Add");
            }
        }
    }
}