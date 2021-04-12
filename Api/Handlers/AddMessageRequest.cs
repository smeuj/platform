using MediatR;
using Nouwan.Smeuj.Domain;
using Nouwan.Smeuj.Framework;

namespace Nouwan.Smeuj.Api.Handlers
{
    internal class AddMessageRequest:IRequest<Result<Message>>
    {
        public Message Message { get; }

        public AddMessageRequest(Message message)
        {
            Message = message;
        }
    }
}