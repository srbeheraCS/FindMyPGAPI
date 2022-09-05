using FindMyPG.Controllers.Base;
using FindMyPG.Models;
using FindMyPG.Service.PGInfos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FindMyPG.Core.Entities;

namespace FindMyPG.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PGInfoController : BaseController
    {
        private readonly IPGInfoService _pGInfoService;
        private readonly IMapper _mapper;

        public PGInfoController(IPGInfoService pGInfoService, IMapper mapper)
        {
            _pGInfoService = pGInfoService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("")]
        public IActionResult InsertPGInfo(PGInfoModel request)
        {
            if (ModelState.IsValid)
            {
                _pGInfoService.insertPGInfo(_mapper.Map<PGInfo>(request));
                return Ok("Success");
            }

            return BadRequest(ModelState);
        }

    }
}
