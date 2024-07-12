using System;
using Models.OrdersModel;
namespace Repositories.OrderRepository
{
    public interface IOrder
    {
        public List<Orders> ListOfOrders(int OrderId);
        public Orders AddOrder(Orders orders); 
        public List<Orders> GetListOfOrdersForCustomer(int CustomerId);
        public Task<bool> SendOrderInvoiceEmail(OrdersDto order, IFormFile formFile);
    }
}
