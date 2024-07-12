using Models.ReservationModel;
using Models.TableModel;

namespace Repositories.ReservationRepository
{
    public interface IReservationRepo
    {
        public List<Reservation> GetAllReservation();
        public Reservation AddReservation(Reservation reservation);
        public Reservation GetReservationById(int reservationId);
        public Table GetTableNumber(int seatingCapacity, string email);
    }
}