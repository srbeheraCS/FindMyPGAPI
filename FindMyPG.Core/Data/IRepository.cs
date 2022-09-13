using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Data
{
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        void Insert(TEntity entity, bool saveChanges = false);
        void Insert(IEnumerable<TEntity> entities, bool saveChanges = false);
        void Update(TEntity entity, bool saveChanges = false);
        void Delete(TEntity entity, bool saveChanges = false);
        IQueryable<TEntity> Table { get; }
        TEntity GetByID(object id);

    }
}
