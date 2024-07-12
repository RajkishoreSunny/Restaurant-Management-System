using System;
using System.ComponentModel.DataAnnotations;
using Models.ReservationModel;
using Models.TableModel;
namespace Models.ReservationTableModel
{
    public class ReservationTable 
    {
        [Key]
        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }
        public int TableId { get; set; }
        public Table? Table { get; set; }
    }
}