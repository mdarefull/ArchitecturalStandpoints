using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Commons.AspNet
{
    public abstract class BaseController : ControllerBase
    {
        protected ILogger Logger { get; }

        public BaseController(ILogger logger = null) => Logger = logger ?? NullLogger.Instance;
    }
}
