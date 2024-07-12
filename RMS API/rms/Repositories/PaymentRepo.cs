using Models.PaymentModel;
using Repositories.PaymentRepository;

namespace Repositories.PaymentRepo
{
    public class PaymentRepository : IPayment
    {
        private readonly RMSDbContext _dbContext;
        public PaymentRepository(RMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Payment UpdatePaymentStatus(Payment payment)
        {
            try
            {
                _dbContext.Payments.Add(payment);
                _dbContext.SaveChanges();
                return payment;
            }
            catch
            {
                return null;
            }
        }
    }
}