using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duplicati.CentralStation.Data.Models;

namespace Duplicati.CentralStation.Data
{
    public class DuplicatiContext : DbContext
    {
        public DuplicatiContext() : base("CentralStation")
        {
            
        }

        public IDbSet<Instance> Instances { get; set; } 
        public IDbSet<BackupReport> BackupReports { get; set; }  
    }
}
