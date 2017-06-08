namespace WOM3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Naslov", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Naslov");
        }
    }
}
