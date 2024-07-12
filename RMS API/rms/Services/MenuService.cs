using System;
using Models.MenuRepo;
using Repositories.MenuRepo;
namespace Services.MenuService
{
    public class MenuService
    {
        private readonly IMenuRepo _menuRepo;

        public MenuService(IMenuRepo menuRepo)
        {
            _menuRepo = menuRepo;
        }
        public Menu AddMenu(Menu menu)
        {
            return _menuRepo.AddMenu(menu);
        }
        public List<Menu> GetListOfMenu()
        {
            return _menuRepo.GetListOfMenu();
        }
        public Menu GetMenuById(int id)
        {
           return  _menuRepo.GetMenuById(id);
        }
        public List<Menu> GetMenuByCategoryId(int Id)
        {
            return _menuRepo.GetMenuByCategoryId(Id);
        }
        public bool UploadImage(IFormFile formFile, RMSDbContext dbContext, int Id)
        {
            return _menuRepo.UploadImage(formFile, dbContext, Id);
        }
        public List<Menu> GetMenuByName(string Name)
        {
            return _menuRepo.GetMenuByName(Name);
        }
    }
}