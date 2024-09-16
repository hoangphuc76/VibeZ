using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Package : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public int Number_of_acc { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User_package> Packages { get; set; }
        public virtual ICollection<Package_features> Features { get; set; }
    }
}
