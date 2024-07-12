using Repositories.AboutRepo;
using Models.About;

namespace Services.AboutSvc
{
    public class AboutService 
    {
        private readonly IAbout _about;
        public AboutService(IAbout about)
        {
            _about = about;
        }
        public List<About> GetListOfRestaurants()
        {
            return _about.GetListOfRestaurants();
        }
        public About GetRestaurantById(int id)
        {
            return _about.GetRestaurantById(id);
        }
        public About AddRestaurant(About about)
        {
            return _about.AddRestaurant(about);
        }
    }
}