using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.States;
using FindMyPG.Service.ZipCodes;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ZipCodeController : BaseController
    {
        private readonly IZipCodeService _zipCodeService;
        private readonly IMapper _mapper;
        public ZipCodeController(IZipCodeService zipCodeService, IMapper mapper)
        {
            _zipCodeService = zipCodeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("cityId")]
        public IActionResult GetAllActiveZipCodeByCityId(int cityId)
        {
            var zipcodes = _mapper.Map<List<ZipCodeModel>>
                (_zipCodeService.GetAllActiveZipCodeByCityId(cityId));

            return Ok(zipcodes);
        }
    }
        
}
