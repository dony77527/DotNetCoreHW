using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeWork1.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController 
    {
        private readonly ContosoUniversityContext db;
        private readonly ContosoUniversityContextProcedures spdb;
        public DepartmentController(ContosoUniversityContext db, ContosoUniversityContextProcedures spdb)  : base(db, spdb)
        {
            this.spdb = spdb;
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Department.Where(m => !m.IsDeleted).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Department.Find(id);
        }

        [HttpPost("")]
        public async Task<ActionResult<Department>> PostDepartmentAsync(Department model)
        {
            await spdb.Department_Insert(model.Name, model.Budget, model.StartDate, model.InstructorId);            
            // db.Department.Add(model);
            // db.SaveChanges();
            return Created($"api/Department/{model.DepartmentId}", model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department model)
        {
            var record = db.Department.Find(id);
            if (record == null)
            {
                return NotFound();
            }
            await spdb.Department_Update(model.DepartmentId, model.Name, model.Budget, model.StartDate, model.InstructorId, model.RowVersion);
            // record.Name = model.Name;
            // record.Budget = model.Budget;
            // record.StartDate = model.StartDate;
            // record.InstructorId = model.InstructorId;
            // record.RowVersion = model.RowVersion;
            // db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartmentById(int id)
        {
           
            // 使用Store Procedure
            var record = db.Department.Find(id);
            if (record == null)
            {
                return NotFound();
            }
            await spdb.Department_Delete(record.DepartmentId, record.RowVersion);      

            // 假刪除 
            // return FakeDelete<Course>(id);

            // 使用一般操作      
            // db.Department.Remove(record);
            // db.SaveChanges();
            return Ok();
        }

        [HttpGet("DepartmentCourseCount")]
        public ActionResult<IEnumerable<VwDepartmentCourseCount>> GetDepartmentCourseCount()
        {
            //using Microsoft.EntityFrameworkCore;
            var list = db.VwDepartmentCourseCount.FromSqlRaw("SELECT * FROM [dbo].[vwDepartmentCourseCount]").ToList<VwDepartmentCourseCount>();
            return list;
        }
    }
}