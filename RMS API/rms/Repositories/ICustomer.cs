using System;
using Models.CustomerModel;
using Models.LoginModel;
namespace Repositories.CustomerRepository
{
    public interface ICustomer
    {
        public Customer AddCustomer(Customer customer);
        public Customer UpdateCustomer(int Id, Customer customer);
        public bool DeleteCustomer(int Id);
        public List<Customer> ListOfCustomers();
        public Customer GetCustomerById(int Id);
        public object LoginCustomer(Login login);
    }
}