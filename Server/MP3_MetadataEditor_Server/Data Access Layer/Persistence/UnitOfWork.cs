using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP3_MetadataEditor_RestServiceLibrary.Core;
using MP3_MetadataEditor_RestServiceLibrary.Core.Repositories;
using MP3_MetadataEditor_Server.Context_Classes;

namespace MP3_MetadataEditor_Server.Data_Access_Layer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MP3DbContext _context;
        public IMP3Repository Mp3s { get; }


        public UnitOfWork(MP3DbContext context)
        {
            _context = context;
            Mp3s = new MP3Repository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
