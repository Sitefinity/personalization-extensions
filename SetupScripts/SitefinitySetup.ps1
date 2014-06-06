Import-Module WebAdministration

$currentPath = Split-Path $script:MyInvocation.MyCommand.Path
$variables = Join-Path $currentPath "\Variables.ps1"
. $variables
. $iisModule
. $sqlModule
. $functionsModule
. $cleanup

write-output "------- Installing Sitefinity --------"

ProjectCleanup

write-output "Setting up Application pool..."

New-WebAppPool $appPollName -Force

Set-ItemProperty IIS:\AppPools\$appPollName managedRuntimeVersion v4.0 -Force

#Setting application pool identity to NetworkService
Set-ItemProperty IIS:\AppPools\$appPollName processmodel.identityType -Value 2 

write-output "Deploy SitefinityWebApp to test execution machine $machineName"

if (Test-Path $projectBuildLocation){
	CleanWebsiteDirectory $projectBuildLocation 10 $appPollName
}  

write-output "Sitefinity deploying from $projectBuildLocation..."

Copy-Item -Path $projectBackupLocation $projectBuildLocation -Recurse -ErrorAction stop

write-output "Sitefinity successfully deployed."

function InstallSitefinity()
{
    $sitefinityWebAppSolution = $projectBuildLocation +"\"+ $projectBuildName
    
    write-output "Building Sitefinity solution"
    BuildSolution $sitefinityWebAppSolution
    
    AttachDatabase $databaseServer $databaseName $databaseBackupName $databaseBackupLocation
    
	$siteId = GetNextWebsiteId
	write-output "Registering $siteName website with id $siteId in IIS."
	New-WebSite -Id $siteId -Name $siteName -Port $websitePort -HostHeader localhost -PhysicalPath $projectBuildLocation -ApplicationPool $appPollName -Force
	Start-WebSite -Name $siteName

	write-output "Setting up Sitefinity..."

	$installed = $false

	while(!$installed){
		try{    
			$response = GetRequest $websiteUrl
			if($response.StatusCode -eq "OK"){
				$installed = $true;
				$response
			}
		}catch [Exception]{
			Restart-WebAppPool $appPollName -ErrorAction Continue
			write-output "$_.Exception.Message"
			$installed = $false
		}
	}

	write-output "----- Sitefinity successfully installed ------"
}

function BuildSolution($slnFile)
{
    $BuildArgs = @{
         FilePath = $msBuildExe64
         ArgumentList = $slnFile, "/t:Build"
         RedirectStandardOutput = $true
         Wait = $true
     }
     
    Start-Process @BuildArgs
}
