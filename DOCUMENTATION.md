# EZXception Guide

## Installation

```bash
dotnet add package EZXception
```

Or search `EZXception` in the Visual Studio NuGet Package Manager.

## Compatibility

Targets `netstandard2.0`:

| Runtime | Minimum version |
|---|---|
| .NET | 8, 9, 10+ |
| .NET Framework | 4.6.1+ |
| Mono | 5.4+ |
| Xamarin.iOS | 10.14+ |
| Xamarin.Android | 8.0+ |
| Unity | 2018.1+ |

---

## Namespaces

| Namespace | What it covers |
|---|---|
| `EZXception.Validation` | Field validation, format, range, length, duplicates |
| `EZXception.Authorization` | Auth, permissions, tokens, roles, account state |
| `EZXception.Data` | Not found, duplicates, DB errors, concurrency conflicts |
| `EZXception.Business` | Business rules, state machines, workflows, quotas |
| `EZXception.Domain` | DDD aggregate roots, invariants, domain violations |
| `EZXception.Configuration` | Missing or invalid config keys |
| `EZXception.ExternalService` | API errors, timeouts, rate limits, unavailability |
| `EZXception.IO` | File and directory access, format, size, existence |
| `EZXception.Network` | Connections, DNS, SSL, proxy |
| `EZXception.Serialization` | Serialize/deserialize failures |
| `EZXception.Concurrency` | Deadlocks, resource locks, retry exhaustion |

---

## EZXception.Validation

```csharp
using EZXception.Validation;
```

### ValidationException

Base exception. Supports collecting multiple errors in a single throw.

```csharp
// Single message
throw new ValidationException("The request body is invalid.");

// Multiple errors at once
var errors = new[] { "Email is required.", "Username too short.", "Date out of range." };
throw new ValidationException("One or more validation errors occurred.", errors);

// Wrapping an inner exception
throw new ValidationException("Validation pipeline failed.", innerException);
```

```csharp
catch (ValidationException ex)
{
    foreach (var error in ex.Errors)
        Console.WriteLine(error);
}
```

| Property | Type |
|---|---|
| `Errors` | `IReadOnlyList<string>` |

---

### RequiredFieldException

```csharp
// Auto message: "The field 'Email' is required and cannot be null or empty."
throw new RequiredFieldException("Email");

// Custom message
throw new RequiredFieldException("Email", "An email address must be provided.");

// With inner exception
throw new RequiredFieldException("Email", "Email extraction failed.", innerException);
```

| Property | Type |
|---|---|
| `FieldName` | `string` |

---

### InvalidFormatException

```csharp
// Field name only
throw new InvalidFormatException("PhoneNumber");

// With expected format
throw new InvalidFormatException("PhoneNumber", "+1-555-000-0000");

// With expected format and the bad value received
throw new InvalidFormatException("PhoneNumber", "+1-555-000-0000", input);

// Wrapping a parse exception
throw new InvalidFormatException("DateOfBirth", "Failed to parse date.", innerException);
```

| Property | Type |
|---|---|
| `FieldName` | `string` |
| `ExpectedFormat` | `string?` |
| `ActualValue` | `object?` |

---

### ValueOutOfRangeException

```csharp
// Min and max
throw new ValueOutOfRangeException("Age", minValue: 0, maxValue: 120);

// With the actual bad value
throw new ValueOutOfRangeException("Age", minValue: 0, maxValue: 120, actualValue: -5);

// One-sided range — pass null for the unbounded side
throw new ValueOutOfRangeException("Price", minValue: 0m, maxValue: null, actualValue: -9.99m);

// Custom message
throw new ValueOutOfRangeException("StartDate", "Start date must not be in the past.");

// Custom message with inner exception
throw new ValueOutOfRangeException("StartDate", "Range check failed.", innerException);
```

| Property | Type |
|---|---|
| `FieldName` | `string` |
| `MinValue` | `object?` |
| `MaxValue` | `object?` |
| `ActualValue` | `object?` |

---

### DuplicateValueException

```csharp
// Field name only
throw new DuplicateValueException("Username");

// With the conflicting value
throw new DuplicateValueException("Username", "john_doe");

// With inner exception
throw new DuplicateValueException("Username", "Uniqueness check failed.", innerException);
```

