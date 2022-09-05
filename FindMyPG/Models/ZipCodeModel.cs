﻿using FindMyPG.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FindMyPG.Models
{
    public class ZipCodeModel:BaseModel
    {
        public string AreaName { get; set; }
        public int Value { get; set; }
        public int CityId { get; set; }
    }
   
    
}
