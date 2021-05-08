using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Nouwan.SmeujPlatform.Messages.Domain;
using Nouwan.SmeujPlatform.Shared.Infrastructure;

[assembly: InternalsVisibleTo("DataAccess.Tests")]
namespace Nouwan.SmeujPlatform.Messages.Infrastructure
{
    public interface IMessageRepository
    {
        Task<Message> Add(Message message, CancellationToken cancellationToken);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly IDbConnectionFactory<Message> dbConnectionFactory;
        private readonly ILogger<MessageRepository> logger;

        public MessageRepository(IDbConnectionFactory<Message> dbConnectionFactory, ILogger<MessageRepository> logger)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.logger = logger;
            logger.LogTrace("ctor");
        }

        public async Task<Message> Add(Message message, CancellationToken cancellationToken)
        {
            logger.LogDebug("Add; Adding message={@Message}", message);

            int id;
            using (var connection = await dbConnectionFactory.CreateAndOpenConnection())
            {
                id = await connection.QuerySingleAsync<int>(
                    new CommandDefinition("INSERT INTO \"Messages\" (\"AuthorId\", \"SendOn\", \"MessageId\") VALUES(@AuthorId, @SendOn, @MessageId) RETURNING \"Id\"",
                        message, cancellationToken: cancellationToken)).ConfigureAwait(false);
            }

            logger.LogInformation("Add; Inserted Id: {@Result}", id);
            return message with {Id = id};
        }
    }
}
