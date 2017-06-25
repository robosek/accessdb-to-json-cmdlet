using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace GetJsonFromAccessDbCmdlet.Helpers
{
    internal class AccessDbHelper
    {
        internal AccessDbHelper(string databaseFilePath, string databaseName)
        {
            _databaseFilePath = databaseFilePath;
            _databaseName = databaseName;
        }

        internal DataSet GetDataSet()
        {
            string connectionString = GetDatabaseConnectionString();
            DataSet resultData = GetDataSetWithRequiredName();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                IEnumerable<string> tablesName = GetRequiredTablesName(connection);

                foreach (string tableName in tablesName)
                {
                    using (OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", tableName), connection))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        {
                            DataTable buffor = new DataTable($"{tableName}");
                            adapter.Fill(buffor);
                            resultData.Tables.Add(buffor);
                        }
                    }
                }
            }

            return resultData;
        }


        private string GetDatabaseConnectionString()
        {
            string fullPath = _databaseFilePath;
            return string.Format(CONNECTION_STRING, fullPath);
        }

        private DataSet GetDataSetWithRequiredName()
        {
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = _databaseName;
            return dataSet;
        }


        private IEnumerable<string> GetRequiredTablesName(OleDbConnection connection)
        {
            DataTable dataTable = connection.GetSchema(ACCESSDB_TABLES_SCHEMA);
            return dataTable.AsEnumerable()
                .Select(dataRow => dataRow.Field<string>("TABLE_NAME"))
                .Where(dataRow => !dataRow.StartsWith(ACCESSDB_SYSTEM_SCHEMA));
        }

        private string ConvertDataSetToJson(DataSet database)
        {
            return JsonConvert.SerializeObject(database,Formatting.Indented);
        }

        private const string ACCESSDB_TABLES_SCHEMA = "Tables";
        private const string ACCESSDB_SYSTEM_SCHEMA = "MSys";
        private const string CONNECTION_STRING = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};User Id=Admin;Password=";

        private string _databaseFilePath = string.Empty;
        private string _databaseName = string.Empty;
    }
}
