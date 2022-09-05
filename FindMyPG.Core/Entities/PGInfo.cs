using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class PGInfo: BaseEntity
    {
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int ZipId { get; set; }
        public string Landmark { get; set; }
        public int PGCategory { get; set; } 
        public string ContactNumber { get; set; }
        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual ZipCode ZipCode { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PGRoom> PGRooms { get; set; }
        public virtual ICollection<PGBooking> PGBookings { get; set; }
        public virtual ICollection<PGPackage> PGPackages { get; set; }
    }
}
