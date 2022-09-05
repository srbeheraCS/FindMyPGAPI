using FindMyPG.Core.Entities;
using FindMyPG.Core.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Data
{
    public class PGDBContext : IdentityDbContext<User,Role,long>,IDbContext
    {
        public PGDBContext(DbContextOptions<PGDBContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var Configurations = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(x => x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            foreach(var configuration in Configurations)
            {
                dynamic? instance=Activator.CreateInstance(configuration);
                builder.ApplyConfiguration(instance);
            }
            foreach(var relationship in builder.Model.GetEntityTypes().SelectMany(m=>m.GetForeignKeys()))
            {
                relationship.DeleteBehavior= DeleteBehavior.NoAction;
            }
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

    }
}
