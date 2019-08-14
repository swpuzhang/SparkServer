start dotnet publish -c Release GateWays\ApiGateWay\ApiGateWay.csproj -o work\GateWays\ApiGateWay
TIMEOUT /T 5
start dotnet publish -c Release GateWays\WSGateWay\WSGateWay.csproj -o work\GateWays\WSGateWay
TIMEOUT /T 5
start dotnet publish -c Release Services\Account\Account.WebApi\Account.WebApi.csproj -o work\Services\Account
TIMEOUT /T 5
start dotnet publish -c Release Services\Money\Money.WebApi\Money.WebApi.csproj -o work\Services\Money
TIMEOUT /T 5
start dotnet publish -c Release Services\Sangong\Sangong.Game.WebApi\Sangong.Game.WebApi.csproj -o work\Services\Sangong\SangongGame
TIMEOUT /T 5
start dotnet publish -c Release Services\Sangong\Sangong.Matching.WebApi\Sangong.Matching.WebApi.csproj -o work\Services\Sangong\SangongMatching