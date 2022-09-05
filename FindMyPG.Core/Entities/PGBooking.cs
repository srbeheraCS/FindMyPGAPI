using FindMyPG.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class PGBooking:BaseEntity
    {
        public long SeekerId { get; set; }
        public virtual User User { get; set; }
        public int PGInfoId { get; set; }
        public virtual PGInfo PGInfo { get; set; }
        
        public int PGRoomId { get; set; }
        public virtual PGRoom PGRoom { get; set; }
        
        public int PGPackageId { get; set; }
        public virtual PGPackage PGPackage { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
    }
}
