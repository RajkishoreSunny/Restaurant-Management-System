using Microsoft.EntityFrameworkCore;

public class TableStatusUpdateService : BackgroundService
{
  private readonly IServiceProvider _serviceProvider;
  private readonly TimeSpan _delayInterval;
  private readonly TimeSpan _seatedThreshold;
  private readonly ILogger<TableStatusUpdateService> _logger;

  public TableStatusUpdateService(IServiceProvider serviceProvider, ILogger<TableStatusUpdateService> logger)
  {
    _serviceProvider = serviceProvider;
    _logger = logger;
    _delayInterval = TimeSpan.FromMinutes(1); 
    _seatedThreshold = TimeSpan.FromMinutes(5);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        using (var scope = _serviceProvider.CreateScope())
        {
          var dbContext = scope.ServiceProvider.GetRequiredService<RMSDbContext>();

          var now = DateTime.UtcNow;
          var windowStart = now;
          var windowEnd = now.AddMinutes(5);

          _logger.LogInformation($"Checking reservations between {windowStart:O} and {windowEnd:O}");


          var reservationsToUpdate = await dbContext.Reservations
                        .Include(r => r.Table)
                        .Where(r => r.ReservationDateTime >= windowStart && r.ReservationDateTime <= windowEnd)
                        .ToListAsync(stoppingToken);

          // Update table statuses for reservations within the window
          foreach (var reservation in reservationsToUpdate)
          {
            if (reservation.Table != null)
            {
              var timeDifference = now - reservation.ReservationDateTime;

              Console.WriteLine($"Reservation ID: {reservation.ReservationId}, Time Difference: {timeDifference.TotalMinutes} minutes");

              if (timeDifference <= TimeSpan.FromMinutes(1))
              {
                reservation.Table.Status = "Seated";
              }
              dbContext.Entry(reservation.Table).State = EntityState.Modified;
            }
            _logger.LogInformation($"Current Reservations: {reservation.ReservationId}");
          }
          await dbContext.SaveChangesAsync(stoppingToken);

          //For AVailable status update
          

          _logger.LogInformation($"Updated table statuses for {reservationsToUpdate.Count} reservations.");
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An error occurred while updating table statuses.");
      }

      try
      {
        await Task.Delay(_delayInterval, stoppingToken);
      }
      catch (TaskCanceledException)
      {
        // Task was canceled, break the loop
        break;
      }
    }
  }
}
