using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP3_MetadataEditor_RestServiceLibrary.Core.Repositories;
using MP3_MetadataEditor_Server.Models;
using MP3_MetadataEditor_Server.Context_Classes;

namespace MP3_MetadataEditor_Server.Data_Access_Layer.Persistence
{
    public class MP3Repository : Repository<MP3>, IMP3Repository
    {   
        public MP3DbContext MP3DbContext
        {
            get { return Context as MP3DbContext; }
        }

        public MP3Repository(MP3DbContext context) 
            : base(context)
        {

        }

        public void AddMp3(MP3 mp3)
        {
            if (MP3DbContext.MP3Sets.FirstOrDefault(x => x.SongTitle == mp3.SongTitle && x.Artist == mp3.Artist) == null)
            {
                MP3DbContext.MP3Sets.Add(mp3);
            }
        }
    }
}
