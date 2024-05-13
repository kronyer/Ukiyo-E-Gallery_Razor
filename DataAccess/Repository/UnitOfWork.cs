using DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Artist = new ArtistRepository(_db);
            Ukiyoe = new UkiyoeRepository(_db);
        }

        public IArtistRepository Artist { get; private set;}
        public IUkiyoeRepository Ukiyoe { get; private set;}

        public IArtistRepository ArtistRepository => throw new NotImplementedException();

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
