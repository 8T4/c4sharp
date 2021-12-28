using C4Sharp.Models;

namespace C4Sharp.Sample.Orders;

public static class Structures
{
    public static Component OrdersController => new(
        nameof(OrdersController),
        nameof(OrdersController))
    {
        Description = "Orders controller",
        Technology = "Asp.net REST API"
    };

    public static Component IOrdersService => new(
        nameof(IOrdersService),
        nameof(IOrdersService))
    {
        Description = "Orders service interface",
        Technology = "C# Interface"
    };

    public static Component OrdersService => new(
        nameof(OrdersService),
        nameof(OrdersService))
    {
        Description = "Orders service concret",
        Technology = "C# class"
    };

    public static Component IOrdersRepository => new(
        nameof(IOrdersRepository),
        nameof(IOrdersRepository))
    {
        Description = "Orders repository interface",
        Technology = "C# interface"
    };

    public static Component OrdersRepository =>
        new(nameof(OrdersRepository), nameof(OrdersRepository))
        {
            Description = "Orders repository",
            Technology = "C# class / Dapper Framework"
        };
}
