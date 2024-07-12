using System;
namespace Models.CustomerModel
{
    public class Customer
    {
        public int CustomerId { get; set; } = 0;
        public string? CustomerName { get; set;}
        public string? CustomerEmail { get; set;}
        public string? Password { get; set;}
        public string? CustomerPhone { get; set;}
        public string? CustomerAddress { get; set;}
    }
}