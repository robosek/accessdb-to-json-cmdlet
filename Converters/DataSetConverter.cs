using GetJsonFromAccessDbCmdlet.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetJsonFromAccessDbCmdlet.Converters
{
    internal class DataSetConverter : IJsonConverter
    {
        public DataSetConverter(DataSet data)
        {
            _data = data;
        }
        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(_data, Formatting.Indented);
        }

        private DataSet _data;
    }
}
