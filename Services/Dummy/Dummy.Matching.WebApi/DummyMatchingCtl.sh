#!/bin/bash
dotnet Dummy.Matching.WebApi.dll --urls="http://*:9000" --MatchingGroup="1" --Rabbitmq:Queue="DummyMatching-1"