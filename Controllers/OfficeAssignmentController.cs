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
    public class OfficeAssignmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public OfficeAssignmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<OfficeAssignment>> GetOfficeAssignments()
        {
            return db.OfficeAssignment.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<OfficeAssignment> GetOfficeAssignmentById(int id)
        {
            return db.OfficeAssignment.Find(id);
        }

        [HttpPost("")]
        public ActionResult<OfficeAssignment> PostOfficeAssignment(OfficeAssignment model)
        {
            db.OfficeAssignment.Add(model);
            db.SaveChanges();
            return Created("api/OfficeAssignment/", model);
        }

        [HttpPut("{id}")]
        public IActionResult PutOfficeAssignment(int id, OfficeAssignment model)
        {
            var record = db.OfficeAssignment.Find(id);
            if(record == null){
                return NotFound();
            }
            record.InstructorId = model.InstructorId;
            record.Location = model.Location;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<OfficeAssignment> DeleteOfficeAssignmentById(int id)
        {
            var record = db.OfficeAssignment.Find(id);
            if(record == null){
                return NotFound();
            }
            db.OfficeAssignment.Remove(record);
            db.SaveChanges();
            return null;
        }
    }
}