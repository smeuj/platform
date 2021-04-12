using System;
using Nouwan.Smeuj.Domain;

namespace Nouwan.Smeuj.UnitTests.Common
{
    public static class TestDataFactory
    {
        public static Message CreateMessage(int authorId = 1, DateTime? sentOn = null, int messageId = 1, int id = 0) =>
            new(authorId, sentOn ?? DateTimeOffset.Now.ToLocalTime().UtcDateTime, id);

        public static Author CreateAuthor(int id = 0, string username = "someUsername", int discordId = 2) => new Author(id, username, discordId);
    }
}
