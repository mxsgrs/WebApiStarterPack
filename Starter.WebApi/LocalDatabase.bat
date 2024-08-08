@echo off

:: Pull SQL Server image
docker pull mxsgrs/startedb:v1.0.0

:: Run a SQL Server container
docker run -p 1433:1433 --name starterdb -d mxsgrs/starterdb:v1.0.0