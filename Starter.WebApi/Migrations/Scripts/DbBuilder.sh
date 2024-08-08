#!/bin/bash
# Parameters
SQLCMD_PATH="/opt/mssql-tools18/bin/sqlcmd"
SERVER="localhost"
USERNAME="sa"
PASSWORD="B1q22MPXUgosXiqZ"
DATABASE="Starter"
# Execution
echo "Waiting for SQL Server to start"
sleep 30s
echo "Running DbCreation.sql"
$SQLCMD_PATH -C -S $SERVER -U $USERNAME -P $PASSWORD -d master -i /usr/src/app/DbCreation.sql
echo "Running InitialCreate.sql"
$SQLCMD_PATH -C -S $SERVER -U $USERNAME -P $PASSWORD -d $DATABASE -i /usr/src/app/InitialCreate.sql