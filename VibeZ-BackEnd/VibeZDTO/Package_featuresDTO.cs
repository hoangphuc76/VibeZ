using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(PackId), nameof(FeatureId))]
    public class Package_featuresDTO : BaseEntity
    {
        public Guid PackId { get; set; }
        public Guid FeatureId { get; set; }
    }
}
