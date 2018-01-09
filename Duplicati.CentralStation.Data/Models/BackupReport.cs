using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicati.CentralStation.Data.Models
{
    [Table("BackupReport")]
    public class BackupReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AddedFiles { get; set; }
        public int SizeOfAddedFiles { get; set; }
        public int ExaminedFiles { get; set; }
        public int SizeOfExaminedFiles { get; set; }
        public int NotProcessedFiles { get; set; }
        public int DeletedFiles { get; set; }
        public long DurationTicks { get; set; }

        [NotMapped]
        public TimeSpan Duration
        {
            get { return new TimeSpan(DurationTicks); }
            set { DurationTicks = value.Ticks; }
        }

        public int InstanceId { get; set; }

        public virtual Instance Instance { get; set; }
    }
}
