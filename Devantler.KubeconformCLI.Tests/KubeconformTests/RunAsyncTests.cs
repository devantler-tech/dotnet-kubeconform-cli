namespace Devantler.KubeconformCLI.Tests.KubeconformTests;

/// <summary>
/// Tests for the RunAsync method.
/// </summary>
public class RunAsyncTests
{
  /// <summary>
  /// Tests that the binary can return the version of the kubeconform CLI command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_Version_ReturnsVersion()
  {
    // Act
    var (exitCode, message) = await Kubeconform.RunAsync(["-v"]);

    // Assert
    Assert.Equal(0, exitCode);
    Assert.Equal("development", message);
  }
}
