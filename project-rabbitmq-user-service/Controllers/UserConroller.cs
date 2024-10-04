using Application.Interfaces.AppUser;
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

        public UserConroller(ILogger<UserConroller> logger, IAppUserService appSerice, IConfiguration configuration)
        {
            _logger = logger;
            _appUserService = appSerice;
            _configuration = configuration;

        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AppUser appUser)
        {
            try
            {
                var result = await _appUserService.AddUserAsync(appUser);

                string connectionString = _configuration.GetValue<string>("RabbitMqConnectionString:DefaultConnection");

                ConnectionFactory factory = new ConnectionFactory();
              
                factory.Uri = new Uri(connectionString);
                factory.ClientProvidedName = "Project RabbitMq User Service";

                IConnection cnn = factory.CreateConnection();
                IModel channel = cnn.CreateModel();

                string exchangeName = "DemoExchange";
                string routingKey = "demo-rouiting-key";
                string queueName = "DemoQueue";

                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
                channel.QueueDeclare(queueName, false, false, false, null);
                channel.QueueBind(queueName, exchangeName, routingKey, null);

                byte[] messageBodyBytes = Encoding.UTF8.GetBytes(result.Message);
                channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

                channel.Close();
                cnn.Close();

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
