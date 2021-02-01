using System.Collections.Generic;
using System.Linq;
using Dapper;
using Nouwan.Smeuj.Domain;
using Npgsql;
using NSubstitute;

namespace Nouwan.Smeuj.DataAccess.Tests
{
    internal static class DbTestHelper
    {
        public const string ConnectionString = "Server=localhost;Port=5432;Database=smeuj-test;User Id=postgres;Password=Secret01!;";
        public static int AddMessage(Message message)
        {
            Migrate.Run();
            using var connection = new NpgsqlConnection(ConnectionString);
            return connection.QuerySingle<int>(
                @"INSERT INTO ""Messages"" (""AuthorId"", ""SendOn"", ""MessageId"") VALUES(@AuthorId, @SendOn, @MessageId) RETURNING ""Id""",
                message);
        }

        public static IReadOnlyCollection<Message> GetAllMessages()
        {
            Migrate.Run();
            using var connection = new NpgsqlConnection(ConnectionString);
            return connection.Query<Message>(
                @"SELECT ""AuthorId"", ""SendOn"", ""MessageId"", ""Id"" FROM ""Messages""").ToArray();
        }

        public static IDbConnectionFactory GetConnectionFactory()
        {
            var dbConnectionFactory = Substitute.For<IDbConnectionFactory>();
            dbConnectionFactory.CreateAndOpenConnection().Returns(new NpgsqlConnection(ConnectionString));
            return dbConnectionFactory;
        }

        public static int AddAuthor(Author author)
        {
            Migrate.Run();
            using var connection = new NpgsqlConnection(ConnectionString);
            return connection.QuerySingle<int>(
                @"INSERT INTO ""Authors"" (""DiscordId"", ""Username"") VALUES(@DiscordId, @Username) RETURNING ""Id""", author);
        }

        public static void ClearDatabase()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Execute(@"DROP SCHEMA public CASCADE");
            connection.Execute(@"CREATE SCHEMA public");
            Migrate.Run();
        }
    }
}
