using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStoreService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController(ISender sender) : ControllerBase
    {
        protected readonly ISender _sender = sender;
    }
}
