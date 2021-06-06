using C4Sharp.Models;

namespace C4Sharp.Sample.Orders
{
    public static class Structures
    {
        public static Component OrdersController => new(
            alias: nameof(OrdersController),
            label: nameof(OrdersController),
            description: "Orders controller",
            technology: "Asp.net REST API"
        );
        
        public static Component IOrdersService => new(
            alias: nameof(IOrdersService),
            label: nameof(IOrdersService),
            description: "Orders service interface",
            technology: "C# Interface"
        );    
        
        public static Component OrdersService => new(
            alias: nameof(OrdersService),
            label: nameof(OrdersService),
            description: "Orders service concret",
            technology: "C# class"
        );   
        
        public static Component IOrdersRepository => new(
            alias: nameof(IOrdersRepository),
            label: nameof(IOrdersRepository),
            description: "Orders repository interface",
            technology: "C# interface"
        );    
        
        public static Component OrdersRepository => new(
            alias: nameof(OrdersRepository),
            label: nameof(OrdersRepository),
            description: "Orders repository",
            technology: "C# class / Dapper Framework"
        );         
    }
}