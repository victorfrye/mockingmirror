namespace VictorFrye.MockingMirror.AppHost;

public class LlmResource(string name) : Resource(name), IResourceWithConnectionString, IResourceWithoutLifetime
{
    internal IResource? InnerResource { get; set; }
    internal ReferenceExpression? ConnectionString { get; set; }

    public ReferenceExpression ConnectionStringExpression => ConnectionString ?? throw new InvalidOperationException("Connection string is not set.");
}
