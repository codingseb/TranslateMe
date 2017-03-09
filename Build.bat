@echo Off
REM set config=%1
REM if "%config%" == "" (
   REM set config=Release
REM )
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

%nuget% restore TranslateMe.sln

"C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild" TranslateMe.sln /p:Configuration="Release" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

%nuget% pack "TranslateMe.nuspec" -NoPackageAnalysis -verbosity detailed -Version %version% -p Configuration="Release"
REM %nuget% pack TranslateMe.WPF\TranslateMeWPF.csproj -IncludeReferencedProjects -Build -Version %version% -p Configuration="%config%"

PAUSE