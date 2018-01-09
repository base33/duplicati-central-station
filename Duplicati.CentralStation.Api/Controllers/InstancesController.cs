using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Duplicati.CentralStation.Data;
using Duplicati.CentralStation.Data.Models;

namespace Duplicati.CentralStation.Api.Controllers
{
    public class InstancesController : ApiController
    {
        public async Task<IEnumerable<Instance>> Get()
        {
            using (DuplicatiContext db = new DuplicatiContext())
            {
                return await db.Instances.OrderBy(i => i.Name).ToListAsync();
            }
        }

        public async Task Post(Instance instance)
        {
            using (DuplicatiContext db = new DuplicatiContext())
            {
                db.Instances.Add(instance);
                await db.SaveChangesAsync();
            }
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            using (DuplicatiContext db = new DuplicatiContext())
            {
                var instance = db.Instances.FirstOrDefault(i => i.Id == id);

                var reports = instance.BackupReports.ToList();

                foreach (var report in reports)
                {
                    db.BackupReports.Remove(report);
                }
                db.Instances.Remove(instance);
                await db.SaveChangesAsync();
            }
        }
    }
}
