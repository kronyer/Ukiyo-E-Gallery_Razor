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
    public class UkiyoeRepository : Repository<Ukiyoe>, IUkiyoeRepository
    {
        private readonly AppDbContext _db;

        public UkiyoeRepository(AppDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Ukiyoe ukiyoe)
        {
            var ukiyoEToUpdate = _db.Ukiyoe.FirstOrDefault(x => x.Id == ukiyoe.Id);
            ukiyoEToUpdate.Name = ukiyoe.Name;
            ukiyoEToUpdate.Description = ukiyoe.Description;
            ukiyoEToUpdate.ArtistId = ukiyoe.ArtistId;
            if (ukiyoe.Image != null)
            {
                ukiyoEToUpdate.Image = ukiyoe.Image;
            }
        }
    }
}
