using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP3_MetadataEditor_Server.Models;

namespace MP3_MetadataEditor_RestServiceLibrary.Core.Repositories
{
    public interface IMP3Repository : IRepository<MP3>
    {
        void AddMp3(MP3 mp3);
    }
}