| Property | Type |
|---|---|
| `FieldName` | `string` |
| `Value` | `object?` |

---

### StringLengthException

```csharp
// Max only — "Bio' must not exceed 500 characters. Actual length: 612."
throw new StringLengthException("Bio", actualLength: 612, maxLength: 500);

// Min only
throw new StringLengthException("Password", actualLength: 4, minLength: 8);

// Both min and max
throw new StringLengthException("Username", actualLength: 1, minLength: 3, maxLength: 20);

// With inner exception
throw new StringLengthException("Username", "Length validation failed.", innerException);
```

| Property | Type |
|---|---|
| `FieldName` | `string` |
| `ActualLength` | `int` |
| `MinLength` | `int?` |
| `MaxLength` | `int?` |

---

## EZXception.Authorization

```csharp
using EZXception.Authorization;
```

### UnauthorizedException

Base exception for unauthenticated requests (HTTP 401). Use when the user has no valid session or token.

```csharp
// Default: "Authentication is required to access this resource."
throw new UnauthorizedException();

throw new UnauthorizedException("Please log in to continue.");

throw new UnauthorizedException("Token validation failed.", innerException);
```

---

### ForbiddenException

Authenticated but not permitted (HTTP 403).

```csharp
// Default: "You do not have permission to perform this action."
throw new ForbiddenException();

throw new ForbiddenException("You cannot delete other users' posts.");

// With the required permission name
throw new ForbiddenException("Access denied.", "posts:delete");

throw new ForbiddenException("Permission check failed.", innerException);
```

| Property | Type |
|---|---|
| `RequiredPermission` | `string?` |

---

### InvalidCredentialsException

```csharp
// Default: "The provided credentials are invalid."
throw new InvalidCredentialsException();

// Keep messages vague — don't reveal which field is wrong
throw new InvalidCredentialsException("Invalid email or password.");

throw new InvalidCredentialsException("Credential lookup failed.", innerException);
```

---

### TokenExpiredException

```csharp
// Default: "The authentication token has expired. Please re-authenticate."
throw new TokenExpiredException();

// With expiry timestamp
throw new TokenExpiredException(token.ExpiresAt);

throw new TokenExpiredException("Token decode failed.", innerException);
```

| Property | Type |
|---|---|
| `ExpiresAt` | `DateTimeOffset?` |

---

### AccountLockedException

```csharp
// "This account is locked. Please contact support."
throw new AccountLockedException();

// With user ID
throw new AccountLockedException(userId: "user_42");

// With user ID and unlock time
throw new AccountLockedException(userId: "user_42", lockedUntil: DateTimeOffset.UtcNow.AddMinutes(15));

throw new AccountLockedException("Lock check failed.", innerException);
```

| Property | Type |
|---|---|
| `UserId` | `string?` |
| `LockedUntil` | `DateTimeOffset?` |

---

### InsufficientRoleException

```csharp
// "Role 'Admin' is required to perform this action."
throw new InsufficientRoleException("Admin");

// "Role 'Admin' is required, but user has role 'Viewer'."
throw new InsufficientRoleException("Admin", actualRole: user.Role);

throw new InsufficientRoleException("Role lookup failed.", innerException);
```

| Property | Type |
|---|---|
| `RequiredRole` | `string?` |
| `ActualRole` | `string?` |

---

## EZXception.Data

```csharp
using EZXception.Data;
```

### EntityNotFoundException

```csharp
// "User was not found."
throw new EntityNotFoundException("User");

// "User with id '42' was not found."
throw new EntityNotFoundException("User", id);

throw new EntityNotFoundException("DB read failed.", innerException);
```

| Property | Type |
|---|---|
| `EntityType` | `string?` |
| `EntityId` | `object?` |

---

### DuplicateEntityException

```csharp
// "A duplicate User already exists."
throw new DuplicateEntityException("User");

// "A User with value 'john@example.com' already exists."
throw new DuplicateEntityException("User", conflictingValue: email);

throw new DuplicateEntityException("Duplicate check failed.", innerException);
```

| Property | Type |
|---|---|
| `EntityType` | `string?` |
| `ConflictingValue` | `object?` |

---

### DataIntegrityException

Thrown on constraint violations (foreign key, unique index, check constraint).

