@echo off

REM Creating a Newline variable (the two blank lines are required!)
set NLM=^


set NL=^^^%NLM%%NLM%^%NLM%%NLM%
echo [91m
echo [1mWARNING! [0m[91mThis script will install or/and[1m UPGRADE[0m following: 
echo [0m
echo Chocolatey^
::%NLM%NodeJS^
::%NLM%Docker^
%NLM%NPM^
%NLM%dotnet
echo [91m[1m

SET /P AREYOUSURE=Are you sure you want to upgrade all this (Y/[N])?
IF /I "%AREYOUSURE%" NEQ "Y" GOTO END
echo [0m


REM Install chocolatey
REM ------------------
SET DIR=%~dp0%

::download install.ps1
%systemroot%\System32\WindowsPowerShell\v1.0\powershell.exe -NoProfile -ExecutionPolicy Bypass -Command "((new-object net.webclient).DownloadFile('https://chocolatey.org/install.ps1','%DIR%install.ps1'))"
::run installer
%systemroot%\System32\WindowsPowerShell\v1.0\powershell.exe -NoProfile -ExecutionPolicy Bypass -Command "& '%DIR%install.ps1' %*"
del install.ps1

taskkill /f /im explorer.exe && explorer.exe
ping localhost -n 1

choco feature enable -n allowGlobalConfirmation
choco upgrade chocolatey
call  refreshenv
               
echo Installing pre-requisites

cinst dotnet-6.0-sdk
cup dotnet-6.0-sdk
call refreshenv
dotnet tool install --global dotnet-ef

::cinst docker-desktop
::call  refreshenv

::choco upgrade docker-desktop
::call  refreshenv

::cinst nodejs-lts
::call  refreshenv
::choco upgrade nodejs-lts
::call npm install -g npm
call refreshenv


:END