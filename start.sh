#!/usr/bin/bash

echo **Bygger prosjekt**
dotnet build
echo **Starter service som deamon med logging**
screen -dmS goalStatsTrackerService bash -c 'dotnet run --no-build >> /var/log/service/goalStatsTrackerService.log 2>> /var/log/service/goalStatsTrakcerService.err'
