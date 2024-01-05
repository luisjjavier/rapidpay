using Core.PaymentFees;

namespace API.BackgroundProcesses
{
    /// <summary>
    /// Background service responsible for periodically updating the service fee using Universal Fee Exchange.
    /// </summary>
    public class UniversalFeeExchange : IHostedService, IDisposable
    {
        private readonly IPaymentFeeService _feeService;
        private readonly ILogger<UniversalFeeExchange> _logger;
        private Timer? _timer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalFeeExchange"/> class.
        /// </summary>
        /// <param name="feeService">The payment fee service to calculate the service fee.</param>
        /// <param name="logger">Logger for logging relevant information and errors.</param>
        public UniversalFeeExchange(IPaymentFeeService feeService, ILogger<UniversalFeeExchange> logger)
        {
            _feeService = feeService;
            _logger = logger;
        }

        /// <summary>
        /// Starts the background service and initializes the timer for periodic updates.
        /// </summary>
        /// <param name="stoppingToken">A <see cref="CancellationToken"/> that is triggered when the service is stopped.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Performs the work of updating the service fee.
        /// </summary>
        /// <param name="state">The state object passed to the timer (not used).</param>
        private void DoWork(object? state)
        {
            var newServiceFee = _feeService.CalculatePaymentFee();

            _logger.LogInformation("New service fee {newServiceFee}", newServiceFee);
        }

        /// <summary>
        /// Stops the background service and disposes of the timer.
        /// </summary>
        /// <param name="stoppingToken">A <see cref="CancellationToken"/> that is triggered when the service is stopped.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Disposes of the timer when the object is being disposed.
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
