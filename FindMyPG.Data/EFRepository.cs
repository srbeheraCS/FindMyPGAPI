using FindMyPG.Core.Data;
using FindMyPG.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private IDbContext _context;
        private DbSet<TEntity> _entities;
        public EFRepository(IDbContext context)
        {
            _context = context;
        }
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }
        public IQueryable<TEntity> Table => Entities;

        public void Delete(TEntity entity, bool saveChanges = false)
        {
            if (entity == null)
                throw new NullReferenceException();
            Entities.Remove(entity);
            if (saveChanges)
                saveChange(saveChanges);
        }

        public TEntity GetByID(object id)
        {
            return Entities.Find(id);
        }
        public void Insert(TEntity entity, bool saveChanges = false)
        {

            if (entity == null)
                throw new NullReferenceException();
            Entities.Add(entity);
        }
        public void saveChange(bool saveChanges)
        {
            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Insert(IEnumerable<TEntity> entities, bool saveChanges = false)
        {
            if (entities == null)
                throw new NullReferenceException();
            Entities.AddRange(entities);
            if (saveChanges)
                saveChange(saveChanges);
        }

        public void Update(TEntity entity, bool saveChanges = false)
        {
            if (entity == null)
                throw new NullReferenceException();
            Entities.Update(entity);
            if (saveChanges)
                saveChange(saveChanges);

        }


    }
}
