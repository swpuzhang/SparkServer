start dotnet publish -c Release GateWays\ApiGateWay\ApiGateWay.csproj -o ..\..\work\GateWays\ApiGateWay
TIMEOUT /T 2
start dotnet publish -c Release GateWays\WSGateWay\WSGateWay.csproj -o ..\..\work\GateWays\WSGateWay
TIMEOUT /T 2
start dotnet publish -c Release Services\Account\Account.WebApi\Account.WebApi.csproj -o ..\..\..\work\Services\Account
TIMEOUT /T 2
start dotnet publish -c Release Services\Money\Money.WebApi\Money.WebApi.csproj -o ..\..\..\work\Services\Money
TIMEOUT /T 2
start dotnet publish -c Release Services\Sangong\Sangong.Game.WebApi\Sangong.Game.WebApi.csproj -o ..\..\..\work\Services\Sangong\SangongGame
TIMEOUT /T 2
start dotnet publish -c Release Services\Sangong\Sangong.Matching.WebApi\Sangong.Matching.WebApi.csproj -o ..\..\..\work\Services\Sangong\SangongMatching
TIMEOUT /T 2
start dotnet publish -c Release Services\Reward\Reward.WebApi\Reward.WebApi.csproj -o ..\..\..\work\Services\Reward\
TIMEOUT /T 2
start dotnet publish -c Release Services\Friend\Friend.WebApi\Friend.WebApi.csproj -o ..\..\..\work\Services\Friend\
TIMEOUT /T 2
start dotnet publish -c Release Services\MsgCenter\MsgCenter.WebApi\MsgCenter.WebApi.csproj -o ..\..\..\work\Services\MsgCenter\
TIMEOUT /T 2
start dotnet publish -c Release Services\ServerLog\ServerLog.WebApi\ServerLog.WebApi.csproj -o ..\..\..\work\Services\ServerLog\
TIMEOUT /T 2
start dotnet publish -c Release GateWays\InterfaceDemo\InterfaceDemo.csproj -o ..\..\work\GateWays\InterfaceDemo
