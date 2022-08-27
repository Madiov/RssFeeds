using Fanap.MarginParkingPlus.Services.Shared;
using RSSFeeds.Database.Shared;

namespace RSSFeeds.Services.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly ILoggerManager logger;
        protected readonly IConfiguration configuration;
        //protected readonly IMapper mapper;
        public BaseService(IUnitOfWork unitOfWork, ILoggerManager logger, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.configuration = configuration;
            //this.mapper = mapper;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
                unitOfWork.Dispose();
        }

    }
}
