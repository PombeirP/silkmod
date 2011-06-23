###########################################################################################################################
# Remove the nuGet packages from the projects, so we can get the binaries
###########################################################################################################################
Write-Host "Removing nuGet packages.... "
Write-Host "Depending on your system, there may be errors in the removal of these nuGet packages."
Write-Host "Please ignore errors for a moment."

Uninstall-Package SqlServerCompact -Project MileageStats.Data.SqlCe -RemoveDependencies -Force
Uninstall-Package SqlServerCompact -Project MileageStats.Data.SqlCe.tests -RemoveDependencies -Force

Uninstall-Package EntityFramework -Project MileageStats.Data.SqlCe -RemoveDependencies -Force
Uninstall-Package EntityFramework -Project MileageStats.Data.SqlCe.Tests -RemoveDependencies -Force

Uninstall-Package DotNetOpenAuth -Project MileageStats.Web -RemoveDependencies -Force
Uninstall-Package DotNetOpenAuth -Project MileageStats.Web.Tests -RemoveDependencies -Force

Write-Host "Done removing packages.  Please pay attention to errors now."
