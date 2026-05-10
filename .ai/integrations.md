# Integrations

## Azure AD B2C (authentication)

- **Instance**: `https://clothestore.b2clogin.com`
- **Tenant**: `clothestore.onmicrosoft.com` (TenantId: `3a099a37-...`)
- **Client ID**: `026c53c9-4a85-43d6-8c1e-d7aa4746002e`
- **Policy**: `B2C_1_sign_in_up`
- **Required scope**: `tasks.read`

Configured in `Program.cs` via `AddMicrosoftIdentityWebApiAuthentication`. Configuration values are bound from the `AzureAdB2C` section in `appsettings.json`.

**Identity extraction**: `ClotheStore.Application/Services/IdentityService.cs` reads the user object ID from the claim `http://schemas.microsoft.com/identity/claims/objectidentifier`. Inject `IIdentityService` to get the current user ID in any service.

**Current state**: All controllers are decorated with `[AllowAnonymous]`. Auth must be re-enabled (`[Authorize]`) before production deployment.

## Azure Blob Storage

- **Account**: `privatedesign`
- **Authentication**: `DefaultAzureCredential` (uses managed identity in production; local dev uses Azure CLI login)
- **Containers**:
  - `design-preview` — temporary previews, not yet confirmed by user
  - `design-final` — confirmed, permanent design assets

Configuration bound from the `AzureStorage` section in `appsettings.json`. The `ConnectionString` value in `appsettings.json` is sensitive — never commit it. Use environment variables or Azure Key Vault in non-dev environments.

Blob operations are exposed via:
- `Application/Queries/IBlobQueryService.cs` — read/list blobs
- `ClotheStore.Api/Controllers/BlobController.cs` — upload/download endpoints

## SQL Server (local dev)

- **Server**: `DESKTOP-UB146BB\DESKTOPUB146BB` (local named instance)
- **Database**: `PRueba_1`
- **Connection string key**: `ConnectionStrings:Default` in `appsettings.json`

The connection string in `appsettings.json` is a local dev credential. Do not commit production credentials. Use `appsettings.Development.json` for dev overrides that are gitignored, or use environment variables.

Query/command telemetry is captured by `ClotheStore.Repository/Repositories/Interceptor/QueryCommandInterceptor.cs` — it logs all EF Core SQL operations.