```csharp
throw new DataIntegrityException("Cannot delete category — products are still assigned to it.");

// With constraint name for logging
throw new DataIntegrityException("FK constraint violated.", constraintName: "FK_Products_Category");

throw new DataIntegrityException("Constraint check failed.", innerException, "FK_Products_Category");
```

| Property | Type |
|---|---|
| `ConstraintName` | `string?` |

---

### OptimisticConcurrencyException

Thrown when a record was modified by another process between your read and write.

```csharp
// Generic message
throw new OptimisticConcurrencyException();

// With entity type
throw new OptimisticConcurrencyException(entityType: "Order");

// With entity type and ID
throw new OptimisticConcurrencyException(entityType: "Order", entityId: orderId);

throw new OptimisticConcurrencyException("Concurrency check failed.", innerException);
```

| Property | Type |
|---|---|
| `EntityType` | `string?` |
| `EntityId` | `object?` |

---

### DatabaseException

General DB failure — connection dropped, transaction failed, etc.

```csharp
throw new DatabaseException("Failed to connect to the database.");

// With operation name for logging
throw new DatabaseException("Transaction rollback failed.", operation: "CommitOrder");

throw new DatabaseException("DB operation failed.", innerException, operation: "BulkInsert");
```

| Property | Type |
|---|---|
| `Operation` | `string?` |

---

### QueryException

Inherits `DatabaseException`. For invalid or failed queries.

```csharp
throw new QueryException("Invalid filter expression.");

// With the query string for debugging
throw new QueryException("Query execution failed.", query: "SELECT * FROM Orders WHERE ...");

throw new QueryException("Query failed.", innerException, query: rawQuery);
```

| Property | Type |
|---|---|
| `Query` | `string?` |

---

## EZXception.Business

```csharp
using EZXception.Business;
```

### BusinessRuleException

Base exception for domain/business rule violations.

```csharp
throw new BusinessRuleException("Orders cannot be placed outside of business hours.");

// With rule name for structured logging
throw new BusinessRuleException("Order placed outside business hours.", ruleName: "BusinessHoursOnly");

throw new BusinessRuleException("Rule evaluation failed.", innerException, ruleName: "BusinessHoursOnly");
```

| Property | Type |
|---|---|
| `RuleName` | `string?` |

---

### InvalidStateTransitionException

```csharp
// "Order cannot transition from 'Shipped' to 'Pending'."
throw new InvalidStateTransitionException("Order", fromState: "Shipped", toState: "Pending");

// No entity type
throw new InvalidStateTransitionException(null, fromState: "Active", toState: "Draft");

throw new InvalidStateTransitionException("Transition check failed.", innerException);
```

| Property | Type |
|---|---|
| `EntityType` | `string?` |
| `FromState` | `string?` |
| `ToState` | `string?` |

---

### WorkflowException

```csharp
throw new WorkflowException("Payment step failed: card declined.");

// With workflow and step context
throw new WorkflowException("Card declined.", workflowName: "Checkout", stepName: "Payment");

throw new WorkflowException("Step failed.", innerException, workflowName: "Checkout", stepName: "Payment");
```

| Property | Type |
|---|---|
| `WorkflowName` | `string?` |
| `StepName` | `string?` |

---

### OperationNotAllowedException

```csharp
// "Operation 'DeleteAccount' is not allowed: account has active subscriptions."
throw new OperationNotAllowedException("DeleteAccount", reason: "account has active subscriptions.");

throw new OperationNotAllowedException("Operation check failed.", innerException);
```

| Property | Type |
|---|---|
| `OperationName` | `string?` |

---

### QuotaExceededException

```csharp
// "Quota 'MonthlyAPIRequests' has been exceeded."
throw new QuotaExceededException("MonthlyAPIRequests");

// With limit
throw new QuotaExceededException("MonthlyAPIRequests", limit: 1000);

// With limit and current usage — "Quota 'MonthlyAPIRequests' exceeded: 1001/1000."
throw new QuotaExceededException("MonthlyAPIRequests", limit: 1000, current: 1001);

throw new QuotaExceededException("Quota check failed.", innerException);
```

| Property | Type |
|---|---|
| `QuotaName` | `string?` |
| `Limit` | `long?` |
| `Current` | `long?` |

---

## EZXception.Domain

```csharp
using EZXception.Domain;
```

### DomainException

Base exception for rich domain model errors.

