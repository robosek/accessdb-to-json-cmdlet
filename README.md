# accessdb-to-json-cmdlet
Powershell commandlet for converting Access-database to JSON format

## Usage 
- <strong> !! Important !! You have to use x86 version of Powershell </strong>
- Build application in VS
- Go to build directory
- Open PowerShell x86
- `Import-Module .\GetJsonFromAccessDbCmdlet.dll`
- `Get-JsonFromAccessDb -AccessDbFilePath "./someaccessdb.accdb"`
- If you want to save output to file also, use optional parameter `-SaveOutputToFile 1`

