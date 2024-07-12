using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.CustomerModel;
using Models.LoginModel;
using Repositories.CustomerRepository;
using Services.CustomerService;
namespace Controller.CustomerCnt
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerSvc _customerSvc;
        public CustomerController(CustomerSvc customerSvc)
        {
            _customerSvc = customerSvc;
        }

        [HttpGet("GetCustomerById")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            try
            {
                var customer = _customerSvc.GetCustomerById(id);
                if (customer != null)
                {
                    return Ok(customer);
                }
                return BadRequest();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("ListOfCustomer")]
        public ActionResult<List<Customer>> ListOfCustomers()
        {
            try
            {
                var customerList = _customerSvc.ListOfCustomers();
                if (customerList != null)
                {
                    return Ok(customerList);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddCustomer")]
        public ActionResult<Customer> AddCustomer(Customer customer)
        {
            try
            {
                var response = _customerSvc.AddCustomer(customer);
                if (response != null)
                {
                    return Ok(response);
                }
                return StatusCode(400);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpPut("UpdateCustomer")]
        public ActionResult<Customer> UpdateCustomer(int Id, Customer customer)
        {
            try
            {
                var updateCustomer = _customerSvc.UpdateCustomer(Id, customer);
                if (updateCustomer != null)
                {
                    return Ok(updateCustomer);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("LoginUser")]
        [EnableCors("AllowOrigin")]
        public ActionResult<object> LoginUser(Login login)
        {
            try
            {
                var response = _customerSvc.LoginCustomer(login);
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