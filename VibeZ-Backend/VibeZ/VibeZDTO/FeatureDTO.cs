using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class FeatureDTO : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Package_features> PackageFeatures { get; set; }
    }
}
