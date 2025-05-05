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
    public class VartionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VartionController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("Get")]
        public IActionResult Index()
        {
            IEnumerable<Vartion> vartions = unitOfWork.VartionGeneralRepository.GetAll();
            if (vartions.Any())
            {
                List<VartionInsertDTO> vartionDTOs = mapper.Map<List<VartionInsertDTO>>(vartions);
                return Ok(vartionDTOs);
            }
            return BadRequest("The vartions Is not Found");

        }
        [HttpGet("GetWithCatigory")]
        public async Task<IActionResult> GetWithCatigory()
        {
            IEnumerable<Vartion> vartions = await unitOfWork.VartionGeneralRepository
                .FindAllAsync(X => X.Id != 0, ["Catigory", "ProductVartions"]);
            if (vartions.Any())
            {
                List<VartionWithCatDTO> vartionDTOs = mapper.Map<List<VartionWithCatDTO>>(vartions);
                return Ok(vartionDTOs);
            }
            return BadRequest("The vartions Is not Found");

        }

        [HttpGet("GetById/{id:int}", Name = "GetByIDVarTheUrl")]
        public async Task<IActionResult> GetById(int id)
        {

            if (id != 0)
            {
                Vartion vartion = await unitOfWork.VartionGeneralRepository.FindAsync(X => X.Id == id, ["Catigory", "ProductVartions.Product"]);
                if (vartion != null)
                {
                    VartionWithCatDTO output = mapper.Map<VartionWithCatDTO>(vartion);
                    return Ok(output);
                }
                return BadRequest("The Vartion Is not Found");
            }
            return BadRequest("The Id Is Invalid");

        }
        [HttpGet("GetByProductId/{productid:int}")]
        public async Task<IActionResult> GetByProductId(int productid)
        {

            if (productid != 0)
            {
                Vartion vartion=null;
                Product product= await unitOfWork.ProductGeneralRepository.FindAsync(X=>X.Id==productid);
                if (product != null)
                    vartion = await unitOfWork.VartionGeneralRepository.FindAsync(X => X.CatigoryId == product.CatigoryId);
                
                if (vartion != null)
                {
                    VartionInsertDTO output = mapper.Map<VartionInsertDTO>(vartion);
                    return Ok(output);
                }
                return BadRequest("The Vartion Is not Found");
            }
            return BadRequest("The Id Is Invalid");

        }


        [HttpPost("Insert")]

        public IActionResult Insert(VartionInsertDTO vartionInsertDTO)
        {
            if (vartionInsertDTO != null)
            {

                Vartion vartion = mapper.Map<Vartion>(vartionInsertDTO);
                if (vartion != null)
                {

                    unitOfWork.VartionGeneralRepository.Add(vartion);
                    unitOfWork.Compelete();
                    string url = Url.Link("GetByIDVarTheUrl", new { vartion.Id });
                    VartionInsertDTO output = mapper.Map<VartionInsertDTO>(vartion);
                    return Created(url, output);
                }
                return BadRequest("The Vartion Is not found");

            }
            return BadRequest("The Model Is Empty");
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(VartionInsertDTO vartionInsertDTO)
        {

            if (vartionInsertDTO != null)
            {
                Vartion vartion = mapper.Map<Vartion>(vartionInsertDTO);
                if (vartion != null)
                {
                    unitOfWork.VartionGeneralRepository.Update(vartion);
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
                Vartion vartion = await unitOfWork.VartionGeneralRepository.FindAsync(X => X.Id == id);
                if (vartion != null)
                {
                    unitOfWork.VartionGeneralRepository.Delete(vartion);
                    unitOfWork.Compelete();
                    return Ok();
                }
                return BadRequest("The the vartion not found");
            }
            return BadRequest("The id is invalid");
        }





    }
}
