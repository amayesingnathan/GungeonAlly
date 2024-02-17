#!/bin/bash

echo "Checking for init_complete file.";

if [ ! -f /init/init_complete ]; then
  echo "Running database build script.";
  /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P $1 -d master -i /init/init.sql;
  
  echo "Creating init_complete file.";
  touch /init/init_complete;
fi