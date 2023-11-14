using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class ShoppingController : SuperController
    {
        public IActionResult List()
        {
            DbDemoContext db = new DbDemoContext();
            var datas = from t in db.TProducts
                        select t;
            List<CProductWrap> list = new List<CProductWrap>();
            foreach (TProduct t in datas)
                list.Add(new CProductWrap() { product = t });

            return View(list);
        }
        public IActionResult CartView()
        {
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                return RedirectToAction("List");
            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
            if (cart == null) 
                return RedirectToAction("List");
            return View(cart);
        }

        public IActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FId = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(CAddToCartViewModel vm)
        {
            DbDemoContext db = new DbDemoContext();
            TProduct pDb = db.TProducts.FirstOrDefault(t => t.FId == vm.txtFId);
            if(pDb != null)
            {
                string json = "";
                List<CShoppingCartItem> cart = null;
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                {
                    json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                    cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
                }
                else
                    cart = new List<CShoppingCartItem>();
                CShoppingCartItem item = new CShoppingCartItem()
                {
                    price =(decimal)pDb.FPrice,
                    productId = vm.txtFId,
                    count = vm.txtCount,
                    product =pDb
                };
                cart.Add(item);
                json=JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST, json);
            }
            return RedirectToAction("List");
        }
    }
}
