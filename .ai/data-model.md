# Data model

All entities are defined in `ClotheStore.Domain/Models/`. DbSets and EF mappings are in `ClotheStore.Repository/Context/ApplicationDbContext.cs`.

## Core entities

| Entity | Domain folder | DbSet name | Notes |
|---|---|---|---|
| Address | `Models/Address/` | `Address` | User shipping/billing address |
| ContactPreference | `Models/ContactPreference/` | `ContactPreference` | User notification preferences |
| CartItem | `Models/Item/` | `CartItem` | Line item in a cart |
| Customization | `Models/Customization/` | `Customization` | Design customization applied to a cart item |
| Image | `Models/Blob/` | `Image` | Blob reference for design assets |
| Design | `Models/Design/` | `Design` | A saved design template |
| OptionColor | `Models/Option/` | `OptionColor` | Color option for a design |
| OptionSize | `Models/Option/` | `OptionSize` | Size option |
| OptionFont | `Models/Option/` | `OptionFont` | Font option |
| User | `Models/User/` | — | Managed via Azure B2C; local user record minimal |
| Tracking/SiteInteraction | `Models/Tracking/` | — | Written by `TrackActionMiddleware`; not a DbSet — inserted via Dapper or raw SQL |

## Relationships (inferred from repositories and handlers)

- A **Cart** aggregates multiple **CartItems**.
- A **CartItem** references a **Design** and may have one or more **Customizations**.
- A **Customization** references an **Image** (blob asset).
- A **Design** has associated **OptionColor**, **OptionSize**, and **OptionFont** records.
- A **User** has one or more **Addresses** and one **ContactPreference**.

## Enums

Defined in `ClotheStore.Domain/Enum/`. Read this folder before adding new status or type fields.

## Validation

Domain-level validation lives in `ClotheStore.Domain/Models/Validations/`. Add new validation rules here, not in controllers or services.

## AppSettings binding

`ClotheStore.Domain/Core/AppSettings.cs` defines the strongly-typed configuration model. Add new configuration sections here and bind in `Program.cs`.
