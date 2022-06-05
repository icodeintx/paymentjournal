
#Remove the 'publish' folder if it exists
Remove-Item publish -Recurse
Write-Output "Removing existing publish folder"

#Use DotNet to make a publish release in the 'publish' folder
dotnet publish -c Release -o ./publish
Write-Output "Writing publish files to the publish folder"

#backup the application settings files
Copy-Item C:\OneDrive\Personal\AmaraCode\production\apps\PaymentJournal\appsettings.* C:\OneDrive\Personal\AmaraCode\production\apps\PaymentJournal\bak
Write-Output "Application Settings files backed up"

#copies all files from the publish folder to the destination folder overwriting files as needed.
Copy-Item -Path ".\publish\*" -Destination "C:\OneDrive\Personal\AmaraCode\production\apps\PaymentJournal\" -Force -Recurse
Write-Output "All files copied"

#replace the application settings files with the originals
Copy-Item C:\OneDrive\Personal\AmaraCode\production\apps\PaymentJournal\bak\appsettings.* C:\OneDrive\Personal\AmaraCode\production\apps\PaymentJournal\
Write-Output "Application files replaced"
