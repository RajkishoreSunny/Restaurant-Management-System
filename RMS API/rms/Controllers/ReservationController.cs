using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.ReservationModel;
using Models.TableModel;
using Services.ReservationSvc;

namespace Controller.ReservationCtrl
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _rservice;

        public ReservationController(ReservationService rservice)
        {
            _rservice = rservice;
        }
        [HttpGet("GetAllReservation")]
        public ActionResult<List <Reservation>> GetAllReservation()
        {
            try
            {
                var listOfReservation = _rservice.GetAllReservation();
                return Ok(listOfReservation);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("AddReservation")]
        public ActionResult<Reservation> AddReservation(Reservation reservation)
        {
            try
            {
                var reservationToAdd = _rservice.AddReservation(reservation);
                return Ok(reservationToAdd);
            }
            catch
            {
                return BadRequest("Could Not Add");
            }
        }

        [HttpGet("GetReservationById")]
        public ActionResult<Reservation> GetReservationById(int Id)
        {
            try
            {
                var reservation = _rservice.GetReservationById(Id);
                return Ok(reservation);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("GetTableNumber")]
        public ActionResult<Table> GetTableNumber(int seatingCapacity, string email)
        {
            try
            {
                var table = _rservice.GetTableNumber(seatingCapacity, email);
                if(table != null)
                {
                    return Ok(table);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}