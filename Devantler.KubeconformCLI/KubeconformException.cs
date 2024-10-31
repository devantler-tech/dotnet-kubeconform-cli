namespace Devantler.KubeconformCLI;

/// <summary>
/// Represents an exception that is thrown when an error occurs in Kubeconform.
/// </summary>
[Serializable]
public class KubeconformException : Exception
{
  /// <summary>
  /// Initializes a new instance of the <see cref="KubeconformException"/> class.
  /// </summary>
  public KubeconformException()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="KubeconformException"/> class with a specified error message.
  /// </summary>
  /// <param name="message"></param>
  public KubeconformException(string? message) : base(message)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="KubeconformException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
  /// </summary>
  /// <param name="message"></param>
  /// <param name="innerException"></param>
  public KubeconformException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}
