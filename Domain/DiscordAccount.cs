using System;

namespace Nouwan.Smeuj.Domain
{
    public class DiscordLogin
    {
        public DiscordLogin(int discordId, string email, DateTimeOffset createdOn, int id, int authorId)
        {
            DiscordId = discordId;
            Email = email;
            CreatedOn = createdOn;
            Id = id;
            AuthorId = authorId;
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int DiscordId  { get; set; }
        public string Email  { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}