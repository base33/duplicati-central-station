using Duplicati.CentralStation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicati.CentralStation.Api.Models
{
    public class BackupDetailed
    {
        public int InstanceId { get; set; }
        public string InstanceName { get; set; }
        public string Url { get; set; }
        public BackupReport LatestReport { get; set; }
        public List<BackupReport> PreviousReports { get; set; }
    }
}
