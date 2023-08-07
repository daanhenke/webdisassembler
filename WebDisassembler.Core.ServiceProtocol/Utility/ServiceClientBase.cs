using MassTransit;

namespace WebDisassembler.Core.ServiceProtocol.Utility;

public abstract class ServiceClientBase
{
    private readonly IScopedClientFactory _clientFactory;

    protected ServiceClientBase(IScopedClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    protected async ValueTask<TResponse> SendRequest<TRequest, TResponse>(TRequest request)
        where TRequest : class
        where TResponse : class
    {
        var client = _clientFactory.CreateRequestClient<TRequest>();
        var response = await client.GetResponse<TResponse>(request);

        return response.Message;
    }
}