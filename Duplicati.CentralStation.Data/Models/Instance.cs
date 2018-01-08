using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicati.CentralStation.Data.Models
{
    [Table("Instance")]
    public class Instance
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public virtual ICollection<BackupStatus> BackupStatuses { get; set; } 
    }
}
