using Azure.Messaging.ServiceBus;

namespace MyRecipeBook.Infrastructure.Services.ServiceBus;

public class DeleteUserProcessor(ServiceBusProcessor processor)
{
    public ServiceBusProcessor GetProcessor() => processor;
}
