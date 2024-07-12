using System;
using Models.OrdersModel;
using Repositories.OrderRepository;
namespace Services.OrderSvc
{
    public class OrderService
    {
        private readonly IOrder _order;
        public OrderService(IOrder order)
        {
            _order = order;
        }
        public List<Orders> ListOfOrders(int OrderId)
        {
            return _order.ListOfOrders(OrderId);
        }
        public Orders AddOrder(Orders order)
        {
            return _order.AddOrder(order);
        }
        public List<Orders> GetListOfOrdersForCustomer(int CustomerId)
        {
            return _order.GetListOfOrdersForCustomer(CustomerId);
        }
        public async Task<bool> SendOrderInvoiceEmail(OrdersDto orders, IFormFile formFile)
        {
            return await _order.SendOrderInvoiceEmail(orders, formFile);
        }
    }
}