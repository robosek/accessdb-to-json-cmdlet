using GetJsonFromAccessDbCmdlet.Converters;
using GetJsonFromAccessDbCmdlet.Helpers;
using GetJsonFromAccessDbCmdlet.Interfaces;
using System;
using System.Management.Automation;

namespace GetJsonFromAccessDbCmdlet
{
    [Cmdlet(VerbsCommon.Get, "JsonFromAccessDb")]
    [OutputType(typeof(string))]
    public class GetJsonFromAccessDbCmdlet : Cmdlet
    {
        [Parameter]
        public string AccessDbFilePath { get; set; }
        [Parameter]
        public bool SaveOutputToFile { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (!FileHelper.FileExists(AccessDbFilePath))
            {
                WriteWarning("There is no such file in directory!");
                StopProcessing();
            }

        }
        protected override void ProcessRecord()
        {
            WriteProgress(new ProgressRecord(1,"Conversion","Converting database..."));
            AccessDbHelper acccessdbHelper = new AccessDbHelper(AccessDbFilePath, "Database");
            IJsonConverter dbJsonConverter =  new DataSetConverter(acccessdbHelper.GetDataSet());
            string databaseJson = dbJsonConverter.ConvertToJson();
            if (SaveOutputToFile)
                FileHelper.SaveToFile(databaseJson,$"output_{Guid.NewGuid()}.json");
            WriteObject(databaseJson);
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
            throw new Exception("Process is stopped");
        }
    }
}
