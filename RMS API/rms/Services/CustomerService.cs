using System;
using Models.CustomerModel;
using Models.LoginModel;
using Repositories.CustomerRepository;
namespace Services.CustomerService
{
    public class CustomerSvc
    {
        private readonly ICustomer _customer;

        public CustomerSvc(ICustomer customer)
        {
            _customer = customer;
        }
        public Customer AddCustomer(Customer customer)
        {
            return _customer.AddCustomer(customer);
        }
        public Customer GetCustomerById(int Id)
        {
            return _customer.GetCustomerById(Id);
        }
        public List<Customer> ListOfCustomers()
        {
            return _customer.ListOfCustomers();
        }
        public Customer UpdateCustomer(int Id, Customer customer)
        {
            return _customer.UpdateCustomer(Id, customer);
        }
        public object LoginCustomer(Login login)
        {
            return _customer.LoginCustomer(login);
        }
    }
}