using AutoMapper;
using DEPI.Core.Models;
using DEPIAPI.DTO;
using IntialRepo.Core.IRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DEPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatigoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public CatigoryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet("Get")]
        public IActionResult Index()
        {
           IEnumerable<Catigory> catigories= unitOfWork.CatigoryGeneralRepository.GetAll();
            if (catigories.Any()) {
                var catigoryDTOs= _mapper.Map<List<CatigoryDTO>>(catigories);
                return Ok(catigoryDTOs);
            }
            return BadRequest("The catigories Is not Found");

        }
        [HttpGet("GetById/{id:int}", Name = "GetByIDCatTheUrl")]
        public async Task<IActionResult> GetById(int id) {

            if (id != 0) {

                Catigory catigory = await unitOfWork.CatigoryGeneralRepository.FindAsync(X=>X.Id==id);
                if (catigory != null) {
                    CatigoryDTO output = _mapper.Map<CatigoryDTO>(catigory);
                    return Ok(output);
                    }
                return BadRequest("The Catigory Is not Found");
            }
        return BadRequest("The Id Is Invalid");
        
        }
        [HttpGet("GetByIdWithRelatedPrd/{id:int}")]
        public async Task<IActionResult> GetByIdWithRelatedPrd(int id) {

            if (id != 0)
            {
                Catigory catigory = await unitOfWork.CatigoryGeneralRepository.FindAsync(X => X.Id == id, ["Products"]);
                if (catigory != null)
                {
                    CatigoryWithPrdDTO output = _mapper.Map<CatigoryWithPrdDTO>(catigory);
                    return Ok(output);
                }
                return BadRequest("The Catigory Is not Found");
            }
            return BadRequest("The Id Is Invalid");


        }


        [HttpPost("Insert")]

        public IActionResult Insert(CatigoryDTO catigoryDTO)
        {
            if (catigoryDTO != null) { 
            
               Catigory catigory= _mapper.Map<Catigory>(catigoryDTO);
                if (catigory != null) {
                
                    unitOfWork.CatigoryGeneralRepository.Add(catigory);
                    unitOfWork.Compelete();
                    string url = Url.Link("GetByIDCatTheUrl", new { catigory.Id });
                    CatigoryDTO output = _mapper.Map<CatigoryDTO>(catigory);
                    return Created(url, output);
                }
                return BadRequest("The Catigory Is not found");

            }
            return BadRequest("The Model Is Empty");
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(CatigoryDTO newCatDTO)
        {

            if (newCatDTO != null)
            {
                Catigory catigory = _mapper.Map<Catigory>(newCatDTO);
                if (catigory != null)
                {
                    unitOfWork.CatigoryGeneralRepository.Update(catigory);
                    unitOfWork.Compelete();
                    return Ok();
                }
            }
            return BadRequest("The Model Is Invalid");
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id != 0)
            {
                Catigory catigory = await unitOfWork.CatigoryGeneralRepository.FindAsync(X => X.Id == id);
                if (catigory != null)
                {
                    unitOfWork.CatigoryGeneralRepository.Delete(catigory);
                    unitOfWork.Compelete();
                    return Ok();
                }
                return BadRequest("The the catigory not found");
            }
            return BadRequest("The id is invalid");
        }
    }


}

