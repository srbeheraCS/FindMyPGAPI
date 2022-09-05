using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class City:BaseEntity
    {
        public string Name { get; set; }

        public int StateId { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<PGInfo> PGinfos { get; set; }
        public virtual ICollection<ZipCode> ZipCodes { get; set; }
    }
}
