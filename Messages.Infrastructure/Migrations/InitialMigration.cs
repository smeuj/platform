using FluentMigrator;

namespace Nouwan.SmeujPlatform.Messages.Infrastructure.Migrations
{
    [Migration(0)]
    public class InitialMigration:Migration
    {
        public override void Up()
        {
            Create.Table("Authors")
                .WithColumn("Id").AsInt64().Identity().NotNullable().Unique()
                .WithColumn("DiscordId").AsInt64().Indexed()
                .Unique("IX_Author_DiscordId")
                .WithColumn("Username").AsAnsiString().NotNullable();
            
            Create.Table("Messages")
                .WithColumn("Id").AsInt64().Identity().NotNullable().Unique()
                .WithColumn("AuthorId").AsInt64().ForeignKey("FK_Author_Id_Message_AuthorId","public","Authors","Id").NotNullable()
                .WithColumn("SendOn").AsDateTime2().NotNullable()
                .WithColumn("MessageId").AsInt64().NotNullable().Indexed("IX_Message_MessageId").Unique();
        }

        public override void Down()
        {
        }
    }
}
