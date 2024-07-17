# Watson

<p align="center">
  <a href="https://dev.azure.com/benito356/NetDevOpsTest/_packaging?_a=package&feed=e3acf8ba-ec70-46f0-b1a5-da1ce3dd5d9f&package=b8696a32-e71a-4479-9b0e-002997b8d8ef&preferRelease=true">
    <img alt="Stable version" src="https://feeds.dev.azure.com/benito356/339c91a8-9d6c-4082-8b1a-93c2ae76b637/_apis/public/Packaging/Feeds/e3acf8ba-ec70-46f0-b1a5-da1ce3dd5d9f/Packages/b8696a32-e71a-4479-9b0e-002997b8d8ef/Badge" />
  </a>
  &nbsp;
  <a href="https://dev.azure.com/benito356/NetDevOpsTest/_artifacts/feed/Example-Preview">
    <img alt="GitHub commits since latest release (by SemVer)" src="https://img.shields.io/github/commits-since/pleonex/template-csharp/latest?sort=semver" />
  </a>
  &nbsp;
  <a href="https://github.com/pleonex/template-csharp/workflows/Build%20and%20release">
    <img alt="Build and release" src="https://github.com/pleonex/template-csharp/workflows/Build%20and%20release/badge.svg?branch=main&event=push" />
  </a>
  &nbsp;
  <a href="https://choosealicense.com/licenses/mit/">
    <img alt="MIT License" src="https://img.shields.io/badge/license-MIT-blue.svg?style=flat" />
  </a>
  &nbsp;
</p>

Watson.Lib is a C# library designed for modifying Unity-based games by providing tools for asset management, game logic customization, and utility functions. This library supports various games, offering specific implementations and tools required for each game.

## Supported Games

- **AI: The Somnium Files 2**
- **Neptunia: Sisters vs Sisters**
- **Windose**

## Build

The project requires .NET 8.0 SDK to build.

To build, test, and generate artifacts, run:

```sh
# Build and run tests (with code coverage!)
dotnet run --project build/orchestrator

# (Optional) Create bundles (nuget, zips, docs)
dotnet run --project build/orchestrator -- --target=Bundle
```

## Release

Create a new GitHub release with a tag `v{Version}` (e.g. `v2.4`) and that's it!
This triggers a pipeline that builds and deploy the project.