```csharp
throw new DomainException("Customer credit limit exceeded.");

// With domain context name
throw new DomainException("Credit limit exceeded.", domainName: "Billing");

throw new DomainException("Domain rule failed.", innerException, domainName: "Billing");
```

| Property | Type |
|---|---|
| `DomainName` | `string?` |

---

### InvariantViolationException

Thrown when an aggregate's always-true invariant is broken.

```csharp
// "OrderTotal must always be >= 0" invariant violated
throw new InvariantViolationException("OrderTotalNonNegative", "Order total cannot be negative.");

throw new InvariantViolationException("OrderTotalNonNegative", "Invariant check failed.", innerException);
```

| Property | Type |
|---|---|
| `InvariantName` | `string?` |
| `DomainName` | `string?` (inherited) |

---

### AggregateRootException

Thrown when an operation crosses or violates an aggregate root boundary.

```csharp
throw new AggregateRootException("Order", aggregateId: orderId, "Cannot modify a completed order.");

throw new AggregateRootException("Order", "Boundary check failed.", innerException);
```

| Property | Type |
|---|---|
| `AggregateType` | `string?` |
| `AggregateId` | `object?` |

---

## EZXception.Configuration

```csharp
using EZXception.Configuration;
```

### ConfigurationException

Base exception for all configuration errors.

```csharp
throw new ConfigurationException("Configuration is in an invalid state.");

throw new ConfigurationException("Config load failed.", configKey: "Smtp:Host");

throw new ConfigurationException("Config load failed.", innerException, configKey: "Smtp:Host");
```

| Property | Type |
|---|---|
| `ConfigKey` | `string?` |

---

### MissingConfigurationException

```csharp
// "Required configuration key 'Smtp:Host' is missing."
throw new MissingConfigurationException("Smtp:Host");

// Custom message
throw new MissingConfigurationException("Smtp:Host", "SMTP host must be configured before sending emails.");

throw new MissingConfigurationException("Smtp:Host", "Config read failed.", innerException);
```

| Property | Type |
|---|---|
| `ConfigKey` | `string?` (inherited) |

---

### InvalidConfigurationException

```csharp
// "Configuration key 'Smtp:Port' has an invalid value."
throw new InvalidConfigurationException("Smtp:Port");

// With the bad value
throw new InvalidConfigurationException("Smtp:Port", actualValue: "abc");

// With the bad value and what was expected
throw new InvalidConfigurationException("Smtp:Port", actualValue: "abc", expectedDescription: "an integer between 1 and 65535");

throw new InvalidConfigurationException("Smtp:Port", "Port parse failed.", innerException);
```

| Property | Type |
|---|---|
| `ActualValue` | `object?` |
| `ExpectedDescription` | `string?` |

---

## EZXception.ExternalService

```csharp
using EZXception.ExternalService;
```

### ExternalServiceException

Base exception for all external service failures.

```csharp
throw new ExternalServiceException("PaymentGateway", "Unexpected response format.");

throw new ExternalServiceException("PaymentGateway", "Call failed.", innerException);
```

| Property | Type |
|---|---|
| `ServiceName` | `string?` |

---

### ApiException

Thrown when an external HTTP API returns an error status code.

```csharp
// "API call to 'Stripe' failed with status 400."
throw new ApiException("Stripe", statusCode: 400);

// With response body for debugging
throw new ApiException("Stripe", statusCode: 400, responseBody: responseContent);

// With response body and the request URL
throw new ApiException("Stripe", statusCode: 400, responseBody: responseContent, requestUrl: requestUri);

throw new ApiException("Stripe", "Deserialization of error response failed.", innerException);
```

| Property | Type |
|---|---|
| `ServiceName` | `string?` (inherited) |
| `StatusCode` | `int?` |
| `ResponseBody` | `string?` |
| `RequestUrl` | `string?` |

---

### ServiceUnavailableException

```csharp
// "Service 'InventoryService' is currently unavailable. Please try again later."
throw new ServiceUnavailableException("InventoryService");

throw new ServiceUnavailableException("InventoryService", "Service is under maintenance until 3 AM.");

throw new ServiceUnavailableException("InventoryService", "Health check failed.", innerException);
```

---

### RateLimitException

