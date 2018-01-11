using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public int ModifiedFiles { get; set; }
        public int DeletedFolders { get; set; }
        public int OpenedFiles { get; set; }
        public int SizeOfModifiedFiles { get; set; }
        public int SizeOfOpenedFiles { get; set; }
        public int AddedFolders { get; set; }
        public int TooLargeFiles { get; set; }
        public int FilesWithError { get; set; }
        public int ModifiedFolders { get; set; }
        public int ModifiedSymlinks { get; set; }
        public int AddedSymlinks { get; set; }
        public bool PartialBackup { get; set; }
        public bool DryRun { get; set; }
        public bool VerboseOutput { get; set; }
        public bool VerboseErrors { get; set; }
        public string Warnings { get; set; }
        public string Errors { get; set; }

        [NotMapped]
        public TimeSpan Duration
        {
            get { return new TimeSpan(DurationTicks); }
            set { DurationTicks = value.Ticks; }
        }

        public int InstanceId { get; set; }
        
        [JsonIgnore]
        public virtual Instance Instance { get; set; }
    }
}
