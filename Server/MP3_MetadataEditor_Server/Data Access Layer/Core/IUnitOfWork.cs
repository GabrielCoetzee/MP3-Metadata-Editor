using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP3_MetadataEditor_RestServiceLibrary.Core.Repositories;

namespace MP3_MetadataEditor_RestServiceLibrary.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMP3Repository Mp3s { get; }
        int Complete();
    }
}
