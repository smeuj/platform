using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nouwan.Smeuj.Api.Handlers;
using Nouwan.Smeuj.Domain;
using Nouwan.Smeuj.Framework;
using Serilog;

namespace Nouwan.Smeuj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : SmeujBaseController
    {
        private readonly IMediator mediator;
        private static readonly ILogger Logger = LoggerFactory.CreateLogger<MessagesController>();

        public MessagesController(IMediator mediator)
        {
            this.mediator = mediator;
            Logger.Debug("MessagesController;ctor;");
        }

        // POST api/<MessagesController> 
        [HttpPost]
        [Authorize]
        public async Task<ObjectResult> Post([FromBody] Message message)
        {
            if (message.IsSaved)
            {
                Logger.Information("Post; posted message already has an id and cannot" +
                                   " be inserted message.id {@messageId}", message.Id);
                return ReturnResult(Result<Message>.Unprocessable(message));
            }

            Logger.Debug("Post; Received message {@message}", message);
            var result = await mediator.Send(new AddMessageRequest(message));
            Logger.Information("Post; saved posted message with Id {@id}",result.Payload?.Id);

            return ReturnResult(result);
        }
    }
}
