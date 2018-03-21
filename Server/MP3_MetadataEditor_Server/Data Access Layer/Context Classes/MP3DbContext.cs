using MP3_MetadataEditor_Server.Models;
using System.Data.Entity;

namespace MP3_MetadataEditor_Server.Context_Classes
{
    public class MP3DbContext : DbContext
    {
        public MP3DbContext()
            :base("MP3Repository") //DB Name
        {

        }
        public DbSet<MP3> MP3Sets { get; set; }
    }
}
