namespace Devantler.KubeconformCLI.Tests.KubeconformTests;

/// <summary>
/// Tests for the RunAsync method.
/// </summary>
public class RunAsyncTests
{
  /// <summary>
  /// Tests the RunAsync method with valid input.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_WithValidInput_ShouldRunSuccessfully()
  {
    // Arrange
    string[] kubeconformFlags = ["-skip=Secret"];
    string[] kubeconformConfig = ["-strict", "-ignore-missing-schemas", "-schema-location", "default", "-schema-location", "https://raw.githubusercontent.com/datreeio/CRDs-catalog/main/{{.Group}}/{{.ResourceKind}}_{{.ResourceAPIVersion}}.json", "-verbose"];
    string assetsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "assets");
    string[] filesInAssetsDirectory = Directory.GetFiles(assetsDirectoryPath, "*.yaml", SearchOption.AllDirectories);

    // Act
    var exceptions = new List<Exception>();

    foreach (string file in filesInAssetsDirectory)
    {
      try
      {
        await Kubeconform.RunAsync(file, kubeconformFlags, kubeconformConfig, CancellationToken.None);
      }
      catch (KubeconformException ex)
      {
        exceptions.Add(ex);
      }
    }

    // Assert
    Assert.Empty(exceptions);
  }

  /// <summary>
  /// Tests the RunAsync method with defaults.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_WithDefaults_ShouldRunSuccessfully()
  {
    // Arrange
    string assetsDirectoryPath = Path.Combine(AppContext.BaseDirectory, "assets");
    string[] filesInAssetsDirectory = Directory.GetFiles(assetsDirectoryPath, "*.yaml", SearchOption.AllDirectories);

    // Act
    var exceptions = new List<Exception>();

    foreach (string file in filesInAssetsDirectory)
    {
      try
      {
        await Kubeconform.RunAsync(file, cancellationToken: CancellationToken.None);
      }
      catch (KubeconformException ex)
      {
        exceptions.Add(ex);
      }
    }

    // Assert
    Assert.Empty(exceptions);
  }
}
