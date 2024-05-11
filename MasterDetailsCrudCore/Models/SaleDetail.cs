using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MasterDetailsCrudCore.Models
{
    public class SaleDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleDetailId { get; set; }
        [ForeignKey("SaleMaster")]
        public int? SaleId { get; set; }
        public virtual SaleMaster SaleMaster { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
    }
}
