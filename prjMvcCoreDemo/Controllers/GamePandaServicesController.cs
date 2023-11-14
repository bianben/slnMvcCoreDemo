using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prjMvcCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePandaServicesController : ControllerBase
    {
        // GET: api/<GamePandaServicesController>
        [HttpGet]
        public IEnumerable<TProduct> Get()
        {
            DbDemoContext db = new DbDemoContext();
            var datas = from t in db.TProducts
                        select t;
            foreach(var t in datas)
            {
                t.FCost = 0;
                if (t.FQty > 100)
                    t.FQty = 100;
            }
            return datas;
        }

        // GET api/<GamePandaServicesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GamePandaServicesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GamePandaServicesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GamePandaServicesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
