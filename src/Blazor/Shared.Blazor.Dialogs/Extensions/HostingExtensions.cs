using Microsoft.Extensions.DependencyInjection;

namespace Shared.Blazor.Dialogs.Extensions;

public static class HostingExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBlazorDialogServices()
        {
            services.AddLocalization();
            services.AddScoped<Localization.ILocalizationService, Localization.LocalizationService>();
            services.AddScoped<IModalService, ModalService>();
            return services;
        }
    }
}
