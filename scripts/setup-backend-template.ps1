param(
    [string]$TemplateName = "Nexavor.Service.Template"
)

Write-Host "🚀 Creating Nexavor Service Template..."

# Root
New-Item -ItemType Directory -Path $TemplateName -Force | Out-Null
Set-Location $TemplateName

# ---------------------------
# CREATE PROJECTS
# ---------------------------
dotnet new webapi -n Nexavor.Service.API
dotnet new classlib -n Nexavor.Service.Application
dotnet new classlib -n Nexavor.Service.Domain
dotnet new classlib -n Nexavor.Service.Infrastructure

# ---------------------------
# CREATE SOLUTION
# ---------------------------
dotnet new sln -n Nexavor.Service

dotnet sln add .\Nexavor.Service.API\Nexavor.Service.API.csproj
dotnet sln add .\Nexavor.Service.Application\Nexavor.Service.Application.csproj
dotnet sln add .\Nexavor.Service.Domain\Nexavor.Service.Domain.csproj
dotnet sln add .\Nexavor.Service.Infrastructure\Nexavor.Service.Infrastructure.csproj

# ---------------------------
# ADD REFERENCES
# ---------------------------
dotnet add .\Nexavor.Service.API reference .\Nexavor.Service.Application
dotnet add .\Nexavor.Service.API reference .\Nexavor.Service.Infrastructure

dotnet add .\Nexavor.Service.Application reference .\Nexavor.Service.Domain
dotnet add .\Nexavor.Service.Infrastructure reference .\Nexavor.Service.Application

# ---------------------------
# ADD SAMPLE DOMAIN ENTITY
# ---------------------------
@"
namespace Nexavor.Service.Domain;

public class SampleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
"@ | Out-File .\Nexavor.Service.Domain\SampleEntity.cs

# ---------------------------
# ADD DB CONTEXT
# ---------------------------
@"
using Microsoft.EntityFrameworkCore;
using Nexavor.Service.Domain;

namespace Nexavor.Service.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<SampleEntity> Samples => Set<SampleEntity>();
}
"@ | Out-File .\Nexavor.Service.Infrastructure\AppDbContext.cs

# Add EF Core package
dotnet add .\Nexavor.Service.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add .\Nexavor.Service.Infrastructure package Microsoft.EntityFrameworkCore.InMemory

# ---------------------------
# ADD CONTROLLER
# ---------------------------
@"
using Microsoft.AspNetCore.Mvc;

namespace Nexavor.Service.API.Controllers;

[ApiController]
[Route(""api/[controller]"")]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(""Hello from Nexavor Service 🚀"");
}
"@ | # Ensure Controllers folder exists
New-Item -ItemType Directory -Path ".\Nexavor.Service.API\Controllers" -Force | Out-Null

@"
using Microsoft.AspNetCore.Mvc;

namespace Nexavor.Service.API.Controllers;

[ApiController]
[Route(""api/[controller]"")]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(""Hello from Nexavor Service 🚀"");
}
"@ | Out-File ".\Nexavor.Service.API\Controllers\SampleController.cs"

# ---------------------------
# CLEAN DEFAULT WEATHER FILES
# ---------------------------
Remove-Item .\Nexavor.Service.API\WeatherForecast.cs -ErrorAction Ignore
Remove-Item .\Nexavor.Service.API\Controllers\WeatherForecastController.cs -ErrorAction Ignore

# ---------------------------
# DOCKERFILE
# ---------------------------
# Ensure API folder exists
$apiPath = ".\Nexavor.Service.API"

New-Item -ItemType Directory -Path $apiPath -Force | Out-Null

@"
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet publish Nexavor.Service.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT [""dotnet"", ""Nexavor.Service.API.dll""]
"@ | Out-File "$apiPath\Dockerfile"

# ---------------------------
# TEMPLATE CONFIG
# ---------------------------
New-Item -ItemType Directory -Path ".template.config" -Force | Out-Null

@"
{
  ""$schema"": ""http://json.schemastore.org/template"",
  ""author"": ""Nexavor"",
  ""classifications"": [""Microservice"", ""Clean Architecture""],
  ""identity"": ""Nexavor.Service.Template"",
  ""name"": ""Nexavor Microservice"",
  ""shortName"": ""nexavor-service"",
  ""sourceName"": ""Nexavor.Service"",
  ""symbols"": {
    ""ServiceName"": {
      ""type"": ""parameter"",
      ""datatype"": ""string"",
      ""replaces"": ""Nexavor.Service""
    }
  }
}
"@ | Out-File ".template.config\template.json"

Write-Host "🚀 Setting up Nexavor Templates..."
Write-Host "✅ Template Created Successfully!"