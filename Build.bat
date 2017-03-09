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

%nuget% restore TranslateMe.sln

"C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild" TranslateMe.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

%nuget% pack "TranslateMe.nuspec" -NoPackageAnalysis -verbosity detailed -Version %version% -p Configuration="%config%"
REM %nuget% pack TranslateMe.WPF\TranslateMeWPF.csproj -IncludeReferencedProjects -Build -Version %version% -p Configuration="%config%"

PAUSE