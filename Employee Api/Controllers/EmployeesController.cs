using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


[Route("api/[controller]")]
[ApiController]

public class EmployeesController : ControllerBase
{
    private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Designation = "Developer"},
            new Employee { Id = 2, FirstName = "David", LastName = "Bratt", Designation = "Designer"}
        };


    //GET: api/Employees
    [HttpGet]
    public ActionResult<IEnumerable<Employee>> Get()
    {
        return Ok(_employees);
    }

    //GET: api/Employees/id

    [HttpGet("{id}")]

    public ActionResult<Employee> GetById(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    //POST; api/Employees

    [HttpPost]
    public ActionResult<Employee> Post([FromBody] Employee newEmployee)
    {
        if(newEmployee == null)
        {
            return BadRequest("Invalid Request body.");
        }
        newEmployee.Id = _employees.Count + 1;
        _employees.Add(newEmployee);

        return CreatedAtAction(nameof(GetById), new { id = newEmployee.Id }, newEmployee);
    }

    //PUT: api/Employees/id
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Employee updatedEmployee)
    {
        if(updatedEmployee == null || id != updatedEmployee.Id)
        {
            return BadRequest("Invalid Request body or Unmatched ID's");
        }

        var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);

        if (existingEmployee == null)
        {
            return NotFound();
        }

        existingEmployee.FirstName = updatedEmployee.FirstName;
        existingEmployee.LastName = updatedEmployee.LastName;
        existingEmployee.Designation = updatedEmployee.Designation;

        return NoContent();
    }


    //DELETE: api/Employees/id

    [HttpDelete("{id}")]

    public IActionResult Delete(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);

        if (employee == null)
        {
            return NotFound();
        }
        _employees.Remove(employee);

        return NoContent();
    }
}

