using AutoMapper;
using EmployeeListApplication.Core.Models;
using EmployeeListApplication.Core.Services.Interfaces;
using EmployeeListApplication.Server.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeListApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreateDto employeeDto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var employeeEntity = _mapper.Map<Employee>(employeeDto);

            var employeeEntityReturned = await _employeeService.CreateEmployeeAsync(employeeEntity);

            var employeeDtoToReturn = _mapper.Map<EmployeeForGetDto>(employeeEntityReturned);

            return Ok(employeeDtoToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeeEntities = await _employeeService.GetAllEmployeesAsync(trackChanges: false);

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeForGetDto>>(employeeEntities);

            return Ok(employeeDtos);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest(("Employee ID cannot be empty."));

            // Validate GUIDs for ID to prevent unnecessary DB calls
            if (!Guid.TryParse(employeeId, out _))
                return BadRequest("Invalid Employee ID format.");

            var employeeEntity = await _employeeService.GetEmployeeByIdAsync(employeeId, trackChanges: false);
            var employeeDto = _mapper.Map<EmployeeForGetDto>(employeeEntity);

            return Ok(employeeDto);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest("Employee ID cannot be empty.");

            // Validate GUIDs for ID to prevent unnecessary DB calls
            if (!Guid.TryParse(employeeId, out _))
                return BadRequest("Invalid Employee ID format.");

            await _employeeService.DeleteEmployeeAsync(employeeId, trackChanges: true);

            return NoContent();
        }
    }
}
