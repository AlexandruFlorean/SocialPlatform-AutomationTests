@echo off
set CONNECTION="Server=localhost,60991;Database=social_platform;User Id=sa;Password=P@rola!@#4;TrustServerCertificate=True;"

echo Scaffolding Database Context...

dotnet ef dbcontext scaffold %CONNECTION% Microsoft.EntityFrameworkCore.SqlServer ^
    --output-dir DatabaseContext ^
    --force ^
    --no-onconfiguring ^
    --context SocialPlatformDbContext ^
    --namespace SocialPlatform.Tests.Common.DatabaseContext ^
    --verbose
     
if %ERRORLEVEL% EQU 0 (
    echo.
    echo Scaffolding SUCCESSful!
) else (
    echo.
    echo Scaffolding FAILED!
)
pause
