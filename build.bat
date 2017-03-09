@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

call %nuget% restore TranslateMe.sln

call "%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" TranslateMe.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

call %nuget% pack "TranslateMe.nuspec" -NoPackageAnalysis -o Build -verbosity detailed -Version %version% -p Configuration="%config%"

PAUSE