namespace Core.Test.NetRelated;

/// <summary>
/// Tests that replace or use the process-wide <c>HttpChannelExt.SharedHttpClient</c> must not run in parallel.
/// Put them into this collection: <c>[Collection(SharedHttpClientCollection.Name)]</c>.
/// </summary>
public static class SharedHttpClientCollection
{
    public const string Name = "SharedHttpClient";
}
