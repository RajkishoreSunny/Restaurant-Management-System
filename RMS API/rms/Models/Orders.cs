using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models.CustomerModel;
using Models.MenuRepo;
namespace Models.OrdersModel
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set;} = 0;

        [ForeignKey("Customer")]
        public int CustomerId { get; set;} = 0;
        public DateTime OrderDate { get; set;}
        public decimal TotalPrice { get; set;} = 0;
        public int MenuId {get; set;} = 0;
        public int Quantity { get; set;}

        [JsonIgnore]
        public Customer? Customer { get; set;}

        [JsonIgnore]
        public Menu? Menu { get; set;}
    }
}