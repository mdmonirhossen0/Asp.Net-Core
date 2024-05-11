using System.Collections.Generic;
using System.Data.Entity;

namespace MasterDetailsCrudCore.Models
{
    public class SalesDBCore:DbContext
    {
        public DbSet<SaleMaster> SaleMasters { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
    }
}
