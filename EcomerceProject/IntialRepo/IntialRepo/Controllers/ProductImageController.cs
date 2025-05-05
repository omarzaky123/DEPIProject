//using AutoMapper;
//using DEPI.Core.Models;
//using DEPIAPI.DTO;
//using IntialRepo.Core.IRepositorys;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace DEPIAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductImageController : ControllerBase
//    {
//        private readonly IUnitOfWork unitOfWork;
//        private readonly IMapper mapper;

//        public ProductImageController(IUnitOfWork unitOfWork,IMapper mapper)
//        {
//            this.unitOfWork = unitOfWork;
//            this.mapper = mapper;
//        }

        
//        [HttpGet("GetAll")]
//        public async Task<IActionResult> GetAll() { 
        
//            IEnumerable<ProductImage> productImages = await unitOfWork.ProductImageRepository.FindAllAsync(X => X.Id != 0, ["Product"]);
//            if (productImages.Any()) {
//                IEnumerable<ProductImageDTO> productImageDTOs = mapper.Map<IEnumerable<ProductImageDTO>>(productImages);
//                return Ok(productImageDTOs);
//            }
//            return BadRequest("There Is No Items");
//        }

//        [HttpGet("GetbyId/{id:int}")]
//        public async Task<IActionResult> GetbyId(int id)
//        {
//            if (id != 0)
//            {
//                ProductImage productImage = await unitOfWork.ProductImageRepository.FindAsync(X => X.Id == id, ["Product"]);
//                if (productImage != null)
//                {
//                    ProductImageDTO productImageDTO = mapper.Map<ProductImageDTO>(productImage);
//                    return Ok(productImageDTO);
//                }
//            }
//            return BadRequest("There Is No Items");
//        }


//        [HttpPost("Insert")]

//        public IActionResult Insert(ProductImageInsertDTO productInsertDTO)
//        {
//            if (productInsertDTO != null)
//            {

//                ProductImage productImage = mapper.Map<ProductImage>(productInsertDTO);
//                if (productImage != null)
//                {

//                    unitOfWork.ProductImageRepository.Add(productImage);
//                    unitOfWork.Compelete();
//                    string url = Url.Link("GetByIDPRdTheUrl", new { productImage.Id });
//                    ProductImageInsertDTO output = mapper.Map<ProductImageInsertDTO>(productImage);
//                    return Created(url, output);
//                }
//                return BadRequest("The ProductImage Is not found");

//            }
//            return BadRequest("The Model Is Empty");
//        }

//        [HttpPut("Edit")]
//        public async Task<IActionResult> Edit(ProductImageInsertDTO newPrdDTO)
//        {

//            if (newPrdDTO != null)
//            {
//                ProductImage productImage = mapper.Map<ProductImage>(newPrdDTO);
//                if (productImage != null)
//                {
//                    unitOfWork.ProductImageRepository.Update(productImage);
//                    unitOfWork.Compelete();
//                    return Ok();
//                }
//            }
//            return BadRequest("The Model Is Invalid");
//        }


//        [HttpDelete("Delete/{id:int}")]
//        public async Task<IActionResult> Delete(int id)
//        {

//            if (id != 0)
//            {
//                ProductImage productImage = await unitOfWork.ProductImageRepository.FindAsync(X => X.Id == id);
//                if (productImage != null)
//                {
//                    unitOfWork.ProductImageRepository.Delete(productImage);
//                    unitOfWork.Compelete();
//                    return Ok();
//                }
//                return BadRequest("The the product not found");
//            }
//            return BadRequest("The id is invalid");
//        }



//    }
//}
