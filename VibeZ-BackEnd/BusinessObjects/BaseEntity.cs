using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public abstract class BaseEntity
    {
        public DateOnly CreateDate { get; set; }
        public DateOnly UpdateDate { get; set; }
    }
}
