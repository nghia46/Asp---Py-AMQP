using Asp_Service.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(ISendEndpointProvider sendEndpointProvider) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] HelloMessage message)
        {
            if (message == null || string.IsNullOrWhiteSpace(message.Text))
            {
                return BadRequest("Message text cannot be empty.");
            }

            // Specify the queue name where the message should be sent
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:hello-message-queue"));

            // Send the message to the specified queue
            await endpoint.Send(message);

            return Ok("Message Sent to RabbitMQ Queue 'hello-message-queue'");
        }
    }
}
