




dotnet new sln -o FirstHourLibrarySolution

dotnet new classlib -o FirstHourLibrary

dotnet sln add./FirstHourLibrary/FirstHourLibrary.csproj

dotnet new xunit -o FirstHourLibrary.Tests

dotnet sln add ./FirstHourLibrary.Tests/FirstHourLibrary.Tests.csproj

dotnet add ./FirstHourLibrary.Tests/FirstHourLibrary.Tests.csproj reference ./FirstHourLibrary/FirstHourLibrary.csproj

create .editorconfig file





















