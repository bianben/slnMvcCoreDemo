using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class CustomerController : SuperController
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult List(CKeywordViewModel vm)
        {
            IEnumerable<TCustomer> datas = null;
            DbDemoContext db = new DbDemoContext();
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from t in db.TCustomers
                        select t;
            else
                datas = db.TCustomers.Where(_ => _.FName.Contains(vm.txtKeyword) ||
                    _.FPhone.Contains(vm.txtKeyword) ||
                    _.FEmail.Contains(vm.txtKeyword) ||
                    _.FAddress.Contains(vm.txtKeyword));
            return View(datas);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TCustomer p)
        {
            DbDemoContext db = new DbDemoContext();
            db.TCustomers.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            DbDemoContext db = new DbDemoContext();
            var item = (from t in db.TCustomers
                        where t.FId == id
                        select t).FirstOrDefault();
            db.TCustomers.Remove(item);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            DbDemoContext db = new DbDemoContext();
            TCustomer x = db.TCustomers.FirstOrDefault(p => p.FId == id);
            if (x == null)
                return RedirectToAction("List");
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(TCustomer p)
        {
            DbDemoContext db = new DbDemoContext();
            var item = (from t in db.TCustomers
                        where t.FId == p.FId
                        select t).FirstOrDefault();
            if (item != null)
            {
                item.FId = p.FId;
                item.FName = p.FName;
                item.FEmail = p.FEmail;
                item.FAddress = p.FAddress;
                item.FPhone = p.FPhone;
                item.FPassword = p.FPassword;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
