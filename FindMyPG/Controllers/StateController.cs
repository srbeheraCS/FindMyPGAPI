using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.States;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    
    public class StateController : BaseController
    {
        private readonly IStateService _stateService;
        private readonly IMapper _mapper;
        public StateController(IStateService stateService, IMapper mapper)
        {
            _stateService = stateService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllStates()
        {
            var states = _stateService.GetAllStates();
            var stateModels = _mapper.Map<List<StateModel>>(states);
            return Ok(stateModels);
        }
        [HttpGet]
        [Route("Active")]
        public IActionResult GetAllActiveStates()
        {
            var states = _stateService.GetAllActiveStates();
            var stateModels = _mapper.Map<List<StateModel>>(states);
            return Ok(stateModels);
        }
        [HttpGet]
        [Route("api/v1/[controller]/{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = _stateService.GetStateById(id);
            var stateModel = _mapper.Map<StateModel>(state);
            return Ok(stateModel);
        }
        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes ="Bearer",Roles ="Owner")]
        public IActionResult InsertState(StateModelRequest request)
        { 
            if(_stateService.GetStateByName(request.StateName) == null)
            {
                _stateService.InsertState(_mapper.Map<State>(request));
                return Ok("Success");
            }
            return BadRequest("State Already Exist.");            
        }
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateState(int id, StateUpdateModelRequest request)
        {
            var state=_stateService.GetStateById(id);
            if(state!=null)
            {
                state.Name = request.StateName;
                state.IsActive=request.IsActive;
                _stateService.UpdateState(state);
                return Ok("Success");
            }
            return BadRequest("State is not exists");
        }
    }
}
