using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class User: IdentityUser<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public Role Role { get; set; }  
        public virtual ICollection<PGInfo> PGInfos{ get; set; }
        public virtual PGBooking PGBooking { get; set; }//1-1

    }
}
