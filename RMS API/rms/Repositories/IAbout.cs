using Models.About;

namespace Repositories.AboutRepo
{
    public interface IAbout
    {
        public List<About> GetListOfRestaurants();
        public About GetRestaurantById(int id);
        public About AddRestaurant(About about);
    }
}