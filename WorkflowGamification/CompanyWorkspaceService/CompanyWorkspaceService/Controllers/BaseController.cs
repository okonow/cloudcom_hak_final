using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWorkspaceService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController(ISender sender) : ControllerBase
    {
        protected readonly ISender _sender = sender;
    }
}
