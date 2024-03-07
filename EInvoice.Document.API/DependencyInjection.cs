
using EInvoice.Document.API.Services;
using EInvoice.Document.Application.Common.Interfaces;
using EInvoice.Document.Application.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>(); //create an instance of CurrentUser when every the request come and died when the request is done
        return services;
    }

}

