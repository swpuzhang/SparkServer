#!/bin/bash
dotnet GameSample.Matching.WebApi.dll --urls="http://*:9000" --MatchingGroup="1" --Rabbitmq:Queue="GameSampleMatching-1"