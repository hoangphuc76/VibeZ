using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(PackId),(nameof(UserId)))]
    public class User_package
    {
        public Guid PackId { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        public DateTime Started_Day { get; set; }
        public DateTime End_Day { get; set; }
    }
}
