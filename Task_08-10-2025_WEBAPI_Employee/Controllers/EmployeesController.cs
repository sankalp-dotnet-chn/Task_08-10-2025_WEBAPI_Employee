using Microsoft.AspNetCore.Mvc;
using Task_08_10_2025_WEBAPI_Employee.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task_08_10_2025_WEBAPI_Employee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Emp1", Department = "HR", MobileNo = 9000000000, Email = "emp1@test.com" },
            new Employee { Id = 2, Name = "Emp2", Department = "IT", MobileNo = 9090909090, Email = "emp2@test.com" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            return Ok(_employees);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Employee> GetEmployeeById([FromRoute] int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

        [HttpGet("bydept")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByDept([FromQuery] string department)
        {
            if (string.IsNullOrWhiteSpace(department))
                return BadRequest("Query parameter 'department' is required.");

            var list = _employees.Where(e => string.Equals(e.Department, department, System.StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(list);
        }

        [HttpPost]
        public ActionResult<Employee> AddEmployee([FromBody] Employee newEmployee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_employees.Any(e => e.Id == newEmployee.Id))
                return Conflict($"Employee with Id {newEmployee.Id} already exists.");

            _employees.Add(newEmployee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateEmployee([FromRoute] int id, [FromBody] Employee updatedEmployee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();

            emp.Name = updatedEmployee.Name;
            emp.Department = updatedEmployee.Department;
            emp.MobileNo = updatedEmployee.MobileNo;
            emp.Email = updatedEmployee.Email;

            return Ok();
        }

        [HttpPatch("{id:int}/email")]
        public ActionResult UpdateEmployeeEmail([FromRoute] int id, [FromBody] EmailUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();

            emp.Email = dto.Email;
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteEmployee([FromRoute] int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();
            _employees.Remove(emp);
            return Ok();
        }

        [HttpHead("{id:int}")]
        public IActionResult HeadEmployee([FromRoute] int id)
        {
            var exists = _employees.Any(e => e.Id == id);
            if (!exists) return NotFound();
            return Ok(); 
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,PATCH,DELETE,HEAD,OPTIONS");
            return Ok();
        }
    }
}