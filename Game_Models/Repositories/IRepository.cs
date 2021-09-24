using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Models.Repositories
{
    public interface IRepository<TEntity>
    {
        IList<TEntity> List();
        void Add(TEntity entity);
        void Delete(int id);
        TEntity Find(int id);
        void Update(TEntity entity);
    }
}
