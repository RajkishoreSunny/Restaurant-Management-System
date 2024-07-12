using System;
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using Models.OrdersModel;
using MailKit.Security;
namespace Repositories.OrderRepository
{
    public class OrderRepository : IOrder
    {
        private readonly RMSDbContext _dbContext;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(RMSDbContext dbContext, ILogger<OrderRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Orders AddOrder(Orders order)
        {
            try
            {
                _dbContext.Orders?.Add(order);
                _dbContext.SaveChanges();
                var customerDetails = _dbContext.Customers?.FirstOrDefault(x => x.CustomerId == order.CustomerId);
                var ordererItem = _dbContext.Menu?.FirstOrDefault(x => x.MenuId == order.MenuId);
                if(customerDetails != null && ordererItem != null)
                {
                    SendOrderEmail(customerDetails.CustomerEmail, customerDetails.CustomerName, ordererItem.Name, order.Quantity);
                }
                else
                {
                    Console.WriteLine("Could Not Send");
                }
                return order;
            }
            catch
            {
                return null;
            }
        }

        public List<Orders> GetListOfOrdersForCustomer(int CustomerId)
        {
            try
            {
                var orderDetails = _dbContext.Orders.Where(o => o.CustomerId == CustomerId).ToList();
                return orderDetails;
            }
            catch
            {
                return null;
            }
        }

        public List<Orders> ListOfOrders(int OrderId)
        {
            try
            {
                var orderDetails = _dbContext.Orders.Where(o => o.OrderId == OrderId).ToList();
                return orderDetails;
            }
            catch
            {
                return null;
            }
        }

        #region Email
        private async Task SendOrderEmail(string recepientEmail, string customerName, string orderName, int quantity)
        {
            try
            {
                var senderEmail = "rajkishoresunny4590@gmail.com";
                var senderName = "Raj Restaurant";
                var subject = "Thank You For Ordering";
                var body = $"Dear {customerName},\n\nYour Order for {quantity} plate {orderName} has been recieved successfully. Our qualified Chefs will start preparing your order soon.\n\nThank You for Ordering. Your hot and delicious food will be soon out for delivery.\n\nRegards\nRaj Restaurant";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(recepientEmail, recepientEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = body;
                message.Body = bodyBuilder.ToMessageBody();


                using (var client = new MailKit.Net.Smtp.SmtpClient())
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
        public async Task<bool> SendOrderInvoiceEmail(OrdersDto orders, IFormFile formFile)
        {
            try
            {
                var customerDetails = _dbContext.Customers?.FirstOrDefault(x => x.CustomerId == orders.CustomerId);
                var ordererItem = _dbContext.Menu?.FirstOrDefault(x => x.MenuId == orders.MenuId);
                if(customerDetails != null && ordererItem != null)
                {
                    await SendInvoiceEmail(customerDetails.CustomerEmail, customerDetails.CustomerName, ordererItem.Name, orders.Quantity, formFile);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        private async Task SendInvoiceEmail(string recepientEmail, string customerName, string orderName, int quantity, IFormFile formFile)
        {
            try
            {
                var senderEmail = "rajkishoresunny4590@gmail.com";
                var senderName = "Raj Restaurant";
                var subject = "Thank You For Ordering";
                var body = $"Dear {customerName},\n\nPlease find below attached invoice for your order of {quantity} plate {orderName} as requested by you during our website visit. Thank you for ordering and enjoy your delicious meal. :)\n\nRegards\nRaj Restaurant";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(recepientEmail, recepientEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { TextBody = body };
                
                if(formFile == null || formFile.Length == 0)
                {
                    return;
                }

                using(var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    bodyBuilder.Attachments.Add(formFile.FileName, memoryStream.ToArray(), ContentType.Parse(formFile.ContentType));
                }

                message.Body = bodyBuilder.ToMessageBody();

                using(var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("rajkishoresunny4590@gmail.com", "hvrrfnstoioueeoc");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error");
                throw new Exception("Could not send Email");
            }
        }
        #endregion
    }
}