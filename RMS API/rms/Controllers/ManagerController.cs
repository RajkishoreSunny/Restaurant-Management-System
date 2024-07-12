using Services.ManagerSvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Manager;
using Models.LoginModel;
using Repositories.CustomerRepository;
using Services.CustomerService;
namespace Controller.ManagerCtrl
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerService _managerService;
        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost("UpdateManager")]
        [Authorize]
        public ActionResult<Manager> UpdateManager(int Id, [FromBody]Manager manager)
        {
            try
            {
                var response = _managerService.UpdateManager(Id, manager);
                if(response != null)
                {
                    return Ok(response);
                }
                return Unauthorized();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("LoginManager")]
        [EnableCors("AllowOrigin")]
        public ActionResult<object> LoginManager(Login login)
        {
            try
            {
                var response = _managerService.LoginManager(login);
                if (response != null)
                {
                    return Ok(response);
                }
                return Unauthorized("Not Authorized");
            }
            catch
            {
                return Unauthorized("Not Authorized");
            }
        }
    }
}