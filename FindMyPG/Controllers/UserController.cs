using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        
    }
}
