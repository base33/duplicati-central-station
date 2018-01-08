using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Duplicati.CentralStation.Api.Controllers
{
    public class NotificationsController : ApiController
    {
        public void Notify([FromBody] string body)
        {
            //save to disk
        }
    }
}
