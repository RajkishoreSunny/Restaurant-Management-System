using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Category;

namespace Models.Category
{
    public class CategoryRepo : ICategory
    {
        private readonly RMSDbContext _dbContext;

        public CategoryRepo(RMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MenuCategory AddMenu(MenuCategory menuCategory)
        {
            try
            {
                _dbContext.MenuCategories.Add(menuCategory);
                _dbContext.SaveChanges();
                return menuCategory;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteMenu(int id)
        {
            throw new NotImplementedException();
        }

        public List<MenuCategory> GetAllItems()
        {
            try
            {
                var menuCategory = _dbContext.MenuCategories.ToList();
                return menuCategory;
            }
            catch 
            {
                return null;
            }
        }

        public MenuCategory GetMenuCategoryById(int id)
        {
            try
            {
                var category = _dbContext.MenuCategories.FirstOrDefault(x => x.CategoryId == id);
                return category;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCategoryImage(IFormFile formFile, [FromServices] RMSDbContext dbContext, int Id)
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
                var categoryImg = _dbContext.MenuCategories.FirstOrDefault(x => x.CategoryId == Id);
                if(categoryImg == null)
                {
                    return false;
                }
                categoryImg.CategoryImg = ImageData;
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MenuCategory UpdateMenu(int Id, MenuCategory menuCategory)
        {
            try
            {
                var updateMenu = _dbContext.MenuCategories.FirstOrDefault(x => x.CategoryId == Id);
                updateMenu.CategoryName = menuCategory.CategoryName;
                updateMenu.Description = menuCategory.Description;
                _dbContext.SaveChanges();
                return updateMenu;
            }
            catch
            {
                return null;
            }
        }
    }
}