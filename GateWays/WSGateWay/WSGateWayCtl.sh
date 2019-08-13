#!/bin/bash
dotnet WSGateWay.dll --urls="http://*:6000" --Rabbitmq:Queue="WSGateWay-1"