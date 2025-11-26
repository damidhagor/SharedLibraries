using Microsoft.CodeAnalysis;
using Shared.Blazor.SourceGeneration.LocalizationServiceGenerator.Attributes;
using Shared.Blazor.SourceGeneration.LocalizationServiceGenerator.Generators;
using Shared.Blazor.SourceGeneration.LocalizationServiceGenerator.Providers;

namespace Shared.Blazor.SourceGeneration.LocalizationServiceGenerator;

[Generator]
public sealed class LocalizationServiceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(
            static context => context
                .AddSource(LocalizationServiceAttribute.SourceHint, LocalizationServiceAttribute.Source));

        var resources = context.AdditionalTextsProvider.GetResources();
        var services = context.SyntaxProvider
            .GetServices()
            .AddResources(resources);

        context.RegisterSourceOutput(services, InterfaceGenerator.GenerateLocalizationServiceInterface);
        context.RegisterSourceOutput(services, ClassGenerator.GenerateLocalizationServiceClass);
    }
}
