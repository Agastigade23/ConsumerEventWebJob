using ConsumerEventWebJob;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IConsumer, Consumer>();
    })
    .Build();

await host.StartAsync();
await host.WaitForShutdownAsync();
