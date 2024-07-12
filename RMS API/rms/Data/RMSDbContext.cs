using Microsoft.EntityFrameworkCore;
using Models.TableModel;
using Models.About;
using Models.Category;
using Models.CustomerModel;
using Models.MenuRepo;
using Models.OrdersModel;
using Models.PaymentModel;
using Models.ReservationModel;
using Models.Manager;

public class RMSDbContext : DbContext
{
    public RMSDbContext(DbContextOptions<RMSDbContext> options) : base(options)
    {
        
    }
    public DbSet<MenuCategory>? MenuCategories { get; set; }
    public DbSet<Menu>? Menu { get; set; }
    public DbSet<About>? About { get; set; }
    public DbSet<Orders>? Orders { get; set; }
    public DbSet<Reservation>? Reservations { get; set; }
    public DbSet<Table>? Tables { get; set; }
    public DbSet<Payment>? Payments { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Manager>? Managers { get; set; }
}