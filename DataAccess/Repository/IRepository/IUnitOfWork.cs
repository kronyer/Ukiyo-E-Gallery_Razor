using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IArtistRepository Artist { get; }
        IUkiyoeRepository Ukiyoe { get; }

        void Save();
    }
}
