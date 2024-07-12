using Models.PaymentModel;

namespace Repositories.PaymentRepository
{
    public interface IPayment
    {
        public Payment UpdatePaymentStatus(Payment payment);
    }
}