using RSSFeeds.Services.Shared;

namespace RSSFeeds
{
    public class Start : BackgroundService
    {


        private readonly IRSSUpdaterService RSSUpdaterService;

        public Start(IRSSUpdaterService RSSUpdaterService)
        {

            this.RSSUpdaterService = RSSUpdaterService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    RSSUpdaterService.Start();
                }
            });

        }
    }
}
