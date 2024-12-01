using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class User_package
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public virtual Package Package { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        public decimal total {  get; set; }
        [MaxLength(50)]
        public string PaymentMethod {  get; set; }
        [MaxLength(50)]
        public string TypeOfPremium { get; set; }

        public DateOnly Started_Day { get; set; }
        public DateOnly End_Day { get; set; }
    }
}
