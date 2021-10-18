using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Repositories.IRepositories
{
    public interface ICURT<IEntity>
    {
        IList<IEntity> List();
        IEntity Find(int id);
        IEntity Add(IEntity entity);
        bool Delete(IEntity entity);
        bool Update(IEntity entity);
    }
}
