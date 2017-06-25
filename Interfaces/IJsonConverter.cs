using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetJsonFromAccessDbCmdlet.Interfaces
{
    internal interface IJsonConverter
    {
        string ConvertToJson();
    }
}
