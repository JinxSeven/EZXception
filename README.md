<h1 align="center">
    <a href="https://amplication.com/#gh-light-mode-only">
        <img src="./Assets/EZX-Ribbon.png" height="100px">
    </a>
</h1>

<p align="center">
  <i align="center">Never write another custom exception class ever again</i>
</p>

# EZXception

A comprehensive .NET library of pre-built custom exceptions covering every common scenario — so you never have to write a custom exception class from scratch again.

## Compatibility

Targets **`netstandard2.0`** — works on:

- .NET 8, 9, 10+
- .NET Framework 4.6.1+
- Mono, Xamarin, Unity

## Installation

```bash
dotnet add package EZXception
```

## Namespaces & Exceptions

### `EZXception.Validation`

| Exception                  | When to use                                              |
| -------------------------- | -------------------------------------------------------- |
| `ValidationException`      | One or more validation rules failed                      |
| `RequiredFieldException`   | A required field is null or empty                        |
| `InvalidFormatException`   | Value doesn't match expected format (email, phone, etc.) |
| `ValueOutOfRangeException` | Numeric/date value outside allowed range                 |
| `DuplicateValueException`  | A value already exists where uniqueness is required      |
| `StringLengthException`    | String too short or too long                             |

### `EZXception.Authorization`

| Exception                     | When to use                               |
| ----------------------------- | ----------------------------------------- |
| `UnauthorizedException`       | User is not authenticated (HTTP 401)      |
| `ForbiddenException`          | User lacks permission (HTTP 403)          |
| `TokenExpiredException`       | JWT/session/API key has expired           |
| `InvalidCredentialsException` | Wrong username/password/API key           |
| `AccountLockedException`      | Account locked after failures or by admin |
| `InsufficientRoleException`   | User's role doesn't meet requirements     |

### `EZXception.Data`

| Exception                        | When to use                                  |
| -------------------------------- | -------------------------------------------- |
| `EntityNotFoundException`        | Record not found in data store (HTTP 404)    |
| `DuplicateEntityException`       | Entity already exists (HTTP 409)             |
| `DataIntegrityException`         | DB constraint violated (FK, unique, check)   |
| `OptimisticConcurrencyException` | Row was modified between read and write      |
| `DatabaseException`              | General DB failure (connection, transaction) |
| `QueryException`                 | Invalid or failed query                      |

### `EZXception.Business`

| Exception                         | When to use                                     |
| --------------------------------- | ----------------------------------------------- |
| `BusinessRuleException`           | A domain/business rule was violated             |
| `InvalidStateTransitionException` | Entity can't move from state A to state B       |
| `WorkflowException`               | Workflow step failed or preconditions unmet     |
| `OperationNotAllowedException`    | Action not permitted in current context         |
| `QuotaExceededException`          | Usage limit reached (storage, API calls, items) |

### `EZXception.Configuration`

| Exception                       | When to use                          |
| ------------------------------- | ------------------------------------ |
| `ConfigurationException`        | Base for all config errors           |
| `MissingConfigurationException` | Required config key is absent        |
| `InvalidConfigurationException` | Config key present but has bad value |

### `EZXception.ExternalService`

| Exception                     | When to use                               |
| ----------------------------- | ----------------------------------------- |
| `ExternalServiceException`    | Base for all external service errors      |
| `ApiException`                | HTTP API returned an error response       |
| `ServiceUnavailableException` | Service is down or unreachable (HTTP 503) |
| `RateLimitException`          | Rate limit hit (HTTP 429)                 |
| `OperationTimeoutException`   | Call exceeded allowed time                |

### `EZXception.IO`

| Exception                      | When to use                                |
| ------------------------------ | ------------------------------------------ |
| `EZFileNotFoundException`      | Expected file doesn't exist                |
| `FileAlreadyExistsException`   | File exists but shouldn't                  |
| `InvalidFileFormatException`   | File content doesn't match expected format |
| `FileSizeLimitException`       | File exceeds max size                      |
| `EZDirectoryNotFoundException` | Expected directory doesn't exist           |
| `FileAccessException`          | File locked or permission denied           |

### `EZXception.Network`

| Exception                 | When to use                              |
| ------------------------- | ---------------------------------------- |
| `NetworkException`        | Base for all network errors              |
| `ConnectionException`     | Could not connect to host                |
| `DnsResolutionException`  | Hostname could not be resolved           |
| `SslCertificateException` | SSL/TLS certificate invalid or untrusted |
| `ProxyException`          | Proxy error or misconfiguration          |

### `EZXception.Serialization`

| Exception                  | When to use                                 |
| -------------------------- | ------------------------------------------- |
| `EZSerializationException` | Failed to serialize object to JSON/XML/etc. |
| `DeserializationException` | Failed to deserialize data into an object   |

### `EZXception.Concurrency`

| Exception                   | When to use                               |
| --------------------------- | ----------------------------------------- |
| `DeadlockException`         | Deadlock detected                         |
| `ResourceLockException`     | Resource locked by another thread/process |
| `MaxRetryExceededException` | Retry limit reached                       |

### `EZXception.Domain`

| Exception                     | When to use                                  |
| ----------------------------- | -------------------------------------------- |
| `DomainException`             | Base for rich domain model exceptions        |
| `InvariantViolationException` | Aggregate/entity invariant broken            |
| `AggregateRootException`      | Aggregate root boundary/consistency violated |

## Quick Example

```csharp
using EZXception.Validation;
using EZXception.Data;
using EZXception.Authorization;

// Validation
if (string.IsNullOrEmpty(email))
    throw new RequiredFieldException("Email");

if (!IsValidEmail(email))
    throw new InvalidFormatException("Email", "user@example.com", email);

// Data
var user = await _repo.FindAsync(id)
    ?? throw new EntityNotFoundException("User", id);

// Auth
if (!user.HasRole("Admin"))
    throw new InsufficientRoleException("Admin", user.Role);
```

## Naming Notes

Three exceptions in this library carry an `EZ` prefix to avoid compile-time ambiguity with identically named BCL types:

| EZXception type                                     | Would shadow                                          |
| --------------------------------------------------- | ----------------------------------------------------- |
| `EZXception.IO.EZFileNotFoundException`             | `System.IO.FileNotFoundException`                     |
| `EZXception.IO.EZDirectoryNotFoundException`        | `System.IO.DirectoryNotFoundException`                |
| `EZXception.Serialization.EZSerializationException` | `System.Runtime.Serialization.SerializationException` |

Without the prefix, adding `using EZXception.IO;` to a file that also uses BCL IO types would produce a compiler ambiguity error requiring fully qualified names throughout.

## License

MIT
