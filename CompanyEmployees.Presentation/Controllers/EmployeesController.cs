using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, EmployeeParameters employeeParameters)
        {
            var pagedResult = await _service.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.employees);
        }

        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, id, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");
            
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee, compTrackChanges: false, empTrackChanges: true);
            return NoContent();
        }
    }
}
