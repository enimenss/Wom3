namespace WOM3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailReg",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsernameId = c.String(nullable: false, maxLength: 20),
                        FriendRId = c.String(nullable: false, maxLength: 20),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.FriendRId)
                .ForeignKey("dbo.User", t => t.UsernameId, cascadeDelete: true)
                .Index(t => t.UsernameId)
                .Index(t => t.FriendRId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        Pass = c.String(nullable: false),
                        Avatar = c.String(nullable: false),
                        NumOfNews = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.UserHeroes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 20),
                        Name = c.String(maxLength: 128),
                        Wins = c.Int(nullable: false),
                        Loses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Heroes", t => t.Name)
                .ForeignKey("dbo.User", t => t.Username)
                .Index(t => t.Username)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Image = c.String(nullable: false),
                        Health = c.Int(nullable: false),
                        Mana = c.Int(nullable: false),
                        HealthReg = c.Single(nullable: false),
                        ManaReg = c.Single(nullable: false),
                        Armor = c.Single(nullable: false),
                        Demage = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.HeroSpells",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        SpellID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Heroes", t => t.Name)
                .ForeignKey("dbo.Spells", t => t.SpellID, cascadeDelete: true)
                .Index(t => t.Name)
                .Index(t => t.SpellID);
            
            CreateTable(
                "dbo.Spells",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Demage = c.Single(nullable: false),
                        Area = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 20),
                        ItemID = c.Int(nullable: false),
                        DateOfPurchase = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.Username)
                .Index(t => t.Username)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Image = c.String(nullable: false),
                        Health = c.Int(nullable: false),
                        Mana = c.Int(nullable: false),
                        HealthReg = c.Single(nullable: false),
                        ManaReg = c.Single(nullable: false),
                        Armor = c.Single(nullable: false),
                        Demage = c.Single(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemSpells",
                c => new
                    {
                        ItemID = c.Int(nullable: false),
                        Demage = c.Single(nullable: false),
                        Area = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.Items", t => t.ItemID)
                .Index(t => t.ItemID);
            
            CreateTable(
                "dbo.UserStats",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 20),
                        Wins = c.Int(nullable: false),
                        Loses = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Gold = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.User", t => t.Username)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsernameId = c.String(nullable: false, maxLength: 20),
                        FriendId = c.String(nullable: false, maxLength: 20),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.FriendId)
                .ForeignKey("dbo.User", t => t.UsernameId, cascadeDelete: true)
                .Index(t => t.UsernameId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UsernameId = c.String(nullable: false, maxLength: 20),
                        ReceiverId = c.String(nullable: false, maxLength: 20),
                        Message = c.String(nullable: false),
                        isRead = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User", t => t.ReceiverId)
                .ForeignKey("dbo.User", t => t.UsernameId, cascadeDelete: true)
                .Index(t => t.UsernameId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naslov = c.String(nullable: false),
                        Info = c.String(nullable: false),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        token = c.String(nullable: false, maxLength: 128),
                        Username = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.token)
                .ForeignKey("dbo.User", t => t.Username)
                .Index(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "Username", "dbo.User");
            DropForeignKey("dbo.Messages", "UsernameId", "dbo.User");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.Friends", "UsernameId", "dbo.User");
            DropForeignKey("dbo.Friends", "FriendId", "dbo.User");
            DropForeignKey("dbo.FriendRequests", "UsernameId", "dbo.User");
            DropForeignKey("dbo.FriendRequests", "FriendRId", "dbo.User");
            DropForeignKey("dbo.UserStats", "Username", "dbo.User");
            DropForeignKey("dbo.UserItems", "Username", "dbo.User");
            DropForeignKey("dbo.UserItems", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemSpells", "ItemID", "dbo.Items");
            DropForeignKey("dbo.UserHeroes", "Username", "dbo.User");
            DropForeignKey("dbo.UserHeroes", "Name", "dbo.Heroes");
            DropForeignKey("dbo.HeroSpells", "SpellID", "dbo.Spells");
            DropForeignKey("dbo.HeroSpells", "Name", "dbo.Heroes");
            DropIndex("dbo.Token", new[] { "Username" });
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "UsernameId" });
            DropIndex("dbo.Friends", new[] { "FriendId" });
            DropIndex("dbo.Friends", new[] { "UsernameId" });
            DropIndex("dbo.UserStats", new[] { "Username" });
            DropIndex("dbo.ItemSpells", new[] { "ItemID" });
            DropIndex("dbo.UserItems", new[] { "ItemID" });
            DropIndex("dbo.UserItems", new[] { "Username" });
            DropIndex("dbo.HeroSpells", new[] { "SpellID" });
            DropIndex("dbo.HeroSpells", new[] { "Name" });
            DropIndex("dbo.UserHeroes", new[] { "Name" });
            DropIndex("dbo.UserHeroes", new[] { "Username" });
            DropIndex("dbo.FriendRequests", new[] { "FriendRId" });
            DropIndex("dbo.FriendRequests", new[] { "UsernameId" });
            DropTable("dbo.Token");
            DropTable("dbo.News");
            DropTable("dbo.Messages");
            DropTable("dbo.Friends");
            DropTable("dbo.UserStats");
            DropTable("dbo.ItemSpells");
            DropTable("dbo.Items");
            DropTable("dbo.UserItems");
            DropTable("dbo.Spells");
            DropTable("dbo.HeroSpells");
            DropTable("dbo.Heroes");
            DropTable("dbo.UserHeroes");
            DropTable("dbo.User");
            DropTable("dbo.FriendRequests");
            DropTable("dbo.EmailReg");
        }
    }
}
