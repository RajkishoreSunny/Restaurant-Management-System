using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.OrdersModel;
namespace Models.PaymentModel
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public Orders? Order{ get; set; }
    }
}