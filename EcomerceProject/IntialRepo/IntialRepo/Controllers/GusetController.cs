using AutoMapper;
using DEPI.Core.Models;
using DEPIAPI.DTO;
using IntialRepo.Core.IRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GusetController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

       
        public GusetController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet("ByUser/{userid}")]
        public async Task<IActionResult> GetByUserId(string userid) {

            if (userid != null)
            {
                Guset guset = await unitOfWork.GusetGeneralRepository.FindAsync(X => X.UserId == userid);
                if (guset != null)
                {
                    GusetDTO gusetDTO = _mapper.Map<GusetDTO>(guset);
                    return Ok(gusetDTO);
                }
                return BadRequest("The Guset IS not Found");
            }
            return BadRequest("The Guset IS not Found");
        }

    }
}
