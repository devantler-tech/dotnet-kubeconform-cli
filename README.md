# 🔎 .NET Kubeconform CLI

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler-tech/dotnet-kubeconform-cli/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler-tech/dotnet-kubeconform-cli/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler-tech/dotnet-kubeconform-cli/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler-tech/dotnet-kubeconform-cli)

A simple .NET library that embeds the Kubeconform CLI.

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 or later
- [Kubeconform CLI](https://github.com/yannh/kubeconform?tab=readme-ov-file#installation) installed and available in your system's PATH

### Installation

To get started, you can install the package from NuGet.

```bash
dotnet add package DevantlerTech.KubeconformCLI
```

## 📝 Usage

You can execute the Kubeconform CLI commands using the `Kubeconform` class.

```csharp
using DevantlerTech.KubeconformCLI;

var (exitCode, output) = await Kubeconform.RunAsync(["arg1", "arg2"]);
```
