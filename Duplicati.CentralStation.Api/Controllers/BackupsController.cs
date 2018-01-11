using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Duplicati.CentralStation.Api.Models;
using Duplicati.CentralStation.Data;
using Duplicati.CentralStation.Data.Models;

namespace Duplicati.CentralStation.Api.Controllers
{
    public class BackupsController : ApiController
    {
        public BackupDetailed Get(int id)
        {
            using (DuplicatiContext db = new DuplicatiContext())
            {
                var instance = db.Instances.First(i => i.Id == id);

                var backup = new BackupDetailed
                {
                    InstanceId = instance.Id,
                    InstanceName = instance.Name,
                    Url = instance.Url,
                    PreviousReports = instance.BackupReports.OrderByDescending(b => b.Id).Take(30).ToList(),
                };
                backup.LatestReport = backup.PreviousReports.FirstOrDefault();

                return backup;
            }
        }

        [HttpGet]
        [Route("api/backups/{id}/backup/{backupId}")]
        public BackupReport GetBackup(int id, int backupId)
        {
            using (DuplicatiContext db = new DuplicatiContext())
            {
                return db.BackupReports.FirstOrDefault(b => b.Id == backupId);
            }
        }

        [HttpGet]
        [Route("api/backups/latest")]
        public IEnumerable<Backup> Latest()
        {
            using (DuplicatiContext db = new DuplicatiContext())
            {
                return db.Instances
                    .OrderBy(i => i.Name)
                    .ToList()
                    .Select(GetLastestBackupInfo)
                    .ToList()
                    .OrderByDescending(b => b.ShouldBeFlagged);
            }
        }

        private Backup GetLastestBackupInfo(Instance instance)
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
                    backup.FlagReason = "Backup failed";
                }
                else
                {
                    backup.LastSuccessfulReport = report;

                    backup.ShouldBeFlagged = report.BeginDate < DateTime.Now.AddDays(-1);
                    backup.FlagReason = "No backup since for past " + Math.Ceiling((DateTime.Now - report.BeginDate).TotalHours) + " hours";
                }
            }
            else
            {
                backup.ShouldBeFlagged = true;
                backup.FlagReason = "No backups";
            }

            return backup;
        }
    }
}
