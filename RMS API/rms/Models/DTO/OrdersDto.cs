using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models.OrdersModel
{
    public class OrdersDto
    {
        [Key]
        public int OrderId { get; set;} = 0;

        [ForeignKey("Customer")]
        public int CustomerId { get; set;} = 0;
        public DateTime OrderDate { get; set;}
        public decimal TotalPrice { get; set;} = 0;
        public int MenuId {get; set;} = 0;
        public int Quantity { get; set;}
    }
}