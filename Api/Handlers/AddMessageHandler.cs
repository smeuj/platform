using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nouwan.Smeuj.DataAccess.Repositories;
using Nouwan.Smeuj.Domain;
using Nouwan.Smeuj.Framework;

namespace Nouwan.Smeuj.Api.Handlers
{
    public class AddMessageHandler:IRequestHandler<AddMessageRequest,Result<Message>>
    {
        private readonly IMessageRepository messageRepository;

        public AddMessageHandler(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task<Result<Message>> Handle(AddMessageRequest request, CancellationToken cancellationToken)
        {
            await messageRepository.Add(request.Message, cancellationToken);
            throw new NotImplementedException();
        }
    }
}
