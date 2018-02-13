using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Duplicati.CentralStation.Data.Models
{
    [Table("Instance")]
    [DataContract]
    public class Instance
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public int HoursBetweenBackups { get; set; }

        [DataMember]
        public string DateTimeFormat { get; set; }

        public virtual ICollection<BackupReport> BackupReports { get; set; } 
    }
}
