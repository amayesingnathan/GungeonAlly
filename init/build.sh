#!/bin/bash

echo "Checking for build_complete file.";

if [ ! -f /init/build_complete ]; then
  echo "Running database build script";
  dotnet /app/GungeonAlly.WebScraper.dll;
  
  echo "Creating build_complete file.";
  touch /init/build_complete;
fi