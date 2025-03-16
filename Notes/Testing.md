




# TESTING

Stride uses xunit
Example:
https://doc.stride3d.net/latest/en/manual/troubleshooting/unit-tests.html

How to name tests:
https://enterprisecraftsmanship.com/posts/you-naming-tests-wrong/


cd into the Stride Solution Level

TUNIT

dotnet new install TUnit.Templates

dotnet new TUnit -n "PROJECTNAME.Tests"

Remove example, there should only be the csproj leftover.

Change Target Framework in .csproj to this as Stride is only Windows.
<TargetFramework>net8.0-windows</TargetFramework>

dotnet add ./PROJECTNAME.Tests/PROJECTNAME.Tests.csproj reference ./PROJECTNAME/PROJECTNAME.csproj

Install the extension Name: C# Dev Kit
Go to the C# Dev Kit extension's settings
Enable Dotnet > Test Window > Use Testing Platform Protocol

use dotnet run

dotnet new TUnit -n "PinballStride.Tests"

dotnet add ./PinballStrideTUnit.Tests/PinballStrideTUnit.Tests.csproj reference ./PinballStride/PinballStride.csproj






TUnit can select tests by:

Assembly
Namespace
Class name
Test name
https://thomhurst.github.io/TUnit/docs/tutorial-extras/test-filters



