using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duplicati.CentralStation.Data.Models;

namespace Duplicati.CentralStation.Api.Models
{
    public class Backup
    {
        public int InstanceId { get; set; }
        public string InstanceName { get; set; }
        public bool ShouldBeFlagged { get; set; }
        public string FlagReason { get; set; }
        public BackupReport Report { get; set; }
        public BackupReport LastSuccessfulReport { get; set; }
    }
}
