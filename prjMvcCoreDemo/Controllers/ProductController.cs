using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        public ProductController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(CKeywordViewModel vm)
        {
            IEnumerable<TProduct> datas = null;
            DbDemoContext db = new DbDemoContext();
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from t in db.TProducts
                        select t;
            else
                datas = db.TProducts.Where(_ => _.FName.Contains(vm.txtKeyword));

            return View(datas);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TProduct p)
        {
            DbDemoContext db = new DbDemoContext();
            db.TProducts.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            DbDemoContext db = new DbDemoContext();
            var item = (from t in db.TProducts
                        where t.FId == id
                        select t).FirstOrDefault();
            db.TProducts.Remove(item);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            DbDemoContext db = new DbDemoContext();
            TProduct x = db.TProducts.FirstOrDefault(p => p.FId == id);
            if (x == null)
                return RedirectToAction("List");
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(TProduct p)
        {
            DbDemoContext db = new DbDemoContext();
            var item = (from t in db.TProducts
                        where t.FId == p.FId
                        select t).FirstOrDefault();
            //if (p.photo != null)
            //{
            //    string photoName = Guid.NewGuid().ToString() + ".jpg";
            //    item.fImagePath = photoName;
            //    p.photo.SaveAs(Server.MapPath("../../Images/" + photoName));
            //}
            if (item != null)
            {
                item.FId = p.FId;
                item.FName = p.FName;
                item.FQty = p.FQty;
                item.FCost = p.FCost;
                item.FPrice = p.FPrice;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
