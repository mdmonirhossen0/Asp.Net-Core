using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MasterDetailsCrudCore.Models
{
    public class SaleMaster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public DateTime? CreateDate { get; set; }
        [DisplayName("Full Name")]
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public string ProductType { get; set; }
        public virtual IList<SaleDetail> SaleDetails { get; set; }
    }
}
