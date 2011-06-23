###########################################################################################################################
# Remove the nuGet packages from the projects, so we can get the binaries
###########################################################################################################################
.\RemoveRequiredNuGetPackages.ps1

###########################################################################################################################
# Install the nuGet packages for the projects
###########################################################################################################################
Write-Host "Installing nuGet packages.... "
Install-Package SqlServerCompact -Project MileageStats.Data.SqlCe
Install-Package SqlServerCompact -Project MileageStats.Data.SqlCe.tests

Install-Package EntityFramework -Project MileageStats.Data.SqlCe
Install-Package EntityFramework -Project MileageStats.Data.SqlCe.Tests

Install-Package DotNetOpenAuth -Project MileageStats.Web
Install-Package DotNetOpenAuth -Project MileageStats.Web.Tests

Write-Host "Done installing nuGet packages.... "