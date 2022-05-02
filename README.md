# Planning Poker Web API
An ASP.NET Core Web API .NET 6 application that serves as backend part for Planning Poker app.

## How to run the application
1. Run the [setup script](script/setup-developer-pc.bat) (installs Chocolatey, .NET 6.0 SDK).
2. Navigate to [PlanningPoker.ClientApi](PlanningPoker.ClientApi) directory.
3. Run the following command from the command line:
```cmd
dotnet run --project PlanningPoker.ClientApi.csproj
```
4. To see the endpoints in Swagger, open the generated localhost URL, e.g.:
```
https://localhost:7281
```
and modify it by adding on the end:
```
/swagger
```
It should reformat automatically and look something like this:
```
https://localhost:7281/swagger/index.html
```