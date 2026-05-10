# Clothe Store API

<!-- Keep this file under 120 lines. Extract overflow content to .ai/ and link explicitly. -->

REST API for a custom clothing e-commerce platform. Handles cart management, product design customization, user addresses, and blob storage for design assets. Built with Clean Architecture and CQRS. Active development — no CI/CD pipeline yet.

## Tech stack

| Technology | Version | Notes |
|---|---|---|
| .NET | 8.0 | Web API |
| Entity Framework Core | 8.0.0 | SQL Server provider |
| Dapper | 2.1.35 | Used alongside EF for raw queries |
| SQL Server | — | Local dev instance (`PRueba_1` DB) |
| Azure AD B2C | — | JWT bearer auth (`B2C_1_sign_in_up` policy) |
| Azure Blob Storage | 12.27.0 | Two containers: `design-preview`, `design-final` |
| Mapster | 7.4.0 | DTO ↔ entity mapping |
| Swashbuckle | 6.6.2 | Swagger/OpenAPI at `/swagger` |

## Directory structure

```
ClotheStore.Api/          # Controllers, middleware, DI wiring, Program.cs
ClotheStore.Application/  # CQRS: Commands/ and Queries/ interfaces + ViewModels/
ClotheStore.Domain/       # Entities, enums, repository interfaces, IIdentityService
ClotheStore.Repository/   # EF DbContext, generic + specific repositories, EntityDataHandlers/
.ai/                      # AI context files — patterns, data model, integrations
```

## Key commands

```powershell
# Restore and build
dotnet restore
dotnet build

# Run API (dev)
dotnet run --project ClotheStore.Api

# Swagger UI
# http://localhost:<port>/swagger
```

## Critical rules

- **EntityDataHandlers own persistence logic** — do not put save/update logic in repositories or services. Each entity has a dedicated handler called by `ApplicationDbContext.SaveChangesAsync`. See [.ai/patterns.md](.ai/patterns.md).
- **CQRS split is strict** — command services mutate state, query services read state. Never mix them. New features must add both an `IXCommandService` in `Application/Commands/` and an `IXQueryService` in `Application/Queries/`.
- **All new endpoints need DI registration** — services and repositories are registered in `ClotheStore.Api/Extensions/DIExtension.cs`. Missing registration causes silent null-injection failures.
- **Auth is currently [AllowAnonymous]** — controllers have auth disabled for development. Do not ship without re-enabling `[Authorize]`.
- **`WeatherForecast.cs` is boilerplate** — do not reference or build on it; it is pending removal.

## Reference documentation

| Task | Read this file |
|---|---|
| Add a new entity end-to-end | [.ai/patterns.md](.ai/patterns.md) |
| Understand domain models and relationships | [.ai/data-model.md](.ai/data-model.md) |
| Work with Azure B2C or Blob Storage | [.ai/integrations.md](.ai/integrations.md) |
| Modify DI registration | `ClotheStore.Api/Extensions/DIExtension.cs` |
| Change EF mappings or DbSets | `ClotheStore.Repository/Context/ApplicationDbContext.cs` |
| Add action tracking exclusions | `ClotheStore.Api/Extensions/Attributes/SkipTrackingAttribute.cs` |

## Session resume

1. Read this file, then the `.ai/` file relevant to your task (see table above).
2. Run `dotnet build` — fix any errors before proceeding.
3. Check `TODO.md` for known pending work in your area.
4. The active database is a local SQL Server instance (`PRueba_1`). Connection string is in `ClotheStore.Api/appsettings.json` — do not commit credentials.
