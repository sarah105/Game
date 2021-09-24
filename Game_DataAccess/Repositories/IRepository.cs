using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Repositories
{
    public interface IRepository<TEntity>
    {
        IList<TEntity> List();
        bool Add(TEntity entity);
        bool Delete(TEntity entity);
        TEntity Find(int id);
        bool Update(TEntity entity);
    }
}
