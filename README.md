<h1 align="center">
    <a href="">
        <img src="./Assets/EZX-ribbon-nobg-lit.png#gh-light-mode-only" height="100px">
        <img src="./Assets/EZX-ribbon-nobg-drk.png#gh-dark-mode-only" height="100px">
    </a>
</h1>

<p align="center">
  <i align="center">Never write another custom exception class ever again </i><span>🛑❌</span>
</p>

# EZXception

Writing custom exception classes is repetitive boilerplate. EZXception is a `netstandard2.0` library that ships 50+ pre-built, richly structured exception types covering every scenario a .NET developer encounters — validation, authorization, data access, business rules, configuration, external services, file I/O, networking, serialization, concurrency, and domain modeling.

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

## Documentation

- [Setup & Installation](Docs/guide.md#installation)
- [Validation](Docs/guide.md#ezxceptionvalidation)
- [Authorization](Docs/guide.md#ezxceptionauthorization)
- [Data](Docs/guide.md#ezxceptiondata)
- [Business](Docs/guide.md#ezxceptionbusiness)
- [Domain](Docs/guide.md#ezxceptiondomain)
- [Configuration](Docs/guide.md#ezxceptionconfiguration)
- [External Service](Docs/guide.md#ezxceptionexternalservice)
- [IO](Docs/guide.md#ezxceptionio)
- [Network](Docs/guide.md#ezxceptionnetwork)
- [Serialization](Docs/guide.md#ezxceptionserialization)
- [Concurrency](Docs/guide.md#ezxceptionconcurrency)

## Naming Notes

Three exceptions carry an `EZ` prefix to avoid compile-time ambiguity with identically named BCL types:

| EZXception type                                     | Would shadow                                          |
| --------------------------------------------------- | ----------------------------------------------------- |
| `EZXception.IO.EZFileNotFoundException`             | `System.IO.FileNotFoundException`                     |
| `EZXception.IO.EZDirectoryNotFoundException`        | `System.IO.DirectoryNotFoundException`                |
| `EZXception.Serialization.EZSerializationException` | `System.Runtime.Serialization.SerializationException` |

## License

MIT
