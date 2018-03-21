namespace MP3_MetadataEditor_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMP3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MP3",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumArt = c.Binary(),
                        Album = c.String(),
                        Artist = c.String(),
                        Genre = c.String(),
                        Comments = c.String(),
                        Lyrics = c.String(),
                        Composer = c.String(),
                        SongTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MP3");
        }
    }
}
