@echo off

:: Pull SQL Server image
docker pull mcr.microsoft.com/mssql/server

:: Run a SQL Server container
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MatrixReloaded!" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest