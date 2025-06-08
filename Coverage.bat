dotnet test --test-adapter-path BankSystem.Tests/bank.system.Tests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=Coverage/ /p:excludebyattribute=*.ExcludeFromCodeCoverage*

%USERPROFILE%\.nuget\packages\reportgenerator\5.4.7\tools\net9.0\ReportGenerator.exe "-reports:BankSystem.Tests/Coverage/coverage.opencover.xml" "-targetdir:BankSystem.Tests/Coverage"
pause