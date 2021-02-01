using System;

namespace Nouwan.Smeuj.Domain
{
    public class Author
    {
        public Author(int id, string username, int discordId)
        {
            Id = id;
            Username = username;
            DiscordId = discordId;
        }

        public int Id { get; }
        
        public int? DiscordId { get; set; }

        public string Username { get; set; }

        public void UpdateUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(nameof(userName))) throw new InvalidOperationException();
            Username = userName;
        }
    }
}