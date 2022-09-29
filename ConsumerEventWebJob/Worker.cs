namespace ConsumerEventWebJob
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer _consumer;
        private Timer? _timer = null;
        public Worker(ILogger<Worker> logger, IConsumer consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _timer = new Timer(ConsumerCharge, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(100));

            return Task.CompletedTask;
        }

        public async void ConsumerCharge(object? state)
        {
             _consumer.ConsumerReadEvent("$Default");

        }

     }
}