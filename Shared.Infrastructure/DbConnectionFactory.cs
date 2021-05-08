using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Nouwan.SmeujPlatform.Shared.Infrastructure
{
    public interface IDbConnectionFactory<T>
    {
        Task<IDbConnection> CreateAndOpenConnection();
    }

    public class DbConnectionFactory<T>:IDbConnectionFactory<T>
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<DbConnectionFactory<T>> logger;

        protected DbConnectionFactory(IConfiguration configuration, ILogger<DbConnectionFactory<T>> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            logger.LogTrace("ctor;");
        }

        public async Task<IDbConnection> CreateAndOpenConnection()
        {
            var connection = new NpgsqlConnection(configuration.GetConnectionString(nameof(T)));
            await connection.OpenAsync().ConfigureAwait(false);

            logger.LogDebug("CreateAndOpenConnection; opened connection");
            return connection;
        }
    }
}
