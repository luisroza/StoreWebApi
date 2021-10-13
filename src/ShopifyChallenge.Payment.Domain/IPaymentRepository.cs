using Store.Core.Data;

namespace Store.Payment.Domain
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);
        void AddTransaction(Transaction transaction);
    }
}
