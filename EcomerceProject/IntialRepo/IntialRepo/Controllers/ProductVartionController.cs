using AutoMapper;
using DEPI.Core.Models;
using DEPIAPI.DTO;
using IntialRepo.Core.IRepositorys;
using IntialRepo.EF.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVartionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductVartionController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("GetAllWithPrd")]

        public async Task<IActionResult> GetAllWithPrd() {

            IEnumerable<ProductVartion> productVartions = await unitOfWork.ProductVartionGeneralRepository
                .FindAllAsync(X => X.Id != 0, ["Product"]);
            if (productVartions.Any()) {
                List<ProductVartionDTO> productVartionDTOs = mapper.Map<List<ProductVartionDTO>>(productVartions);
                return Ok(productVartionDTOs);
            }
            return BadRequest("The ProductVarion Is Null");
            
        }

        [HttpGet("GetById/{id:int}", Name = "GetByIDPRDVarTheUrl")]
        public IActionResult GetById(int id)
        {
            if (id != 0)
            {
                ProductVartion PrdVar = unitOfWork.ProductVartionGeneralRepository.GetById(id);
                ProductVartionDTOInsert PrdDTO = mapper.Map<ProductVartionDTOInsert>(PrdVar);
                return Ok(PrdDTO);
            }
            return BadRequest("The Id Is Invalid");
        }
        [HttpGet("GetByIdWithPrd/{id:int}")]
        public async Task<IActionResult> GetByIdWithPrd(int id)
        {
            if (id != 0)
            {
                ProductVartion PrdVar = await unitOfWork.ProductVartionGeneralRepository.FindAsync(X => X.Id == id, ["Product", "Product.Catigory"]);
                if (PrdVar != null)
                {
                    ProductVartionDTO PrdDTO = mapper.Map<ProductVartionDTO>(PrdVar);
                    return Ok(PrdDTO);
                }
                return NotFound();
            }
            return BadRequest("The Id Is Invalid");
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(ProductVartionDTOInsert newprdvartionDTO) {

            if (newprdvartionDTO != null)
            {
                ProductVartion prdvartion = mapper.Map<ProductVartion>(newprdvartionDTO);
                if (prdvartion != null)
                {
                    unitOfWork.ProductVartionGeneralRepository.Update(prdvartion);
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
                ProductVartion productVartion = await unitOfWork.ProductVartionGeneralRepository.FindAsync(X => X.Id == id);
                if (productVartion != null)
                {
                    unitOfWork.ProductVartionGeneralRepository.Delete(productVartion);
                    unitOfWork.Compelete();
                    return Ok();
                }
                return BadRequest("The the ProductVartion not found");
            }
            return BadRequest("The id is invalid");
        }

        [HttpPost("Insert")]

        public IActionResult Insert(ProductVartionDTOInsert productInsertDTO)
        {
            if (productInsertDTO != null)
            {

                ProductVartion productVartion = mapper.Map<ProductVartion>(productInsertDTO);
                if (productVartion != null)
                {

                    unitOfWork.ProductVartionGeneralRepository.Add(productVartion);
                    unitOfWork.Compelete();
                    string url = Url.Link("GetByIDPRDVarTheUrl", new { productVartion.Id });
                    ProductVartionDTOInsert output = mapper.Map<ProductVartionDTOInsert>(productVartion);
                    return Created(url, output);
                }
                return BadRequest("The ProductVartion Is not found");

            }
            return BadRequest("The Model Is Empty");
        }
    }
}
