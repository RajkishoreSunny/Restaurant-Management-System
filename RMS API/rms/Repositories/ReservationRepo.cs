using Models.ReservationModel;
using Models.TableModel;
using MimeKit;
using MailKit.Net.Smtp;

namespace Repositories.ReservationRepository
{
    public class ReservationRepository : IReservationRepo
    {
        private readonly RMSDbContext _dbContext;

        public ReservationRepository(RMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Reservation AddReservation(Reservation reservation)
        {
            try
            {
                _dbContext.Add(reservation);
                _dbContext.SaveChanges();
                return reservation;
            }
            catch
            {
                return null;
            }
        }

        public List<Reservation> GetAllReservation()
        {
            try
            {
                var reservations = _dbContext.Reservations.ToList();
                return reservations;
            }
            catch
            {
                return null;
            }
        }

        public Reservation GetReservationById(int reservationId)
        {
            try
            {
                var reservation = _dbContext.Reservations.FirstOrDefault(x => x.ReservationId == reservationId);
                return reservation;
            }
            catch
            {
                return null;
            }
        }

        public Table GetTableNumber(int seatingCapacity, string email)
        {
            try
            {
                var table = _dbContext.Tables.FirstOrDefault(t => t.SeatingCapacity == seatingCapacity);      
                SendTableBookingMail(email, table.TableId);
                return table;
            }
            catch
            {
                return null;
            }
        }

        private async Task SendTableBookingMail(string recepientEmail, int tableNo)
        {
            try
            {
                var recepient = _dbContext.Customers?.FirstOrDefault(x => x.CustomerEmail == recepientEmail);
                var senderEmail = "rajkishoresunny4590@gmail.com";
                var senderName = "Raj Restaurant";
                var subject = "Table Booking Confirmation";
                var body = $"Dear {recepient?.CustomerName},\n\nYour request for table booking has been successfully approved. Your assigned table is Table Number {tableNo}.\n\nThank You for Booking.\n\nRegards\nRaj Restaurant";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(recepientEmail, recepientEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("rajkishoresunny4590@gmail.com", "hvrrfnstoioueeoc");

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            }
            catch
            {
                throw new Exception("Could not send Email");
            }
        }
    }
}