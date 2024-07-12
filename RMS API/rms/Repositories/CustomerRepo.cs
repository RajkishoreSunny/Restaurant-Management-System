using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MailKit.Net.Smtp;
using Models.CustomerModel;
using Models.LoginModel;
namespace Repositories.CustomerRepository
{
    public class CustomerRepo : ICustomer
    {
        private readonly RMSDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public CustomerRepo(RMSDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        #region CustomerCrud
        public Customer AddCustomer(Customer customer)
        {
            try
            {
                customer.Password = HashPassword(customer.Password);
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                SendRegistrationEmail(customer.CustomerEmail, customer.CustomerName).Wait();
                return customer;
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteCustomer(int Id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int Id)
        {
            try
            {
                var customer = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == Id);
                if(customer != null)
                {
                    return customer;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<Customer> ListOfCustomers()
        {
            try
            {
                return _dbContext.Customers.ToList();
            }
            catch
            {
                return null;
            }
        }

        public Customer UpdateCustomer(int Id, Customer customer)
        {
            try
            {
                var updateCustomer = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == Id);
                if(updateCustomer != null)
                {
                    updateCustomer.CustomerName = customer.CustomerName;
                    updateCustomer.CustomerEmail = customer.CustomerEmail;
                    updateCustomer.CustomerPhone = customer.CustomerPhone;
                    updateCustomer.CustomerAddress = customer.CustomerAddress;
                    updateCustomer.Password = HashPassword(customer.Password);
                    _dbContext.SaveChanges();
                    return updateCustomer;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Authentication
        public object LoginCustomer(Login login)
        {
            try
            {
                var response = _dbContext.Customers.FirstOrDefault(x => x.CustomerEmail == login.Email);
                if (response != null)
                {
                    var authenticUser = VerifyPassword(login.Password, response.Password);
                    var customer = new { response.CustomerId, response.CustomerEmail, response.CustomerPhone, response.CustomerName, response.CustomerAddress };
                    if(authenticUser == true)
                    {
                        var token = GenerateJwtToken(login.Email);
                        return new { Response = customer, Token = token };
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private string HashPassword(string Password)
        {
            using (var sha56 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);
                byte[] hashBytes = sha56.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }
        private bool VerifyPassword(string Password, string PasswordHash)
        {
            string hashedInputPassword = HashPassword(Password);
            return hashedInputPassword == PasswordHash;
        }
        private string GenerateJwtToken(string Email)
        {
            var jwtSecretKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, Email)
            };

            var token = new JwtSecurityToken(
                jwtIssuer,
                jwtAudience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Email
        private async Task SendRegistrationEmail(string recepientEmail, string customerName)
        {
            try
            {
                var senderEmail = "rajkishoresunny4590@gmail.com";
                var senderName = "Raj Restaurant";
                var subject = "Welcome to Raj Restaurant";
                var body = $"Dear {customerName},\n\nWelcome to our family of Raj restaurant where we serve taste, quality and tradition on your table.\n\nThank You for Registering and please visit our outlets to try our authentic cuisines.\n\nRegards\nRaj Restaurant";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress(recepientEmail, recepientEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
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
        #endregion
    }
}
