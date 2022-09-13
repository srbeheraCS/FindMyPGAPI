using FindMyPG.Core.PGEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Configs
{
    public class FindMyPGConfig
    {
        public string SqlConnectionString { get; set; }
        public int MaxCount { get; set; }
        public string JwtSigningKey { get; set; }
        public string JwtValidAudience { get; set; }
        public string JwtValidIssuer { get; set; }
        public double JwtAccessTokenExpireInMinutes { get; set; }
        Singleton singleton = new Singleton();
        
    }
   
}
