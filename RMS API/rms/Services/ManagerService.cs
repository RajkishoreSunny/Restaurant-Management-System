using Models.LoginModel;
using Models.Manager;
using Repositories.ManagerRepository;
namespace Services.ManagerSvc
{
    public class ManagerService
    {
        private readonly IManager _manager;
        public ManagerService(IManager manager)
        {
            _manager = manager;
        }

        public Manager UpdateManager(int Id, Manager manager)
        {
            return _manager.UpdateManager(Id, manager);
        }
        public object LoginManager(Login login)
        {
            return _manager.LoginManager(login);
        }
    }
}