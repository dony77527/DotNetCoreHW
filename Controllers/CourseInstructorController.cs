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
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseInstructorController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors()
        {
            return db.CourseInstructor.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int id)
        {
            return db.CourseInstructor.Find(id);
        }

        [HttpPost("")]
        public ActionResult<CourseInstructor> PostCourseInstructor(CourseInstructor model)
        {
            db.CourseInstructor.Add(model);
            db.SaveChanges();
            return Created($"api/CourseInstructor/", model);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourseInstructor(int id, CourseInstructor model)
        {
            var record = db.CourseInstructor.Find(id);
            if(record == null){
                return NotFound();
            }
            record.CourseId = model.CourseId;
            record.InstructorId = model.InstructorId;
            db.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public ActionResult<CourseInstructor> DeleteCourseInstructorById(int id)
        {
            var record = db.CourseInstructor.Find(id);
            if(record == null){
                return NotFound();
            }
            db.CourseInstructor.Remove(record);
            db.SaveChanges();
            return null;
        }
    }
}