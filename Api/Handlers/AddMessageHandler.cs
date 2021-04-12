using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nouwan.Smeuj.DataAccess.Repositories;
using Nouwan.Smeuj.Domain;
using Nouwan.Smeuj.Framework;
using Serilog;

namespace Nouwan.Smeuj.Api.Handlers
{
    internal class AddMessageHandler : IRequestHandler<AddMessageRequest, Result<Message>>
    {
        private static readonly ILogger Logger = LoggerFactory.CreateLogger<AddMessageHandler>();
        private readonly IMessageRepository messageRepository;

        public AddMessageHandler(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task<Result<Message>> Handle(AddMessageRequest request, CancellationToken cancellationToken)
        {
            Logger.Debug("Handle; Adding message {@message}", request.Message);
            var result = await messageRepository.Add(request.Message, cancellationToken);
            
            Logger.Information("Handle; Added message from request message id {@messageId}", result.Payload?.Id);
            return result;
        }
    }
}
