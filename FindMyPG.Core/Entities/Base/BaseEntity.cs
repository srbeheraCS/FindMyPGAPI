using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities.Base
{
    public class BaseEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity),Key()]
        
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
