# BlazorDialogs

This repository contains several useful helper libraries for Blazor applications.

## BlazorDialogs

This library contains a modal dialog service based on Bootstrap 5 for Blazor applications.

It uses the `BlazorDialogs.Components.ModalDisplay` component to display modal dialogs on a page.

Build in dialogs:
- `ConfirmationModal`: Confirm or cancel an action.
- `TextInputModal`: Displays an input field for user input.
- `ErrorModal`: Displays an error message and offers a button to copy more detailed information.

### How to use
- Add the services to the DI container:

```csharp
using BlazorDialogs.Extensions;

...

builder.Services.AddBlazorDialogServices();
```

- Add the `ModalDisplay` component to your application.
  This component should be placed at the root of the application to ensure that it is always available
  and should also only be present once in the application.

```html
@using BlazorDialogs.Components

<ModalDisplay />
```

- Inject the `IModalService` into your components and use it to show dialogs.

```csharp
using BlazorDialogs.Extensions;
using BlazorDialogs.Models.Results;
using BlazorDialogs.Services;

class MyClass(IModalService modalService)
{
    private readonly IModalService __modalService = modalService;

    private async Task ShowDialog()
    {
        ModalResult<TextInputResult> result = await modalService.ShowTextInput("Please enter your name");
        if (result.IsT0 && result.AsT0.IsT0)
        {
            string name = result.AsT0.AsT0;
            ...
        }
    }
}
```

## BlazorSourceGeneration

This library contains source generators.
To use this project, add the following to your project file:

```xml
<ItemGroup>
  <ProjectReference Include="path\to\BlazorSourceGeneration.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
</ItemGroup>
```

### LocalizationServiceGenerator

To help using the `Microsoft.Extensions.Localization.IStringLocalizer` class,
this source generator will generate a `LocalizationService` class
that will contain properties to provide access to all values from a `.resx` file
without the need to use the key strings directly.

#### How to use

**1) Resource Files:** The embedded localization resource files must be passed to the compilation as additional files
so that they can be parsed by the source generator.
Add the following lines to the `.csproj` file:

```xml
<PropertyGroup>
  <AdditionalFileItemNames>$(AdditionalFileItemNames);EmbeddedResource</AdditionalFileItemNames>
</PropertyGroup>
```

**2) LocalizationServiceAttribute:** Add the `BlazorSourceGeneration.LocalizationServiceGenerator.LocalizationServiceAttribute`
to the class that needs to be declared for the `IStringLocalizer` to find the corresponding resource file:

```csharp
using BlazorSourceGeneration.LocalizationServiceGenerator;

[LocalizationService("Localization.resx")]
public sealed partial class Localization { }
```

The attribute takes the name of the `.resx` file as a parameter.

**3) Registration:** The source generator will generate a service as a nested class which then can be registered
in the DI container. Also the ASP.Net localization middleware needs to be configured:

```csharp
builder.Services.AddLocalization();

builder.Services.AddScoped<Localization.ILocalizationService, Localization.LocalizationService>();

...

app.UseRequestLocalization(options =>
{
    var supportedCultures = new[] { "en-US", "de-DE" };

    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});
```

**4) Using the generated service:** To use the generated service, simply inject the `ILocalizationService`:

```html
@inject Localization.ILocalizationService _localization

<h1> @_localization.MyHeading </h1>
```
