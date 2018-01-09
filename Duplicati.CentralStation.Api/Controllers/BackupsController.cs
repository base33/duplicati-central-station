using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Duplicati.CentralStation.Api.Models;
using Duplicati.CentralStation.Data;

namespace Duplicati.CentralStation.Api.Controllers
{
    public class BackupsController : ApiController
    {
        [HttpGet]
        public IEnumerable<Backup> Latest()
        {
            var backups = new List<Backup>();

            using (DuplicatiContext db = new DuplicatiContext())
            {
                foreach (var instance in db.Instances.OrderBy(i => i.Name).ToList())
                {
                    var report = instance.BackupReports.LastOrDefault();
                    var backup = new Backup
                    {
                        InstanceId = instance.Id,
                        InstanceName = instance.Name,
                        Report = report
                    };

                    if (report != null)
                    {
                        if (!report.Success)
                        {
                            backup.LastSuccessfulReport = instance.BackupReports.LastOrDefault(b => b.Success);
                            backup.ShouldBeFlagged = true;
                        }
                        else
                        {
                            backup.LastSuccessfulReport = report;

                            backup.ShouldBeFlagged = report.BeginDate < DateTime.Now.AddDays(-1);
                        }
                    }
                    else
                    {
                        backup.ShouldBeFlagged = true;
                    }

                    

                    backups.Add(backup);
                }
            }

            return backups.OrderByDescending(b => b.ShouldBeFlagged);
        } 
    }
}
