namespace VictorFrye.MockingMirror.AppHost;

public static class LlmExtensions
{
    public static IResourceBuilder<LlmResource> AddLlm(this IDistributedApplicationBuilder builder, string name) => builder.CreateResourceBuilder(new LlmResource(name));

    public static IResourceBuilder<LlmResource> RunAsOllama(this IResourceBuilder<LlmResource> builder, string model, Action<IResourceBuilder<OllamaResource>>? configure = null)
    {
        if (builder.ApplicationBuilder.ExecutionContext.IsRunMode)
        {
            builder.Reset();

            var ollama = builder.ApplicationBuilder.AddOllama("ollama").WithDataVolume();

            configure?.Invoke(ollama);

            var ollamaModel = ollama.AddModel(builder.Resource.Name, model);

            builder.Resource.InnerResource = ollamaModel.Resource;
            builder.Resource.ConnectionString = ReferenceExpression.Create($"{ollamaModel};Provider={nameof(LlmProvider.Ollama)}");
        }

        return builder;
    }

    public static IResourceBuilder<LlmResource> RunAsOpenAI(this IResourceBuilder<LlmResource> builder, string model, string version, Action<IResourceBuilder<AzureOpenAIResource>>? configure = null)
    {
        if (builder.ApplicationBuilder.ExecutionContext.IsRunMode)
        {
            builder.AsOpenAI(model, version, configure);
        }

        return builder;
    }

    public static IResourceBuilder<LlmResource> PublishAsOpenAI(this IResourceBuilder<LlmResource> builder, string model, string version, Action<IResourceBuilder<AzureOpenAIResource>>? configure = null)
    {
        if (builder.ApplicationBuilder.ExecutionContext.IsPublishMode)
        {
            builder.AsOpenAI(model, version, configure);
        }

        return builder;
    }

    public static IResourceBuilder<LlmResource> AsOpenAI(this IResourceBuilder<LlmResource> builder, string model, string version, Action<IResourceBuilder<AzureOpenAIResource>>? configure = null)
    {
        builder.Reset();

        var openAI = builder.ApplicationBuilder.AddAzureOpenAI("openai");

        configure?.Invoke(openAI);

        var openAIModel = openAI.AddDeployment(
            name: builder.Resource.Name,
            modelName: model,
            modelVersion: version
        );

        builder.Resource.InnerResource = openAIModel.Resource;
        builder.Resource.ConnectionString = ReferenceExpression.Create($"{openAIModel};Provider={nameof(LlmProvider.OpenAI)}");

        return builder;
    }

    private static void Reset(this IResourceBuilder<LlmResource> builder)
    {
        IResource? resource = builder.Resource.InnerResource;

        if (resource is not null)
        {
            builder.ApplicationBuilder.Resources.Remove(resource);

            while (resource is IResourceWithParent child)
            {
                builder.ApplicationBuilder.Resources.Remove(child.Parent);
                resource = child.Parent;
            }
        }

        builder.Resource.ConnectionString = null;
    }
}
