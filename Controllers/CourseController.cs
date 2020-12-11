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
    public class CourseController : BaseController 
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db) : base(db)
        {            
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
              return db.Course.Where(m => !m.IsDeleted).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            return db.Course.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Course> PostCourse(Course model)
        {
            db.Course.Add(model);
            db.SaveChanges();
            return Created($"api/course/{model.CourseId}", model);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, Course model)
        {
            var record = db.Course.Find(id);
            if(record == null){
                return NotFound();
            }
            record.Title = model.Title;
            record.Credits = model.Credits;
            record.DepartmentId = model.DepartmentId;
            db.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseById(int id)
        {
            // 假刪除 
            return FakeDelete<Course>(id);
            
            // var record = db.Course.Find(id);
            // if(record == null){
            //     return NotFound();
            // }            
            // db.Course.Remove(record);
            // db.SaveChanges();
            //return Ok();
        }

        [HttpGet("CourseStudents")]
        public ActionResult<IEnumerable<VwCourseStudents>> GetCourseStudents()
        {
            return db.VwCourseStudents.ToList();
        }


        [HttpGet("CourseStudentCount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetCourseStudentCount()
        {
            return db.VwCourseStudentCount.ToList();
        }
    }
}
