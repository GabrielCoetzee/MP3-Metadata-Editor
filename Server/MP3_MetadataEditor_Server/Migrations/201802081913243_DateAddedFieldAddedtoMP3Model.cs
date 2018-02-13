namespace MP3_MetadataEditor_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateAddedFieldAddedtoMP3Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MP3", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MP3", "DateAdded");
        }
    }
}
