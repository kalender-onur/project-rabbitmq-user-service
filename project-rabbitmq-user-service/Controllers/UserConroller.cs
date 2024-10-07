using Application.Interfaces.AppUser;
using Application.Interfaces.RabbitMq;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace project_rabbitmq_user_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserConroller : ControllerBase
    {
        private readonly ILogger<UserConroller> _logger;
        private readonly IAppUserService _appUserService;
        private readonly IConfiguration _configuration;
        private readonly IRabbitMqService _rabbitMqService;

        public UserConroller(ILogger<UserConroller> logger,
            IAppUserService appSerice,
            IConfiguration configuration,
            IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _appUserService = appSerice;
            _configuration = configuration;
            _rabbitMqService = rabbitMqService;

        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AppUser appUser)
        {
            try
            {
                var result = await _appUserService.AddUserAsync(appUser);

                _rabbitMqService.SendMessage(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(Result<bool>.FailureResult(ex.Message));
            }

        }
    }
}
