using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicati.CentralStation.Api.Models
{
    public class DuplicatiReport
    {
        public string ParsedResult { get; set; }
        public bool Success { get { return ParsedResult == "Success"; } }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AddedFiles { get; set; }
        public int SizeOfAddedFiles { get; set; }
        public int ExaminedFiles { get; set; }
        public int SizeOfExaminedFiles { get; set; }
        public int DeletedFiles { get; set; }
        public int NotProcessedFiles { get; set; }
        public TimeSpan Duration { get; set; }
        public string Messages { get; set; }
    }
}
