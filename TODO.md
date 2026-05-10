# Project TODO

Tasks that need doing but aren't yet in the sprint backlog.
When picking up an item, move it to a ticket and remove it here.

## Auth

- [ ] Re-enable `[Authorize]` on all controllers (currently `[AllowAnonymous]` for dev)
- [ ] Validate B2C scopes per endpoint

## Cleanup

- [ ] Remove `WeatherForecast.cs` boilerplate from `ClotheStore.Api`
- [ ] Evaluate and remove `EntityFramework 6.5.1` dependency from `ClotheStore.Repository.csproj` if unused

## Infrastructure

- [ ] Add CI/CD pipeline (`.github/workflows/` is empty)
- [ ] Move secrets out of `appsettings.json` (Azure Blob connection string, DB credentials) to Key Vault or environment variables
- [ ] Set up a dev database migration strategy (no EF migrations folder found)

## Testing

- [ ] Add unit tests project targeting `ClotheStore.Application`
- [ ] Add integration tests for repository layer

## Cart

- [ ] Implement cart controller (no `CartController` exists — `ICartCommandService` and `ICartQueryService` are defined but not exposed via HTTP)
