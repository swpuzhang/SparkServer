#!/bin/bash
dotnet GameSample.Game.WebApi.dll --urls="http://*:10000" --MatchingGroup="1" --Rabbitmq:Queue="GameSampleGame-1-1" --Rabbitmq:Mathcing="GameSampleMatching-1"