using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.Cities;
using FindMyPG.Service.States;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllCities()
        {
            var cities = _cityService.GetAllCities();
            var cityModels = _mapper.Map<List<CityModel>>(cities);
            return Ok(cityModels);
        }
        [HttpGet]
        [Route("Active")]
        public IActionResult GetAllActiveCities()
        {
            var cities = _cityService.GetAllActiveCities();
            var cityModels = _mapper.Map<List<CityModel>>(cities);
            return Ok(cityModels);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _cityService.GetCityById(id);
            var cityModel = _mapper.Map<CityModel>(city);

            return Ok(cityModel);
        }
        [HttpGet]
        [Route("state/{StateId}")]
        public IActionResult GetAllActiveCityByStateId(int StateId)
        {
            var cityModels = _mapper.Map<List<CityModel>>
                (_cityService.GetAllActiveCities());
            return Ok(cityModels);
        }
        //[HttpPost]
        //[Route("")]
        //public IActionResult InsertCity(CityModelRequest request)
        //{ 
        //    if(_cityService.GetStateByName(request.StateName) == null)
        //    {
        //        _stateService.InsertState(_mapper.Map<State>(request));
        //        return Ok("Success");
        //    }
        //    return BadRequest("State Already Exist.");            
        //}
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCity(int id, CityModelRequest request)
        {
            var city = _cityService.GetCityById(id);
            if (city != null)
            {
                city.Name = request.CityName;
                city.IsActive = request.IsActive;
                _cityService.UpdateCity(city);
                return Ok("Success");
            }
            return BadRequest("City does not exist");
        }
    }
}
