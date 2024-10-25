using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await PaymentDAO.Instance.GetAllPayments();
        }

        public async Task<Payment> GetPaymentById(Guid paymentId)
        {
            return await PaymentDAO.Instance.GetPaymentById(paymentId);
        }

        public async Task AddPayment(Payment payment)
        {
            await PaymentDAO.Instance.Add(payment);
        }

        public async Task UpdatePayment(Payment payment)
        {
            await PaymentDAO.Instance.Update(payment);
        }

        public async Task DeletePayment(Guid paymentId)
        {
            await PaymentDAO.Instance.Delete(paymentId);
        }
    }

}
