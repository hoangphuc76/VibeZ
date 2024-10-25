using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class PackageDTO : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public int Number_of_acc { get; set; }
        public string Description { get; set; }
    }
}
