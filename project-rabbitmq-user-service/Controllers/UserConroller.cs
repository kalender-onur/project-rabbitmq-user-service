using Application.Interfaces.AppUser;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace project_rabbitmq_user_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserConroller : ControllerBase
    {
        private readonly ILogger<UserConroller> _logger;
        private readonly IAppUserService _appUserService;
        public UserConroller(ILogger<UserConroller> logger, IAppUserService appSerice)
        {

            _logger = logger;
            _appUserService = appSerice;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AppUser appUser)
        {
            var result = await _appUserService.AddUserAsync(appUser);

            return Ok(result);
        }
    }
}
