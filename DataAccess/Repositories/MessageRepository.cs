using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Nouwan.Smeuj.Domain;
using Nouwan.Smeuj.Framework;
using Serilog;

[assembly: InternalsVisibleTo("DataAccess.Tests")]
namespace Nouwan.Smeuj.DataAccess.Repositories
{
    public interface IMessageRepository
    {
        Task<Result<Message>> Add(Message message, CancellationToken cancellationToken);
    }

    internal class MessageRepository : IMessageRepository
    {
        private static readonly ILogger Logger = LoggerFactory.CreateLogger<MessageRepository>();

        private readonly IDbConnectionFactory dbConnectionFactory;
        public MessageRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            Logger.Debug("ctor");
        }

        public async Task<Result<Message>> Add(Message message, CancellationToken cancellationToken)
        {
            Logger.Debug("Add; Adding message={@Message}", message);

            using var connection = await dbConnectionFactory.CreateAndOpenConnection();
            var id = await connection.QuerySingleAsync<int>(
                new CommandDefinition("INSERT INTO \"Messages\" (\"AuthorId\", \"SendOn\", \"MessageId\") VALUES(@AuthorId, @SendOn, @MessageId) RETURNING \"Id\"",
                    message, cancellationToken: cancellationToken)).ConfigureAwait(false);

            Logger.Information("Add; Inserted Id: {@Result}", id);
            return Result<Message>.Ok(message with { Id = id });
        }
    }
}
