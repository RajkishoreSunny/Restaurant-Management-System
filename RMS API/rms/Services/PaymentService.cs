using Models.PaymentModel;
using Repositories.PaymentRepository;

namespace Services.PaymentSvc
{
    public class PaymentService
    {
        private readonly IPayment _payment;
        public PaymentService(IPayment payment)
        {
            _payment = payment;
        }
        public Payment UpdatePaymentStatus(Payment payment)
        {
            return _payment.UpdatePaymentStatus(payment);
        }
    }
}