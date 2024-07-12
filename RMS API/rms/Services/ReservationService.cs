using Models.ReservationModel;
using Models.TableModel;
using Repositories.ReservationRepository;

namespace Services.ReservationSvc
{
    public class ReservationService
    {
        private readonly IReservationRepo _reservationRepo;

        public ReservationService(IReservationRepo reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }
        public List<Reservation> GetAllReservation()
        {
            return _reservationRepo.GetAllReservation();
        }
        public Reservation AddReservation(Reservation reservation)
        {
            return _reservationRepo.AddReservation(reservation);
        }
        public Reservation GetReservationById(int reservationId)
        {
            return _reservationRepo.GetReservationById(reservationId);
        }
        public Table GetTableNumber(int seatingCapacity, string email)
        {
            return _reservationRepo.GetTableNumber(seatingCapacity, email);
        }
    }
}