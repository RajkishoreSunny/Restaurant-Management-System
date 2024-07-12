using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.PaymentModel;
using Services.PaymentSvc;

namespace Controller.PaymentCtrl
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _payment;
        public PaymentController(PaymentService payment)
        {
            _payment = payment;
        }

        [HttpPost("UpdatePaymentStatus")]
        public ActionResult<bool> UpdatePaymentStatus(Payment payment)
        {
            try
            {
                var paymentStatus = _payment.UpdatePaymentStatus(payment);
                if(paymentStatus != null)
                {
                    return Ok(true);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }

}