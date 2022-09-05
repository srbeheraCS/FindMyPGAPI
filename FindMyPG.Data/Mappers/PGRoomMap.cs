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
    public class PGRoomMap : IEntityTypeConfiguration<PGRoom>
    {
        public void Configure(EntityTypeBuilder<PGRoom> builder)
        {
            var table=builder.ToTable("PGRoom");
            table.HasKey(x => x.Id);

            table.HasMany(r => r.PGBookings)
                 .WithOne(i => i.PGRoom)
                 .HasForeignKey(k => k.PGRoomId);
        }
        
    }
}
