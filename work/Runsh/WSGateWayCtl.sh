#!/bin/bash

nohup dotnet ~/work/GateWays/WSGateWay/WSGateWay.dll --urls="http://*:6000" --Rabbitmq:Queue="WSGateWay-1" >> /dev/null