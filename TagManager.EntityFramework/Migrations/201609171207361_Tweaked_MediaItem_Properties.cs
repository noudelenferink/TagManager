namespace TagManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tweaked_MediaItem_Properties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaItems", "TypeId", c => c.Int(nullable: false));
            AddColumn("dbo.MediaItems", "FileName", c => c.String(unicode: false));
            AddColumn("dbo.MediaItems", "StatusId", c => c.Int(nullable: false));
            DropColumn("dbo.MediaItems", "Type");
            DropColumn("dbo.MediaItems", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MediaItems", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.MediaItems", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.MediaItems", "StatusId");
            DropColumn("dbo.MediaItems", "FileName");
            DropColumn("dbo.MediaItems", "TypeId");
        }
    }
}
