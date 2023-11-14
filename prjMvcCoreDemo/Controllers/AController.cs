using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string json2obj()
        {
            string json=obj2json();
            TCustomer x=JsonSerializer.Deserialize<TCustomer>(json);
            return x.FName + " / " + x.FEmail;
        }
        public string obj2json()
        {
            TCustomer x = new TCustomer()
            {
                FId = 1,
                FName = "John",
                FPhone = "0980855331",
                FEmail = "john@gmail.com",
                FAddress = "Taipei",
                FPassword = "1234"
            };
            string json=JsonSerializer.Serialize(x);
            return json;
        }
        public IActionResult DemoCountBySession()
        {
            int count = 0;
            if (HttpContext.Session.Keys.Contains("COUNT"))
                count = (int)HttpContext.Session.GetInt32("COUNT");
            count++;
            HttpContext.Session.SetInt32("COUNT",count);
            ViewBag.COUNT = count;
            return View();
        }
    }
}
