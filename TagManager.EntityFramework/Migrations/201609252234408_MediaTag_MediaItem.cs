namespace TagManager.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class MediaTag_MediaItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaItemMediaTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaItemId = c.Int(nullable: false),
                        MediaTagId = c.Int(nullable: false),
                        Position = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MediaItemMediaTag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MediaItems", t => t.MediaItemId, cascadeDelete: true)
                .ForeignKey("dbo.MediaTags", t => t.MediaTagId, cascadeDelete: true)
                .Index(t => t.MediaItemId)
                .Index(t => t.MediaTagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MediaItemMediaTags", "MediaTagId", "dbo.MediaTags");
            DropForeignKey("dbo.MediaItemMediaTags", "MediaItemId", "dbo.MediaItems");
            DropIndex("dbo.MediaItemMediaTags", new[] { "MediaTagId" });
            DropIndex("dbo.MediaItemMediaTags", new[] { "MediaItemId" });
            DropTable("dbo.MediaItemMediaTags",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MediaItemMediaTag_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
