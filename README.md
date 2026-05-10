# Clothe Store API

REST API for a custom clothing e-commerce platform with cart management and design customization.

## What this is

The Clothe Store API powers an e-commerce system where customers can browse clothing, customize designs (colors, sizes, fonts), manage their cart, and save delivery addresses. It handles authentication via Azure AD B2C and stores design assets in Azure Blob Storage. The project is in active development; no production deployment exists yet.

## Tech stack

| Technology | Notes |
|---|---|
| .NET 8.0 | Web API, C# |
| SQL Server | Local dev instance; EF Core 8 + Dapper |
| Azure AD B2C | JWT bearer authentication |
| Azure Blob Storage | Design preview and final asset storage |
| Mapster | DTO mapping |
| Swagger | API docs at `/swagger` |

## Setup

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Install SQL Server (local or Docker) and create a database named `PRueba_1`
3. Update the connection string in `ClotheStore.Api/appsettings.json` to point to your SQL Server instance
4. Install dependencies:
   ```
   dotnet restore
   ```
5. Build the solution:
   ```
   dotnet build
   ```

## Key commands

```powershell
# Run the API
dotnet run --project ClotheStore.Api

# Build all projects
dotnet build

# Restore packages
dotnet restore
```

Swagger UI is available at `http://localhost:<port>/swagger` when running in development.

## Project structure

```
ClotheStore.Api/          # HTTP layer: controllers, middleware, DI wiring
ClotheStore.Application/  # Business logic: CQRS command/query services, DTOs
ClotheStore.Domain/       # Core: entities, enums, repository interfaces
ClotheStore.Repository/   # Data access: EF DbContext, repositories, entity handlers
.ai/                      # AI session context (not for human readers — see CLAUDE.md)
```

## Contributing

- Branch from `main` and target `main` for PRs.
- No CI pipeline is configured yet — run `dotnet build` and fix all warnings before opening a PR.
- Auth is currently disabled (`[AllowAnonymous]`) — do not ship endpoints without re-enabling `[Authorize]`.
- Do not commit secrets — connection strings and storage credentials must stay out of source control.
