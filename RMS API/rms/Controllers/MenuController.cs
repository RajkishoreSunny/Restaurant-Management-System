using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.MenuService;
using Models.MenuRepo;
namespace Controller.MenuController
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]

    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("GetListOfMenu")]
        public ActionResult<List<Menu> > GetListOfMenu()
        {
            try
            {
                var menuItems = _menuService.GetListOfMenu();
                if(menuItems != null)
                {
                    return Ok(menuItems);
                }
                return BadRequest(404);
            }
            catch
            {
                return BadRequest(404);
            }
        }

        [HttpGet("GetMenuById")]
        public ActionResult<Menu> GetMenuById(int id)
        {
            try
            {
                var menu = _menuService.GetMenuById(id);
                if(menu != null)
                {
                    return Ok(menu);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest(404);
            }
        }

        [HttpPost("AddMenu")]
        public ActionResult<Menu> AddMenu([FromBody] Menu menu)
        {
            try
            {
                var response = _menuService.AddMenu(menu);
                if(response != null)
                {
                    return Ok(response);
                }
                return StatusCode(400);
            }
            catch
            {
                return BadRequest("Could Not Add");
            }
        }

        [HttpGet("GetMenuByCategoryId")]
        public ActionResult<List<Menu>> GetMenuByCategoryId(int Id)
        {
            try
            {
                var menu = _menuService.GetMenuByCategoryId(Id);
                if(menu != null)
                {
                    return Ok(menu);
                }
                return BadRequest();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("UploadImage")]
        public ActionResult<bool> UploadImage(IFormFile formFile, [FromServices] RMSDbContext dbContext, int Id)
        {
            try
            {
                var response = _menuService.UploadImage(formFile, dbContext, Id);
                if(response == true)
                {
                    return Ok(response);
                }
                return false;
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetMenuByName")]
        public ActionResult<List<Menu>> GetMenuByName(string Name)
        {
            try
            {
                var menuItems = _menuService.GetMenuByName(Name);
                if(menuItems != null)
                {
                    return Ok(menuItems);
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}