#!/bin/bash
dotnet Dummy.Game.WebApi.dll --urls="http://*:10000" --MatchingGroup="1" --Rabbitmq:Queue="DummyGame-1-1" --Rabbitmq:Mathcing="DummyMatching-1"