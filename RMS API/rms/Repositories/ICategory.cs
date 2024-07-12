using Microsoft.AspNetCore.Mvc;
using Models.Category;

namespace Repositories.Category
{
    public interface ICategory
    {
        public List<MenuCategory> GetAllItems();
        public MenuCategory GetMenuCategoryById(int id);
        public MenuCategory UpdateMenu(int Id, MenuCategory menuCategory);
        public MenuCategory AddMenu(MenuCategory menuCategory);
        public bool DeleteMenu(int id);
        public bool UpdateCategoryImage(IFormFile formFile, [FromServices] RMSDbContext dbContext, int Id);
    }
}