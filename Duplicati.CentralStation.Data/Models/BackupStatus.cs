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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public bool Success { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Message { get; set; }

        public int InstanceId { get; set; }

        public virtual Instance Instance { get; set; }
    }
}
