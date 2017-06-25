using System.IO;

namespace GetJsonFromAccessDbCmdlet.Helpers
{
    static class FileHelper
    {   
        internal static bool FileExists(string filePath)
        {
            return File.Exists(Path.GetFullPath(filePath));
        }

        internal static void SaveToFile(string text, string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName,false))
            {
                file.WriteLine(text);
            }
        }
    }
}
