using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Nouwan.Smeuj.Framework;
using Npgsql;
using Serilog;

namespace Nouwan.Smeuj.DataAccess
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateAndOpenConnection();
    }

    internal class DbConnectionFactory:IDbConnectionFactory
    {
        private static readonly ILogger Logger = LoggerFactory.CreateLogger<DbConnectionFactory>();

        private readonly IConfiguration configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
            Logger.Verbose("ctor;");
        }

        public async Task<IDbConnection> CreateAndOpenConnection()
        {
            var connection = new NpgsqlConnection(configuration.GetConnectionString("default"));
            await connection.OpenAsync().ConfigureAwait(false);

            Logger.Debug("CreateAndOpenConnection; Unprocessable and opened connection");
            return connection;
        }
    }
}
