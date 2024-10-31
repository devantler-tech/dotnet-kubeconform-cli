using System.Runtime.InteropServices;
using CliWrap;
using Devantler.CLIRunner;

namespace Devantler.KubeconformCLI;

/// <summary>
/// A class to run Kubeconform CLI commands.
/// </summary>
public static class Kubeconform
{
  static Command Command => GetCommand();
  internal static Command GetCommand(PlatformID? platformID = default, Architecture? architecture = default, string? runtimeIdentifier = default)
  {
    platformID ??= Environment.OSVersion.Platform;
    architecture ??= RuntimeInformation.ProcessArchitecture;
    runtimeIdentifier ??= RuntimeInformation.RuntimeIdentifier;

    string binary = (platformID, architecture, runtimeIdentifier) switch
    {
      (PlatformID.Unix, Architecture.X64, "osx-x64") => "kubeconform-osx-x64",
      (PlatformID.Unix, Architecture.Arm64, "osx-arm64") => "kubeconform-osx-arm64",
      (PlatformID.Unix, Architecture.X64, "linux-x64") => "kubeconform-linux-x64",
      (PlatformID.Unix, Architecture.Arm64, "linux-arm64") => "kubeconform-linux-arm64",
      (PlatformID.Win32NT, Architecture.X64, "win-x64") => "kubeconform-win-x64.exe",
      (PlatformID.Win32NT, Architecture.X64, "win-arm64") => "kubeconform-win-arm64.exe",
      _ => throw new PlatformNotSupportedException($"Unsupported platform: {Environment.OSVersion.Platform} {RuntimeInformation.ProcessArchitecture}"),
    };
    string binaryPath = Path.Combine(AppContext.BaseDirectory, binary);
    return !File.Exists(binaryPath) ?
      throw new FileNotFoundException($"{binaryPath} not found.") :
      Cli.Wrap(binaryPath);
  }

  /// <summary>
  /// Runs Kubeconform on a file.
  /// </summary>
  /// <param name="file"></param>
  /// <param name="kubeconformFlags"></param>
  /// <param name="kubeconformConfig"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubeconformException"></exception>
  public static async Task RunAsync(string file, string[]? kubeconformFlags = null, string[]? kubeconformConfig = null, CancellationToken cancellationToken = default)
  {
    kubeconformFlags ??= [];
    kubeconformConfig ??= ["-strict", "-ignore-missing-schemas", "-schema-location", "default", "-schema-location", "https://raw.githubusercontent.com/datreeio/CRDs-catalog/main/{{.Group}}/{{.ResourceKind}}_{{.ResourceAPIVersion}}.json", "-verbose"];
    var arguments = kubeconformFlags.Concat(kubeconformConfig).Concat([file]);
    var cmd = Command.WithArguments(arguments);
    var (exitCode, result) = await CLI.RunAsync(cmd, silent: true, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
      throw new KubeconformException($"Kubeconform failed with exit code {exitCode} and error: {result}");
  }
}
