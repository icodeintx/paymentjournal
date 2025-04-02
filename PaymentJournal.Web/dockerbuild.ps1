$ErrorActionPreference = "Stop"

$projectName = "PaymentJournal.Web"
$imageName = "paymentjournal"
$containerName = "paymentjournal-container"

Write-Host "Cleaning bin/obj..."
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue .\bin, .\obj

if (-not (Test-Path "publish")) { New-Item -ItemType Directory -Path "publish" | Out-Null }


# Write-Host "Publishing $projectName..."
dotnet publish "$projectName.csproj" -c Release -o ./out

Write-Host "Building Docker image: $imageName"
# docker build --no-cache -t $imageName .
docker build --no-cache -t paymentjournal .

Write-Host "Creating container: $containerName (but not running it)..."

# Delete any existing stopped container with the same name
$existing = docker ps -a --filter "name=$containerName" --format "{{.ID}}"
if ($existing) {
    Write-Host "Removing old container..."
    docker rm $containerName | Out-Null
}

# Create container (but don’t start it yet)
docker create --name $containerName -p 8080:8080 -v C:\OneDrive\Production\apps\PaymentJournal\Data\litedb.db:/app/litedb.db $imageName

Write-Host "`Done. Use 'docker start $containerName' to run it."
