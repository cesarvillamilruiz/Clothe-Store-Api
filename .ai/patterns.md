# Patterns

Org-level standards (naming, SOLID, security) are enforced via hooks. These patterns are specific to this project and extend those standards.

## Clean Architecture layer rules

Dependencies flow inward only: `Api → Application → Domain ← Repository`.
- `Domain` has zero external NuGet dependencies — keep it that way.
- `Application` defines interfaces; `Repository` and `Api` implement them.
- `Repository` never references `Application` directly.

## CQRS pattern

Every domain area has two service interface pairs:

```
Application/Commands/IXCommandService.cs   — write operations
Application/Queries/IXQueryService.cs      — read operations
```

Implementations live in `ClotheStore.Repository/` (or a future dedicated project). Register both in `DIExtension.cs` as Scoped. See `ClotheStore.Api/Extensions/DIExtension.cs` for existing registrations.

## EntityDataHandler pattern (critical)

All entity persistence logic lives in `ClotheStore.Repository/EntityDataHandlers/`. Each entity has its own handler (e.g., `CartItemEntityDataHandler`).

How it works:
1. Controller calls a command service method.
2. Command service calls `UnitOfWork.SaveChangesAsync()`.
3. `ApplicationDbContext.SaveChangesAsync()` detects changed entities and delegates to the matching `EntityDataHandler`.
4. Handler executes the actual SQL/EF operations inside the active `TransactionScope`.

**Never put save logic in a repository method or service directly.** If you bypass the handler, you skip transaction management and the interceptor.

Reference: `ClotheStore.Repository/Context/ApplicationDbContext.cs` and `ClotheStore.Repository/EntityDataHandlers/`.

## Repository pattern

`GenericRepository<T>` (`ClotheStore.Repository/Repositories/GenericRepository.cs`) provides base CRUD. Specific repositories extend it for domain queries (e.g., `DesignRepository`).

Expose repositories only through `IUnitOfWork` — never inject a repository directly into a controller.

## Unit of Work

`UnitOfWork` (`ClotheStore.Repository/Context/UnitOfWork.cs`) is the single entry point for all data access from the Application layer. It aggregates all repositories and exposes `SaveChangesAsync()`.

## Action tracking middleware

`TrackActionMiddleware` logs every request to the `SiteInteraction` table. To exclude an endpoint from tracking, decorate the controller action with `[SkipTracking]` (defined in `ClotheStore.Api/Extensions/Attributes/SkipTrackingAttribute.cs`).

## Mapster mapping

DTOs (ViewModels) are mapped from/to entities using Mapster. No manual mapping code — Mapster convention-based mapping handles property name matches. Custom mappings are configured at startup. Reference: `ClotheStore.Application/ViewModels/`.

## Legacy patterns

- `EntityFramework 6.5.1` is listed as a dependency in `ClotheStore.Repository.csproj` alongside EF Core 8. This appears to be a leftover dependency — do not use EF6 APIs in new code. All new code uses EF Core 8.
- `WeatherForecast.cs` in `ClotheStore.Api` is boilerplate — pending removal, do not reference.
