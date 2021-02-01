using MediatR;
using Nouwan.Smeuj.Domain;
using Nouwan.Smeuj.Framework;

namespace Nouwan.Smeuj.Api.Handlers
{
    public class AddMessageRequest:IRequest<Result<Message>>
    {
        public Message Message { get; }

        public AddMessageRequest(Message message)
        {
            Message = message;
        }
    }
}