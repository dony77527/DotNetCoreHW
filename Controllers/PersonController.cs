using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeWork1.Models;

namespace HomeWork1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController 
    {
        private readonly ContosoUniversityContext db;
        public PersonController(ContosoUniversityContext db) : base(db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            
            return db.Person.Where(m => !m.IsDeleted).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.Person.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Person> PostPerson(Person model)
        {
            db.Person.Add(model);
            db.SaveChanges();
            return Created($"api/Person/{model.Id}", model);
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, Person model)
        {
            var record = db.Person.Find(id);
            if(record == null){
                return NotFound();
            }
            record.LastName = model.LastName;
            record.FirstName = model.FirstName;
            record.HireDate = model.HireDate;
            record.EnrollmentDate = model.EnrollmentDate;
            record.Discriminator = model.Discriminator;
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {              
            // 假刪除 
            return FakeDelete<Person>(id);
            
            // var record = db.Person.Find(id);
            // if(record == null){
            //     return NotFound();
            // }
            // db.Person.Remove(record);
            // db.SaveChanges();
            // return null;
        }
    }
}