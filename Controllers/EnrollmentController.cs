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
    public class EnrollmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public EnrollmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments()
        {
            return db.Enrollment.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollmentById(int id)
        {
            return db.Enrollment.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Enrollment> PostEnrollment(Enrollment model)
        {
            db.Enrollment.Add(model);
            db.SaveChanges();
            return Created($"api/Enrollment/{model.EnrollmentId}", model);
        }

        [HttpPut("{id}")]
        public IActionResult PutEnrollment(int id, Enrollment model)
        {
            var record = db.Enrollment.Find(id);
            if(record == null){
                return NotFound();
            }
            record.CourseId = model.CourseId;
            record.StudentId = model.StudentId;
            record.Grade = model.Grade;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Enrollment> DeleteEnrollmentById(int id)
        {
            var record = db.Enrollment.Find(id);
            if(record == null){
                return NotFound();
            }
            db.Enrollment.Remove(record);
            db.SaveChanges();
            return Ok();
        }
    }
}