using Models.Category;
using Repositories.Category;

namespace Services.Category
{
    public class CategoryService
    {
        private readonly ICategory _category;

        public CategoryService(ICategory category)
        {
            _category = category; 
        }
        public List<MenuCategory> GetAllItems()
        {
            return _category.GetAllItems();
        }
        public MenuCategory GetMenuCategoryById(int Id)
        {
            return _category.GetMenuCategoryById(Id);
        }
        public MenuCategory UpdateMenu(int Id, MenuCategory menuCategory)
        {
            return _category.UpdateMenu(Id, menuCategory);
        }
        public bool UpdateCategoryImage(IFormFile formFile, RMSDbContext dbContext, int Id)
        {
            return _category.UpdateCategoryImage(formFile, dbContext, Id);
        }
        public MenuCategory AddMenu(MenuCategory menuCategory)
        {
            return _category.AddMenu(menuCategory);
        }
    }
}