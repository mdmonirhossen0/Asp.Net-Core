using MasterDetailsCrudCore.Models;
using MasterDetailsCrudCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MasterDetailsCrudCore.Controllers
{
    public class SalesController : Controller
    {
        SalesDBCore db = new SalesDBCore();
        [HttpGet]
        public IActionResult Index(int? id)
        {
            var item = new List<SaleMaster>
            {
                new SaleMaster {ProductType = "Shirt"},
                new SaleMaster {ProductType = "Pant"}
            };
            VmSale oSale = null;
            var oSM = db.SaleMasters.Where(x => x.SaleId == id).FirstOrDefault();
            if (oSM != null)
            {

                oSale = new VmSale();
                oSale.SaleId = oSM.SaleId;
                oSale.CreateDate = oSM.CreateDate;
                oSale.CustomerName = oSM.CustomerName;
                oSale.Gender = oSM.Gender;
                oSale.ProductType = oSM.ProductType;
                var listSaleDetail = new List<VmSale.VmSaleDetail>();
                var listSD = db.SaleDetails.Where(x => x.SaleId == id).ToList();
                foreach (var oSD in listSD)
                {
                    var oSaleDetail = new VmSale.VmSaleDetail();
                    oSaleDetail.SaleDetailId = oSD.SaleDetailId;

                    oSaleDetail.SaleId = oSD.SaleId;
                    oSaleDetail.ProductName = oSD.ProductName;
                    oSaleDetail.Price = (int)oSD.Price;
                    listSaleDetail.Add(oSaleDetail);
                }
                oSale.SaleDetails = listSaleDetail;
            }
            oSale = oSale == null ? new VmSale() : oSale;
            ViewData["List"] = db.SaleMasters.ToList();
            ViewData["ListD"] = db.SaleDetails.ToList();
            ViewData["Drop"] = new SelectList(item, "ProductType", "ProductType");
            return View(oSale);
        }
        [HttpPost]
        public ActionResult Index(VmSale vmSale)
        {
            var oSaleMaster = db.SaleMasters.Find(vmSale.SaleId);
            if (oSaleMaster == null)
            {
                oSaleMaster = new SaleMaster();
                oSaleMaster.CreateDate = vmSale.CreateDate;
                oSaleMaster.CustomerName = vmSale.CustomerName;
                oSaleMaster.Gender = vmSale.Gender;
                //oSaleMaster.ProductType = form["Selected"];
                db.SaleMasters.Add(oSaleMaster);
                var listSaleDetail = new List<SaleDetail>();
                for (var i = 0; i < vmSale.ProductName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(vmSale.ProductName[i]))
                    {
                        var oSaleDetail = new SaleDetail();
                        oSaleDetail.SaleId = oSaleMaster.SaleId;
                        oSaleDetail.ProductName = vmSale.ProductName[i];
                        oSaleDetail.Price = vmSale.Price[i];
                        listSaleDetail.Add(oSaleDetail);

                    }
                }
                db.SaleMasters.Add(oSaleMaster);
                db.SaleDetails.AddRange(listSaleDetail);
                db.SaveChanges();
            }
            else
            {
                oSaleMaster.CreateDate = vmSale.CreateDate;
                oSaleMaster.CustomerName = vmSale.CustomerName;
                oSaleMaster.Gender = vmSale.Gender;
                //oSaleMaster.ProductType = form["Selected"];
                var listSaleDetailRemove = db.SaleDetails.Where(x => x.SaleId ==
                vmSale.SaleId).ToList();
                var listSaleMasterRemove = db.SaleMasters.Find(vmSale.SaleId);
                db.SaleMasters.Remove(listSaleMasterRemove);
                db.SaleDetails.RemoveRange(listSaleDetailRemove);
                db.SaveChanges();
                var listSaleDetail = new List<SaleDetail>();
                for (var i = 0; i < vmSale.ProductName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(vmSale.ProductName[i]))
                    {
                        var oSaleDetail = new SaleDetail();
                        oSaleDetail.SaleId = oSaleMaster.SaleId;
                        oSaleDetail.ProductName = vmSale.ProductName[i];

                        oSaleDetail.Price = vmSale.Price[i];
                        listSaleDetail.Add(oSaleDetail);

                    }
                }
                db.SaleMasters.Add(oSaleMaster);
                db.SaleDetails.AddRange(listSaleDetail);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult IndexDelete(int id)
        {
            var oSaleMaster = (from s in db.SaleMasters where s.SaleId == id select s).FirstOrDefault();
            var oSaleDetail = (from d in db.SaleDetails where d.SaleId == id select d).ToList();
            if (oSaleDetail != null)
                db.SaleMasters.Remove(oSaleMaster);
            db.SaleDetails.RemoveRange(oSaleDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
