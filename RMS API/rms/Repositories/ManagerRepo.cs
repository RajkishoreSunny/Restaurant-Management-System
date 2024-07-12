using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models.LoginModel;
using Models.Manager;

namespace Repositories.ManagerRepository
{
    public class ManagerRepo : IManager
    {
        private readonly RMSDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ManagerRepo(RMSDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        #region ManagerCrud
        public Manager UpdateManager(int Id, Manager manager)
        {
            try
            {
                var availableManager = _dbContext.Managers.FirstOrDefault(m => m.ManagerId == Id);
                if (availableManager != null)
                {
                    availableManager.ManagerName = manager.ManagerName;
                    availableManager.ManagerPassword = HashPassword(manager.ManagerPassword);
                    availableManager.ManagerEmail = manager.ManagerEmail;
                    availableManager.Branch = manager.Branch;
                }
                _dbContext.SaveChanges();
                return availableManager;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Authorization
        public object LoginManager(Login login)
        {
            try
            {
                var response = _dbContext.Managers.FirstOrDefault(x => x.ManagerEmail == login.Email);
                if (response != null)
                {
                    var authenticUser = VerifyPassword(login.Password, response.ManagerPassword);
                    var manager = new { response.ManagerId, response.ManagerEmail, response.ManagerName };
                    if(authenticUser == true)
                    {
                        var token = GenerateJwtToken(login.Email);
                        return new { Response = manager, Token = token };
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
    }
}