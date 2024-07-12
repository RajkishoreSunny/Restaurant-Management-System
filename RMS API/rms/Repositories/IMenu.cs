using System;
using Models.MenuRepo;
namespace Repositories.MenuRepo
{
    public interface IMenuRepo
    {
        public List<Menu> GetListOfMenu();
        public Menu UpdateMenu(int Id, Menu menu);
        public Menu GetMenuById(int Id);
        public Menu AddMenu(Menu menu);
        public bool DeleteMenu(int Id);
        public List<Menu> GetMenuByCategoryId(int Id);
        public bool UploadImage(IFormFile formFile, RMSDbContext dbContext, int Id);
        public List<Menu> GetMenuByName(string Name);
    }
}