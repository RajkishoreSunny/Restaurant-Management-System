using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.CustomerModel;
using Models.ReservationTableModel;
using Models.TableModel;
namespace Models.ReservationModel
{
    public class Reservation
    {
        public int ReservationId { get; set;}

        [ForeignKey("Customer")]
        public int CustomerId { get; set;}
        public DateTime ReservationDateTime { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Status { get; set; }
        public int? TableId { get; set; }
        public Customer? Customer { get; set; }
        public Table? Table{ get; set; }
        public ReservationTable? ReservationTable { get; set; }
    }
}