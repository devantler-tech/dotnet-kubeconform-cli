using CliWrap;
using CliWrap.Buffered;

namespace DevantlerTech.KubeconformCLI;

/// <summary>
/// A class to run Kubeconform CLI commands.
/// </summary>
public static class Kubeconform
{
  /// <summary>
  /// The Kubeconform CLI command.
  /// </summary>
  static Command Command
  {
    get
    {
      string binaryName = "kubeconform";
      string? pathEnv = Environment.GetEnvironmentVariable("PATH");

      if (!string.IsNullOrEmpty(pathEnv))
      {
        string[] paths = pathEnv.Split(Path.PathSeparator);
        foreach (string dir in paths)
        {
          string fullPath = Path.Combine(dir, binaryName);
          if (File.Exists(fullPath))
          {
            return Cli.Wrap(fullPath);
          }
        }
      }

      throw new FileNotFoundException($"The '{binaryName}' CLI was not found in PATH.");
    }
  }

  /// <summary>
  /// Runs the kubeconform CLI command with the given arguments.
  /// </summary>
  /// <param name="arguments"></param>
  /// <param name="validation"></param>
  /// <param name="silent"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public static async Task<(int ExitCode, string Message)> RunAsync(
    string[] arguments,
    CommandResultValidation validation = CommandResultValidation.ZeroExitCode,
    bool silent = false,
    CancellationToken cancellationToken = default)
  {
    using var stdOutConsole = silent ? Stream.Null : Console.OpenStandardOutput();
    using var stdErrConsole = silent ? Stream.Null : Console.OpenStandardError();
    var command = Command.WithArguments(arguments)
      .WithValidation(validation)
      .WithStandardOutputPipe(PipeTarget.ToStream(stdOutConsole))
      .WithStandardErrorPipe(PipeTarget.ToStream(stdErrConsole));
    var result = await command.ExecuteBufferedAsync(cancellationToken).ConfigureAwait(false);
    return (result.ExitCode, result.StandardOutput + result.StandardError);
  }
}
