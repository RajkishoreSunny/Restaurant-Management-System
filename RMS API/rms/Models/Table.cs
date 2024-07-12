using System;
using Models.ReservationModel;
using Models.ReservationTableModel;
namespace Models.TableModel
{
    public class Table
    {
        public int TableId { get; set; }
        public int SeatingCapacity { get; set; }
        public string? Status { get; set; }
        public ReservationTable? ReservationTables { get; set; }
        public ICollection<Reservation>? Reservation { get; set; }
    }
}