```csharp
// "Rate limit exceeded for service 'OpenAI'."
throw new RateLimitException("OpenAI");

// With retry-after duration — "... Retry after 60 seconds."
throw new RateLimitException("OpenAI", retryAfter: TimeSpan.FromSeconds(60));

throw new RateLimitException("OpenAI", "Rate limit parse failed.", innerException);
```

| Property | Type |
|---|---|
| `RetryAfter` | `TimeSpan?` |

---

### OperationTimeoutException

```csharp
// "Operation 'FetchUserProfile' on service 'UserService' timed out."
throw new OperationTimeoutException("UserService", "FetchUserProfile");

// With the actual timeout value
throw new OperationTimeoutException("UserService", "FetchUserProfile", timeout: TimeSpan.FromSeconds(30));

throw new OperationTimeoutException("UserService", "Timeout handler threw.", innerException);
```

| Property | Type |
|---|---|
| `OperationName` | `string?` |
| `Timeout` | `TimeSpan?` |

---

## EZXception.IO

```csharp
using EZXception.IO;
```

> **Note:** `EZFileNotFoundException`, `EZDirectoryNotFoundException`, and `EZSerializationException` carry the `EZ` prefix to avoid compiler ambiguity with BCL types of the same name. See [Naming Notes](../README.md#naming-notes).

### EZFileNotFoundException

```csharp
// "File not found: '/uploads/avatar.png'."
throw new EZFileNotFoundException("/uploads/avatar.png");

throw new EZFileNotFoundException("/uploads/avatar.png", "Profile picture is missing.");

throw new EZFileNotFoundException("/uploads/avatar.png", "File read failed.", innerException);
```

| Property | Type |
|---|---|
| `FilePath` | `string?` |

---

### FileAlreadyExistsException

```csharp
// "File already exists: '/exports/report.csv'."
throw new FileAlreadyExistsException("/exports/report.csv");

throw new FileAlreadyExistsException("/exports/report.csv", "A report for this period already exists.");

throw new FileAlreadyExistsException("/exports/report.csv", "File creation failed.", innerException);
```

| Property | Type |
|---|---|
| `FilePath` | `string?` |

---

### InvalidFileFormatException

```csharp
// "File '/imports/data.csv' has an invalid or unrecognized format."
throw new InvalidFileFormatException("/imports/data.csv");

// With expected format
throw new InvalidFileFormatException("/imports/data.csv", expectedFormat: "CSV");

throw new InvalidFileFormatException("/imports/data.csv", "Header row missing.", innerException);
```

| Property | Type |
|---|---|
| `FilePath` | `string?` |
| `ExpectedFormat` | `string?` |

---

### FileSizeLimitException

```csharp
// Auto-formats bytes — "File '/uploads/video.mp4' exceeds the maximum allowed size of 10.00 MB. Actual size: 54.20 MB."
throw new FileSizeLimitException("/uploads/video.mp4", maxSizeBytes: 10_485_760, actualSizeBytes: 56_817_254);

throw new FileSizeLimitException("/uploads/video.mp4", "File too large.", innerException);
```

| Property | Type |
|---|---|
| `FilePath` | `string?` |
| `MaxSizeBytes` | `long?` |
| `ActualSizeBytes` | `long?` |

---

### EZDirectoryNotFoundException

```csharp
// "Directory not found: '/var/app/uploads'."
throw new EZDirectoryNotFoundException("/var/app/uploads");

throw new EZDirectoryNotFoundException("/var/app/uploads", "Upload directory has not been provisioned.");

throw new EZDirectoryNotFoundException("/var/app/uploads", "Directory check failed.", innerException);
```

| Property | Type |
|---|---|
| `DirectoryPath` | `string?` |

---

### FileAccessException

```csharp
// "Access to file '/data/config.json' was denied or the file is locked."
throw new FileAccessException("/data/config.json");

// With access type — "Cannot write file '/data/config.json'. Access denied or file is locked."
throw new FileAccessException("/data/config.json", accessType: "write");

throw new FileAccessException("/data/config.json", "Permission check failed.", innerException);
```

| Property | Type |
|---|---|
| `FilePath` | `string?` |
| `AccessType` | `string?` |

---

## EZXception.Network

```csharp
using EZXception.Network;
```

### NetworkException

Base exception for all network failures.

```csharp
throw new NetworkException("Network is unavailable.");

throw new NetworkException("Network call failed.", innerException);
```

---

### ConnectionException

```csharp
// Generic: "A network connection could not be established."
throw new ConnectionException();

// With host
throw new ConnectionException(host: "db.internal");

// With host and port — "Could not connect to 'db.internal:5432'."
throw new ConnectionException(host: "db.internal", port: 5432);

throw new ConnectionException("Socket error.", innerException);
```

| Property | Type |
|---|---|
| `Host` | `string?` |
| `Port` | `int?` |

---

### DnsResolutionException

```csharp
// "DNS resolution failed for host 'api.unknown-service.io'."
throw new DnsResolutionException("api.unknown-service.io");

throw new DnsResolutionException("api.unknown-service.io", "DNS lookup timed out.", innerException);
```

| Property | Type |
|---|---|
| `Hostname` | `string?` |

---

### SslCertificateException

```csharp
// "SSL certificate error for 'api.example.com': certificate has expired."
throw new SslCertificateException("api.example.com", reason: "certificate has expired");

throw new SslCertificateException("SSL handshake failed.", innerException);
```

| Property | Type |
|---|---|
| `Host` | `string?` |

---

### ProxyException

```csharp
throw new ProxyException("Proxy returned 407 Proxy Authentication Required.");

// With proxy address for logging
throw new ProxyException("Proxy auth failed.", proxyAddress: "http://proxy.corp:8080");

throw new ProxyException("Proxy error.", innerException, proxyAddress: "http://proxy.corp:8080");
```

| Property | Type |
|---|---|
| `ProxyAddress` | `string?` |

---

## EZXception.Serialization

```csharp
using EZXception.Serialization;
```

### EZSerializationException

```csharp
throw new EZSerializationException("Failed to serialize order to JSON.");

// With target format
throw new EZSerializationException("Serialization failed.", targetFormat: "JSON");

// With target format and the type being serialized
throw new EZSerializationException("Serialization failed.", targetFormat: "JSON", targetType: typeof(Order));

throw new EZSerializationException("Serializer threw.", innerException, targetFormat: "JSON", targetType: typeof(Order));
```

| Property | Type |
|---|---|
| `TargetFormat` | `string?` |
| `TargetType` | `Type?` |

---

### DeserializationException

```csharp
throw new DeserializationException("Failed to deserialize webhook payload.");

// With source format
throw new DeserializationException("Deserialization failed.", sourceFormat: "JSON");

// With source format and target type
throw new DeserializationException("Deserialization failed.", sourceFormat: "JSON", targetType: typeof(WebhookEvent));

// With raw input captured for debugging
throw new DeserializationException("Deserialization failed.", sourceFormat: "JSON", targetType: typeof(WebhookEvent), rawInput: body);

throw new DeserializationException("Parser threw.", innerException, sourceFormat: "JSON", targetType: typeof(WebhookEvent));
```

| Property | Type |
|---|---|
| `SourceFormat` | `string?` |
| `TargetType` | `Type?` |
| `RawInput` | `string?` |

---

## EZXception.Concurrency

```csharp
using EZXception.Concurrency;
```

### DeadlockException

```csharp
// Default: "A deadlock was detected. The operation cannot proceed."
throw new DeadlockException();

throw new DeadlockException("Deadlock detected between Order and Inventory locks.");

throw new DeadlockException("Lock acquisition threw.", innerException);
```

---

### ResourceLockException

```csharp
// Generic: "A required resource is currently locked and cannot be acquired."
throw new ResourceLockException();

// "Resource 'ReportGenerator' is currently locked and cannot be acquired."
throw new ResourceLockException(resourceName: "ReportGenerator");

throw new ResourceLockException("ReportGenerator", "Lock acquisition failed.", innerException);
```

| Property | Type |
|---|---|
| `ResourceName` | `string?` |

---

### MaxRetryExceededException

```csharp
// "Operation failed after 5 retry attempts."
throw new MaxRetryExceededException(attempts: 5);

// "Operation 'SendEmail' failed after 3 attempts."
throw new MaxRetryExceededException(attempts: 3, operationName: "SendEmail");

// With the last inner exception
throw new MaxRetryExceededException(attempts: 3, operationName: "SendEmail", innerException: lastException);
```

| Property | Type |
|---|---|
| `Attempts` | `int` |
| `OperationName` | `string?` |
