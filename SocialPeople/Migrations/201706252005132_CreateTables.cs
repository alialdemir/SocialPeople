namespace SocialPeople.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PeopleId = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 100),
                        PictureUrl = c.String(maxLength: 300),
                        Score = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.PeopleId);
            
            CreateTable(
                "dbo.SocialNetworks",
                c => new
                    {
                        SocialNetworkId = c.Int(nullable: false, identity: true),
                        SocialNetworkType = c.Int(nullable: false),
                        PeopleId = c.Int(nullable: false),
                        Url = c.String(maxLength: 255),
                        Verified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SocialNetworkId)
                .ForeignKey("dbo.People", t => t.PeopleId, cascadeDelete: true)
                .Index(t => t.PeopleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SocialNetworks", "PeopleId", "dbo.People");
            DropIndex("dbo.SocialNetworks", new[] { "PeopleId" });
            DropTable("dbo.SocialNetworks");
            DropTable("dbo.People");
        }
    }
}
