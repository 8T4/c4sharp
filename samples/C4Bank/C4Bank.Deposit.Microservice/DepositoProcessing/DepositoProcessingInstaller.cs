using C4Bank.Deposit.Microservice.DepositoProcessing.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace C4Bank.Deposit.Microservice.DepositoProcessing;

public static class DepositoProcessingInstaller
{
    public static IServiceCollection AddDepositoProcessing(this IServiceCollection services)
    {
        //TODO: Dependecy Injection
        
        return services;
    }
}