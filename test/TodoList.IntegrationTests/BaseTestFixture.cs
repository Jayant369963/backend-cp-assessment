using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TodoList.Api;

namespace TodoList.IntegrationTests;

public class BaseTestFixture
{
    private IConfigurationRoot configuration;
    private IServiceScopeFactory scopeFactory;

    public BaseTestFixture()
    {
        var builder = new ConfigurationBuilder();

        configuration = builder.Build();
        var services = new ServiceCollection();
        var startup = new Startup(configuration);

        startup.ConfigureServices(services);

        scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetService<IMediator>();

        return await mediator.Send(request);
    }
}