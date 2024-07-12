using System;
using Microsoft.AspNetCore.Mvc;
using Models.MenuRepo;
namespace Repositories.MenuRepo
{
    public class MenuRepository : IMenuRepo
    {
        private readonly RMSDbContext _dbContext;

        public MenuRepository(RMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Menu AddMenu(Menu menu)
        {
            try
            {
                _dbContext.Add(menu);
                _dbContext.SaveChanges();
                return menu;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteMenu(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Menu> GetListOfMenu()
        {
            try
            {
                var menuList = _dbContext.Menu.ToList();
                return menuList;
            }
            catch
            {
                return null;
            }
        }

        public List<Menu> GetMenuByCategoryId(int Id)
        {
            try
            {
                var menu = _dbContext.Menu.Where(x => x.CategoryId == Id).ToList();
                if (menu != null)
                {
                    return menu;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Menu GetMenuById(int Id)
        {
            try
            {
                var menuItem = _dbContext.Menu.FirstOrDefault(x => x.MenuId == Id);
                if(menuItem != null)
                {
                    return menuItem;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<Menu> GetMenuByName(string Name)
        {
            if (string.IsNullOrEmpty(Name) || Name.Length < 1)
			{
				return null;
			}
			var foundItems = _dbContext.Menu
				.Where(m => m.Name.Contains(Name))
				.ToList();

			return foundItems;
        }

        public Menu UpdateMenu(int Id, Menu menu)
        {
            throw new NotImplementedException();
        }

        public bool UploadImage(IFormFile formFile, [FromServices] RMSDbContext dbContext, int Id)
        {
            try
            {
                if(formFile == null || formFile.Length == 0)
                {
                    return false;
                }
                byte[] ImageData;
                using (var memoryStream = new MemoryStream())
                {
                    formFile.CopyTo(memoryStream);
                    ImageData = memoryStream.ToArray();
                }
                var menuItem = dbContext.Menu.FirstOrDefault(x => x.MenuId == Id);
                if(menuItem == null)
                {
                    return false;
                }
                menuItem.ItemImg = ImageData;
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}