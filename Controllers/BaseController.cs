using HomeWork1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork1.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        private readonly ContosoUniversityContextProcedures spdb;
        public BaseController(ContosoUniversityContext db)
        {
            this.db = db;
        }     
        public BaseController(ContosoUniversityContext db, ContosoUniversityContextProcedures spdb)
        {
            this.spdb = spdb;
            this.db = db;
        }
        public ActionResult<T> FakeDelete<T>(int id) where T : class, Models.EntityRule.IIsDelete
        {
            var record = this.db.Set<T>().Find(id);
            if (record == null)
            {
                return NotFound();
            }
            record.IsDeleted = true;
            this.db.SaveChanges();
            return Ok();
        }
    }
}