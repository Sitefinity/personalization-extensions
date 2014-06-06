$machineName = gc env:computername
$currentPath = Split-Path $script:MyInvocation.MyCommand.Path

$doc = New-Object System.Xml.XmlDocument
$doc.Load($currentPath + "\Variables.xml") 

#Modules
$iisModule = Join-Path $currentPath "\IIS.ps1"
$sqlModule = Join-Path $currentPath "\SQL.ps1"
$functionsModule = Join-Path $currentPath "\Functions.ps1"
$sitefinitySetup = Join-Path $currentPath "\SitefinitySetup.ps1"
$cleanup = Join-Path $currentPath "\Cleanup.ps1"
 
#Website setup properties

$siteName = $doc.SelectSingleNode("//variables/siteName").InnerText
$websiteUrl = $doc.SelectSingleNode("//variables/websiteUrl").InnerText
$websitePort = $doc.SelectSingleNode("//variables/websitePort").InnerText
$projectBackupLocation = $doc.SelectSingleNode("//variables/projectBackupLocation").InnerText
$projectBuildLocation = $doc.SelectSingleNode("//variables/projectBuildLocation").InnerText
$projectBuildName = $doc.SelectSingleNode("//variables/projectBuildName").InnerText
$databaseBackupLocation = $doc.SelectSingleNode("//variables/databaseBackupLocation").InnerText
$databaseBackupName = $doc.SelectSingleNode("//variables/databaseBackupName").InnerText

$databaseServer = $doc.SelectSingleNode("//variables/databaseServer").InnerText
$appPollName = $siteName
$databaseName = $siteName

$aspNetTempFolder = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Temporary ASP.NET Files\root"
$sqlCmdExe = "C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE" 
$msBuildExe64 = "C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
$msBuildExe32 = "C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"