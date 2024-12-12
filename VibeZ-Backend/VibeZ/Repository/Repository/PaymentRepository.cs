using BusinessObjects;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly VibeZDbContext _context;
        public PaymentRepository(VibeZDbContext context) : base(context)
        {
            _context = context;
        }
    
    }

}
