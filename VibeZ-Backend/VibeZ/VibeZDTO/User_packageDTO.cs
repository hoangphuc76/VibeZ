using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(PackId),(nameof(UserId)))]
    public class User_packageDTO
    {
        public Guid PackId { get; set; }
        public Guid UserId { get; set; }
        public decimal total { get; set; }
        public  string PaymentMethod { get; set; }
        public string TypeOfPremium { get; set; }

        public DateTime Started_Day { get; set; }
        public DateTime End_Day { get; set; }
    }
}
