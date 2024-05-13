using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public readonly AppDbContext _db;

        public ArtistRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Artist artist)
        {
            var artistToUpdate = _db.Artist.FirstOrDefault(x => x.Id ==  artist.Id); 
            artistToUpdate.Name = artist.Name;
        }
    }
}
