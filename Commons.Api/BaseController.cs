using Commons.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Commons.Api
{
    public abstract class BaseController : ControllerBase
    {
        public virtual ILogger Logger { get; }
        public virtual IUnitOfWork UnitOfWork { get; }
        public BaseController(ILogger<BaseController> logger, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            UnitOfWork = unitOfWork;
        }
    }
}
