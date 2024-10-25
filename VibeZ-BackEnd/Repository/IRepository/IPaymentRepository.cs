using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(Guid paymentId);
        Task AddPayment(Payment payment);
        Task UpdatePayment(Payment payment);
        Task DeletePayment(Guid paymentId);
    }

}
