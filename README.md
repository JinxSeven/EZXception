<h1 align="center">
    <a href="">
        <!-- <img src="./Assets/EZX-ribbon-nobg-light.png#gh-light-mode-only" height="100px"> -->
        <img src="./Assets/EZX-ribbon-nobg-dark.png#gh-dark-mode-only" height="100px">
    </a>
</h1>

<p align="center">
  <i align="center">Never write another custom exception class ever again.</i><span>❌</span>
</p>

# EZXception

EZXception is a `netstandard2.0` library that ships 50+ pre-built, richly structured exception types covering every scenario a .NET developer encounters - validation, authorization, data access, business rules, configuration, external services, file I/O, networking, serialization, concurrency, and domain modeling.

Instead of writing this every time:

```csharp
public class UserNotFoundException : Exception
{
    public UserNotFoundException(string id)
        : base($"User '{id}' was not found.") { }
}
```

You just do this:

```csharp
throw new EntityNotFoundException("User", id);
```

Every exception carries typed contextual properties (entity name, field name, status code, timeout, etc.) so catch blocks can read structured data instead of parsing message strings.

## Get Started

```bash
dotnet add package EZXception --version 1.0.0
```

## Documentation

- [Setup & Installation](DOCUMENTATION.md#installation)
- [Validation](DOCUMENTATION.md#ezxceptionvalidation)
- [Authorization](DOCUMENTATION.md#ezxceptionauthorization)
- [Data](DOCUMENTATION.md#ezxceptiondata)
- [Business](DOCUMENTATION.md#ezxceptionbusiness)
- [Domain](DOCUMENTATION.md#ezxceptiondomain)
- [Configuration](DOCUMENTATION.md#ezxceptionconfiguration)
- [External Service](DOCUMENTATION.md#ezxceptionexternalservice)
- [IO](DOCUMENTATION.md#ezxceptionio)
- [Network](DOCUMENTATION.md#ezxceptionnetwork)
- [Serialization](DOCUMENTATION.md#ezxceptionserialization)
- [Concurrency](DOCUMENTATION.md#ezxceptionconcurrency)

## Naming Notes

Three exceptions carry an `EZ` prefix to avoid compile-time ambiguity with identically named BCL types:

| EZXception type                                     | Would shadow                                          |
| --------------------------------------------------- | ----------------------------------------------------- |
| `EZXception.IO.EZFileNotFoundException`             | `System.IO.FileNotFoundException`                     |
| `EZXception.IO.EZDirectoryNotFoundException`        | `System.IO.DirectoryNotFoundException`                |
| `EZXception.Serialization.EZSerializationException` | `System.Runtime.Serialization.SerializationException` |

## License

MIT
