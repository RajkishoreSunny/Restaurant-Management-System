using Models.LoginModel;
using Models.Manager;

namespace Repositories.ManagerRepository
{
    public interface IManager
    {
        public Manager UpdateManager(int Id, Manager manager);
        public object LoginManager(Login login);
    }
}