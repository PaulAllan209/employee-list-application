using AutoMapper;
using Azure;
using EmployeeListApplication.Core.Models;
using EmployeeListApplication.Core.Services.Interfaces;
using EmployeeListApplication.Server.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeeEntities = await _employeeService.GetAllEmployeesAsync(trackChanges: false);

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeForGetDto>>(employeeEntities);

            return Ok(employeeDtos);
        }

        [HttpGet("{employeeId}")]
        [Authorize]
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

        [HttpPatch("{employeeId}")]
        [Authorize]
        public async Task<IActionResult> PatchEmployee(string employeeId, [FromBody] JsonPatchDocument<EmployeeForPatchDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch document cannot be null.");

            if (string.IsNullOrEmpty(employeeId))
                return BadRequest("Employee ID cannot be empty.");

            if (!Guid.TryParse(employeeId, out _))
                return BadRequest("Invalid Employee ID format.");

            var employeeToUpdateEntity = await _employeeService.GetEmployeeByIdAsync(employeeId, trackChanges: true);

            if (employeeToUpdateEntity == null)
                return NotFound();

            // Map entity to DTO
            var employeeForPatchDto = _mapper.Map<EmployeeForPatchDto>(employeeToUpdateEntity);

            // Apply the patch
            patchDoc.ApplyTo(employeeForPatchDto, error =>
            {
                ModelState.AddModelError(error.AffectedObject?.ToString() ?? "", error.ErrorMessage);
            });

            // IMPORTANT: After applying the patch, ModelState may contain errors from invalid patch operations.
            // However, ModelState does NOT automatically validate data annotations on the patched DTO.
            // If you want to ensure data annotation validation, call TryValidateModel(shipmentDto) here.
            // Example: TryValidateModel(shipmentDto);

            // This check ensures that no invalid data (from patch ops or model validation) is persisted.
            // If ModelState is invalid, return 422 Unprocessable Entity with error details
            TryValidateModel(employeeForPatchDto);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            // Map DTO back to entity
            _mapper.Map(employeeForPatchDto, employeeToUpdateEntity);

            // This function call just calls save changes in the repository layer
            await _employeeService.UpdateEmployeeAsync();

            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        [Authorize]
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
