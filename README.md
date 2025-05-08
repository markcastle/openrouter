# OpenRouter API Client (.NET Standard 2.1 / Unity Compatible)

[![Build Status](https://github.com/markcastle/openrouter/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/markcastle/openrouter/actions)
[![Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen.svg)](https://github.com/markcastle/openrouter/actions) <!-- Update the badge URL percentage when coverage changes -->
[![NuGet](https://img.shields.io/nuget/v/OpenRouter.Client.Core.svg)](https://www.nuget.org/packages/OpenRouter.Client.Core)

---

## Project Vision

A robust, modular, and developer-friendly OpenRouter API client targeting .NET Standard 2.1 with full Unity compatibility, 100% xUnit test coverage, and optional support for advanced DI and HTTP resilience.

- **Multi-project architecture** for maximum flexibility and Unity support
- **Provider-agnostic JSON serialization** (System.Text.Json & Newtonsoft.Json)
- **Optional Microsoft DI and ResilientHttpClient**
- **SOLID, KISS, YAGNI** principles
- **Comprehensive documentation and samples**

---

## Solution Structure

- `OpenRouter.Abstractions` - Interfaces, base models, contracts
- `OpenRouter.Client.Core` - Core implementation
- `OpenRouter.Client.SystemTextJson` - System.Text.Json support
- `OpenRouter.Client.NewtonsoftJson` - Newtonsoft.Json support (Unity)
- `OpenRouter.Client.DependencyInjection` - Microsoft DI integration
- `OpenRouter.Client.Resilience` - ResilientHttpClient integration
- Each has a corresponding `.Tests` project (xUnit)

---

## Getting Started

1. Add the desired package(s) from NuGet or UPM.
2. Configure your client using the builder or DI (optional).
3. Use the `IOpenRouterClient` interface for all operations.

---

## Badges

- 100% xUnit test coverage
- .NET Standard 2.1
- Unity compatible
- Modular, extensible, and well-documented

---

## License

MIT © CaptiveReality 2025
