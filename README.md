# 🔎 .NET Kubeconform CLI

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler/dotnet-kubeconform-cli/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler/dotnet-kubeconform-cli/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler/dotnet-kubeconform-cli/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler/dotnet-kubeconform-cli)

<details>
  <summary>Show/hide folder structure</summary>

<!-- readme-tree start -->

```
.
├── .github
│   ├── scripts
│   └── workflows
├── Devantler.KindCLI
│   └── runtimes
│       ├── linux-arm64
│       │   └── native
│       ├── linux-x64
│       │   └── native
│       ├── osx-arm64
│       │   └── native
│       ├── osx-x64
│       │   └── native
│       └── win-x64
│           └── native
└── Devantler.KindCLI.Tests
    ├── KindTests
    └── assets

18 directories
```

<!-- readme-tree end -->

</details>

A simple .NET library that embeds the Kubeconform CLI.

## 🚀 Getting Started

To get started, you can install the package from NuGet.

```bash
dotnet add package Devantler.KubeconformCLI
```

## 📝 Usage

You can execute the Kubeconform CLI commands using the `Kubeconform` class.

```csharp
using Devantler.KubeconformCLI;

string[] kubeconformFlags = ["-skip=Secret"];
string[] kubeconformConfig = ["-strict", "-ignore-missing-schemas", "-schema-location", "default", "-schema-location", "https://raw.githubusercontent.com/datreeio/CRDs-catalog/main/{{.Group}}/{{.ResourceKind}}_{{.ResourceAPIVersion}}.json", "-verbose"];
string [] files = Directory.GetFiles("path/to/manifests", "*.yaml", SearchOption.AllDirectories);

foreach (string file in files)
{
    _ = await Kubeconform.ValidateAsync(file, kubeconformFlags, kubeconformConfig, cancellationToken);
}
```
