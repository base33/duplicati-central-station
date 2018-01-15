using Duplicati.CentralStation.Api.Models;
using Duplicati.CentralStation.Data;
using Duplicati.CentralStation.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Globalization;

namespace Duplicati.CentralStation.Api.Controllers
{
    public class ReportsController : ApiController
    {
        /// <summary>
        /// Success:
        /// message=Duplicati%20Backup%20report%20for%20test%0D%0A%0D%0ADeletedFiles%3A%200%0D%0ADeletedFolders%3A%200%0D%0AModifiedFiles%3A%200%0D%0AExaminedFiles%3A%206%0D%0AOpenedFiles%3A%200%0D%0AAddedFiles%3A%200%0D%0ASizeOfModifiedFiles%3A%200%0D%0ASizeOfAddedFiles%3A%200%0D%0ASizeOfExaminedFiles%3A%2013919%0D%0ASizeOfOpenedFiles%3A%200%0D%0ANotProcessedFiles%3A%200%0D%0AAddedFolders%3A%200%0D%0ATooLargeFiles%3A%200%0D%0AFilesWithError%3A%200%0D%0AModifiedFolders%3A%200%0D%0AModifiedSymlinks%3A%200%0D%0AAddedSymlinks%3A%200%0D%0ADeletedSymlinks%3A%200%0D%0APartialBackup%3A%20False%0D%0ADryrun%3A%20False%0D%0AMainOperation%3A%20Backup%0D%0AParsedResult%3A%20Success%0D%0AVerboseOutput%3A%20False%0D%0AVerboseErrors%3A%20False%0D%0AEndTime%3A%201%2F8%2F2018%2010%3A09%3A16%20PM%0D%0ABeginTime%3A%201%2F8%2F2018%2010%3A09%3A16%20PM%0D%0ADuration%3A%2000%3A00%3A00.2339903%0D%0AMessages%3A%20%5B%0D%0A%20%20%20%20No%20remote%20filesets%20were%20deleted%2C%0D%0A%20%20%20%20removing%20file%20listed%20as%20Temporary%3A%20duplicati-b01323d5f20f448ef98247929ca38a32f.dblock.zip.aes%2C%0D%0A%20%20%20%20removing%20file%20listed%20as%20Temporary%3A%20duplicati-if782675db40345d4a4da546c19c08ddd.dindex.zip.aes%0D%0A%5D%0D%0AWarnings%3A%20%5B%5D%0D%0AErrors%3A%20%5B%5D
        /// 
        /// Error:
        /// message=Duplicati%20Backup%20report%20for%20test%0D%0A%0D%0AFailed%3A%20The%20AWS%20Access%20Key%20Id%20you%20provided%20does%20not%20exist%20in%20our%20records.%0D%0ADetails%3A%20Amazon.S3.AmazonS3Exception%3A%20The%20AWS%20Access%20Key%20Id%20you%20provided%20does%20not%20exist%20in%20our%20records.%20---%3E%20Amazon.Runtime.Internal.HttpErrorResponseException%3A%20The%20remote%20server%20returned%20an%20error%3A%20%28403%29%20Forbidden.%20---%3E%20System.Net.WebException%3A%20The%20remote%20server%20returned%20an%20error%3A%20%28403%29%20Forbidden.%0D%0A%20%20%20at%20System.Net.HttpWebRequest.GetResponse%28%29%0D%0A%20%20%20at%20Amazon.Runtime.Internal.HttpRequest.GetResponse%28%29%0D%0A%20%20%20---%20End%20of%20inner%20exception%20stack%20trace%20---%0D%0A%20%20%20at%20Amazon.Runtime.Internal.HttpRequest.GetResponse%28%29%0D%0A%20%20%20at%20Amazon.Runtime.Internal.HttpHandler%601.InvokeSync%28IExecutionContext%20executionContext%29%0D%0A%20%20%20at%20Amazon.Runtime.Internal.RedirectHandler.InvokeSync%28IExecutionContext%20executionContext%29%0D%0A%20%20%20at%20Amazon.Runtime.Internal.Unmarshaller.InvokeSync%28IExecutionContext%20executionContext%29%0D%0A%20%20%20at%20Amazon.S3.Internal.AmazonS3ResponseHandler.InvokeSync%28IExecutionContext%20executionContext%29%0D%0A%20%20%20at%20Amazon.Runtime.Internal.ErrorHandler.InvokeSync%28IExecutionContext%20executionContext%29%0D%0A%20%20%20---%20End%20of%20inner%20exception%20stack%20trace%20---%0D%0A%20%20%20at%20Duplicati.Library.Main.BackendManager.List%28%29%0D%0A%20%20%20at%20Duplicati.Library.Main.Operation.FilelistProcessor.RemoteListAnalysis%28BackendManager%20backend%2C%20Options%20options%2C%20LocalDatabase%20database%2C%20IBackendWriter%20log%2C%20String%20protectedfile%29%0D%0A%20%20%20at%20Duplicati.Library.Main.Operation.FilelistProcessor.VerifyRemoteList%28BackendManager%20backend%2C%20Options%20options%2C%20LocalDatabase%20database%2C%20IBackendWriter%20log%2C%20String%20protectedfile%29%0D%0A%20%20%20at%20Duplicati.Library.Main.Operation.BackupHandler.PreBackupVerify%28BackendManager%20backend%2C%20String%20protectedfile%29%0D%0A%20%20%20at%20Duplicati.Library.Main.Operation.BackupHandler.Run%28String%5B%5D%20sources%2C%20IFilter%20filter%29%0D%0A%20%20%20at%20Duplicati.Library.Main.Controller.%3C%3Ec__DisplayClass16_0.%3CBackup%3Eb__0%28BackupResults%20result%29%0D%0A%20%20%20at%20Duplicati.Library.Main.Controller.RunAction%5BT%5D%28T%20result%2C%20String%5B%5D%26%20paths%2C%20IFilter%26%20filter%2C%20Action%601%20method%29%0D%0A
        /// </summary>
        /// <returns></returns>
        public async Task Post(int id, [FromBody]DuplicatiReportRequest message)
        {
            var report = ParseDuplicatiReportMessage(message.Message);
            report.InstanceId = id;
            try
            {
                //save to disk
                using (DuplicatiContext db = new DuplicatiContext())
                {
                    db.BackupReports.Add(report);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(HttpContext.Current.Server.MapPath("/app_data/report_errors.txt"), JsonConvert.SerializeObject(ex) + JsonConvert.SerializeObject(report));
                throw ex;
            }
            
        }

        protected BackupReport ParseDuplicatiReportMessage(string message)
        {
            var decoded = HttpUtility.UrlDecode(message);

            if (decoded.Contains("DeletedFiles"))
            {
                return ProcessSuccessMessage(decoded);
            }

            return ProcessFailureMessage(decoded);
        }

        protected BackupReport ProcessFailureMessage(string message)
        {
            return new BackupReport
            {
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now,
                AddedFiles = 0,
                AddedFolders = 0,
                AddedSymlinks = 0,
                DeletedFiles = 0,
                DeletedFolders = 0,
                DryRun = false,
                Duration = TimeSpan.MinValue,
                DurationTicks = 0,
                ExaminedFiles = 0,
                Errors = message,
                FilesWithError = 0,
                Message = message,
                ModifiedFiles = 0,
                ModifiedFolders = 0,
                ModifiedSymlinks = 0,
                NotProcessedFiles = 0,
                OpenedFiles = 0,
                PartialBackup = false,
                SizeOfExaminedFiles = 0,
                Success = false,
                SizeOfAddedFiles = 0,
                SizeOfModifiedFiles = 0,
                SizeOfOpenedFiles = 0,
                TooLargeFiles = 0,
                VerboseErrors = false,
                VerboseOutput = false,
                Warnings = ""
            };
        }

        protected BackupReport ProcessSuccessMessage(string message)
        {
            var lines = message.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines.RemoveRange(0, 2);

            var report = new BackupReport();
            bool insideSquareBrackets = false;
            string currentKey = "";
            foreach (var line in lines)
            {
                string currentValue;
                if (insideSquareBrackets)
                {
                    currentValue = line;
                }
                else
                {
                    var index = line.IndexOf(':');
                    currentKey = line.Substring(0, index).Trim();
                    currentValue = line.Substring(index + 1).Trim();
                }

                if (currentValue == "[")
                {
                    insideSquareBrackets = true;
                }
                else if (currentValue == "]")
                {
                    insideSquareBrackets = false;
                }
                else if (currentValue != "[]")
                {
                    //its a key value (key: value)
                    SetValue(report, currentKey, currentValue, insideSquareBrackets);
                }

            }
            return report;
        }

        protected void SetValue(BackupReport report, string key, string value, bool append)
        {
            switch(key)
            {
                case "DeletedFiles":
                    report.DeletedFiles = int.Parse(value);
                    break;
                case "ExaminedFiles":
                    report.ExaminedFiles = int.Parse(value);
                    break;
                case "ModifiedFiles":
                    report.ExaminedFiles = int.Parse(value);
                    break;
                case "DeletedFolders":
                    report.DeletedFolders = int.Parse(value);
                    break;
                case "OpenedFiles":
                    report.OpenedFiles = int.Parse(value);
                    break;
                case "AddedFiles":
                    report.AddedFiles = int.Parse(value);
                    break;
                case "SizeOfModifiedFiles":
                    report.SizeOfModifiedFiles = int.Parse(value);
                    break;
                case "SizeOfAddedFiles":
                    report.SizeOfAddedFiles = int.Parse(value);
                    break;
                case "SizeOfExaminedFiles":
                    report.SizeOfExaminedFiles = int.Parse(value);
                    break;
                case "SizeOfOpenedFiles":
                    report.SizeOfOpenedFiles = int.Parse(value);
                    break;
                case "NotProcessedFiles":
                    report.NotProcessedFiles = int.Parse(value);
                    break;
                case "AddedFolders":
                    report.AddedFolders = int.Parse(value);
                    break;
                case "TooLargeFiles":
                    report.TooLargeFiles = int.Parse(value);
                    break;
                case "FilesWithError":
                    report.FilesWithError = int.Parse(value);
                    break;
                case "ModifiedFolders":
                    report.ModifiedFolders = int.Parse(value);
                    break;
                case "ModifiedSymlinks":
                    report.ModifiedSymlinks = int.Parse(value);
                    break;
                case "AddedSymlinks":
                    report.AddedSymlinks = int.Parse(value);
                    break;
                case "PartialBackup":
                    report.PartialBackup = value.ToLower() == "true";
                    break;
                case "Dryrun":
                    report.DryRun = value.ToLower() == "true";
                    break;
                case "ParsedResult":
                    report.Success = value.ToLower() == "success";
                    break;
                case "VerboseOutput":
                    report.VerboseOutput = value.ToLower() == "true";
                    break;
                case "VerboseErrors":
                    report.VerboseErrors = value.ToLower() == "true";
                    break;
                case "EndTime":
                    //report.EndDate = DateTime.Parse(value);
                    report.EndDate = DateTime.ParseExact(value, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    break;
                case "BeginTime":
                    //report.BeginDate = DateTime.Parse(value);
                    report.BeginDate = DateTime.ParseExact(value, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    break;
                case "Duration":
                    report.Duration = TimeSpan.Parse(value);
                    break;
                case "Messages":
                    report.Message += !string.IsNullOrEmpty(report.Message) ? Environment.NewLine + value : value;
                    break;
                case "Warnings":
                    report.Warnings = value;
                    break;
                case "Errors":
                    report.Errors = value;
                    break;

            }
        }
    }
}

/*
 * message=Duplicati Backup report for test

DeletedFiles: 0
DeletedFolders: 0
ModifiedFiles: 0
ExaminedFiles: 6
OpenedFiles: 0
AddedFiles: 0
SizeOfModifiedFiles: 0
SizeOfAddedFiles: 0
SizeOfExaminedFiles: 13919
SizeOfOpenedFiles: 0
NotProcessedFiles: 0
AddedFolders: 0
TooLargeFiles: 0
FilesWithError: 0
ModifiedFolders: 0
ModifiedSymlinks: 0
AddedSymlinks: 0
DeletedSymlinks: 0
PartialBackup: False
Dryrun: False
MainOperation: Backup
ParsedResult: Success
VerboseOutput: False
VerboseErrors: False
EndTime: 1/8/2018 10:09:16 PM
BeginTime: 1/8/2018 10:09:16 PM
Duration: 00:00:00.2339903
Messages: [
    No remote filesets were deleted,
    removing file listed as Temporary: duplicati-b01323d5f20f448ef98247929ca38a32f.dblock.zip.aes,
    removing file listed as Temporary: duplicati-if782675db40345d4a4da546c19c08ddd.dindex.zip.aes
]
Warnings: []
Errors: []

    */