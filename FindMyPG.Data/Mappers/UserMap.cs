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
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var table = builder.ToTable("User");
            table.HasKey(x => x.Id);
            table.HasOne(u => u.PGBooking)
                 .WithOne(p => p.User)
                 .HasForeignKey<PGBooking>(k => k.SeekerId);
            table.HasMany(r => r.PGInfos)
                 .WithOne(i => i.User)
                 .HasForeignKey(k => k.OwnerId);
        }
    }
}
