# OpenRouter.ConsoleExample

This example demonstrates how to use the OpenRouter client in a C# console application.

## Environment Variables

For security, the API key is not stored in code or in the repository. Instead, set the following environment variable before running the example:

```
OPENROUTER_API_KEY=your-api-key-here
```

### Setting Environment Variables (Windows)

You can set the environment variable in PowerShell:

```
$env:OPENROUTER_API_KEY="your-api-key-here"
```

Or permanently (for your user):

```
[System.Environment]::SetEnvironmentVariable("OPENROUTER_API_KEY", "your-api-key-here", "User")
```

Alternatively, copy `.env.example` to `.env` and set your key there for reference (but this file is not loaded automatically by the app).

## Running the Example

Build and run the project as usual. If the environment variable is not set, the program will display an error and exit.

---

**Never commit your actual API key to the repository!**
