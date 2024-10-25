using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PaymentDAO : SingletonBase<PaymentDAO>
    {
        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetPaymentById(Guid paymentId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
            if (payment == null) return null;
            return payment;
        }

        public async Task Add(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Payment payment)
        {
            var existingItem = await GetPaymentById(payment.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(payment);
            }
            else
            {
                _context.Payments.Add(payment);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid paymentId)
        {
            var payment = await GetPaymentById(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }

}
