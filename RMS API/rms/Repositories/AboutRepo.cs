using Models.About;

namespace Repositories.AboutRepo
{
    public class AboutRepository : IAbout
    {
        private readonly RMSDbContext _dbContext;
        public AboutRepository(RMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public About AddRestaurant(About about)
        {
            try
            {
                _dbContext.About.Add(about);
                _dbContext.SaveChanges();
                return about;
            }
            catch 
            {
                return null;
            }
        }

        public List<About> GetListOfRestaurants()
        {
            var listOfRestaurants = _dbContext.About.ToList();
            if(listOfRestaurants != null)
            {
                return listOfRestaurants;
            }
            return null;
        }

        public About GetRestaurantById(int id)
        {
            var restaurant = _dbContext.About.FirstOrDefault(a => a.AboutId == id);
            if(restaurant != null)
            {
                return restaurant;
            }
            return null;
        }
    }
}