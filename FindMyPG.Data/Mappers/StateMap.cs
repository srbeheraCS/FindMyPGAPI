using FindMyPG.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Data.Mappers
{
    internal class StateMap : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            var table = builder.ToTable("State");
            table.HasKey(x => x.Id);
            table.HasMany(r => r.PGInfos)
                 .WithOne(i => i.State)
                 .HasForeignKey(k => k.StateId);

            table.HasMany(p => p.Cities)
                 .WithOne(i => i.State)
                 .HasForeignKey(k => k.StateId);
        }
    }
}
