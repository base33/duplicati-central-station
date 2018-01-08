using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicati.CentralStation.Api.Models
{
    public class DuplicatiReport
    {
        public int DeletedFiles { get; set; }
        public int ExaminedFiles { get; set; }
        public string ParsedResult { get; set; }
        public bool Success { get { return ParsedResult == "Success"; } }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
