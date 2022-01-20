docker-compose -f src\docker-compose.yml -f src\docker-compose.override.yml up -d
Powershell.exe -File dynamodb-init.ps1
@echo off
start "" http://localhost/swagger/index.html
PAUSE