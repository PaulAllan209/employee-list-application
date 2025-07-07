using AutoMapper;
using EmployeeListApplication.Core.Models;
using EmployeeListApplication.Core.Services.Interfaces;
using EmployeeListApplication.Server.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeListApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var userForRegistration = _mapper.Map<User>(userForRegistrationDto);

            var result = await _authenticationService.RegisterUser(userForRegistration, userForRegistrationDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authenticationService.ValidateUser(user.UserName, user.Password))
                return Unauthorized();

            return Ok(new { Token = await _authenticationService.CreateToken() });
        }
    }
}
