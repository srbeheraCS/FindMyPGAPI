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
    public class PGPackageMap : IEntityTypeConfiguration<PGPackage>
    {
        public void Configure(EntityTypeBuilder<PGPackage> builder)
        {
            var table=builder.ToTable("PGPackage");
            table.HasKey(k => k.Id);

            table.HasMany(r => r.PGBookings)
                 .WithOne(i => i.PGPackage)
                 .HasForeignKey(k => k.PGPackageId);
        }
    }
}
