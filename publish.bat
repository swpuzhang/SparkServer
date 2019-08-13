start dotnet publish -c Release E:\document\mycode\project\Spark\GateWays\ApiGateWay\ApiGateWay.csproj -o E:\document\mycode\project\Spark\work\GateWays\ApiGateWay
TIMEOUT /T 5
start dotnet publish -c Release E:\document\mycode\project\Spark\GateWays\WSGateWay\WSGateWay.csproj -o E:\document\mycode\project\Spark\work\GateWays\WSGateWay
TIMEOUT /T 5
start dotnet publish -c Release E:\document\mycode\project\Spark\Services\Account\Account.WebApi\Account.WebApi.csproj -o E:\document\mycode\project\Spark\work\Services\Account
TIMEOUT /T 5
start dotnet publish -c Release E:\document\mycode\project\Spark\Services\Money\Money.WebApi\Money.WebApi.csproj -o E:\document\mycode\project\Spark\work\Services\Money
TIMEOUT /T 5
start dotnet publish -c Release E:\document\mycode\project\Spark\Services\Sangong\Sangong.Game.WebApi\Sangong.Game.WebApi.csproj -o E:\document\mycode\project\Spark\work\Services\Sangong\SangongGame
TIMEOUT /T 5
start dotnet publish -c Release E:\document\mycode\project\Spark\Services\Sangong\Sangong.Matching.WebApi\Sangong.Matching.WebApi.csproj -o E:\document\mycode\project\Spark\work\Services\Sangong\SangongMatching