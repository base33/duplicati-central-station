using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicati.CentralStation.Data.Models
{
    [Table("BackupStatus")]
    public class BackupStatus
    {
        [Key]
        public int Key { get; set; }

        public bool Success { get; set; }

        public DateTime DateStamp { get; set; }

        public string Message { get; set; }

        public int InstanceId { get; set; }

        public Instance Instance { get; set; }
    }
}
