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
    internal class ZipCodeMap : IEntityTypeConfiguration<ZipCode>
    {
        public void Configure(EntityTypeBuilder<ZipCode> builder)
        {
            var table=builder.ToTable("ZipCode");
            table.HasKey(t => t.Id);
            table.HasMany(r => r.PGInfos)
                 .WithOne(i => i.ZipCode)
                 .HasForeignKey(k => k.ZipId); 
        }
    }
}
