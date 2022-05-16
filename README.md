# Planning Poker Web API
An ASP.NET Core Web API .NET 6 application that serves as backend part for Planning Poker app.  
SQL database set up in Docker. Endpoints are displayed with Swagger.

## How to run the application
1. Download or clone repository from GitHub.
2. Navigate to [scripts](scripts) directory.
3. Run the [setup script](scripts/setup-developer-pc.bat) (installs Chocolatey, Docker, .NET 6.0 SDK).
4. While in [scripts](scripts) directory, run the following command from the command line  
(sets up SQL database in Docker):
```cmd
docker compose up
```
5. Navigate to [PlanningPoker.ClientApi](PlanningPoker.ClientApi) directory.
6. Run the following command from the command line:
```cmd
dotnet run --project PlanningPoker.ClientApi.csproj
```
7. To see the endpoints in Swagger, open the generated localhost URL, e.g.:
```
https://localhost:7281
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
and modify it by adding on the end:
```
/swagger
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
It should reformat automatically and look similar to this:
```
https://localhost:7281/swagger/index.html
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Application's Swagger page should be opened:

![screenshot](/scr.png "swagger-endpoints")