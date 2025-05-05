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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        [HttpGet("Get")]
        public async Task<IActionResult> Index() {

            IEnumerable<Product> products = await unitOfWork.ProductGeneralRepository
                .FindAllAsync(X => X.Id != 0, 8, 0, ["Catigory"]);

            if (products!=null) {
                var productDTOs = _mapper.Map<List<ProductDTO>>(products);
                return Ok(productDTOs);
            }
            return BadRequest("The Products is not found");
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            IEnumerable<Product> products = await unitOfWork.ProductGeneralRepository
                .FindAllAsync(X => X.Id != 0, ["Catigory", "ProductImages"]);

            if (products != null)
            {
                var productDTOs = _mapper.Map<List<ProductDTO>>(products);
                foreach (var productDTO in productDTOs) {
                    productDTO.TotalNumberOfStock =
                     unitOfWork.ProductVartionGeneralRepository
                    .TotalForCeratinCondition(PV => PV.ProductID == productDTO.Id,
                            PV => PV.Quantity_In_Stock);

                }
                return Ok(productDTOs);
            }
            return BadRequest("The Products is not found");
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) { 
            Product product = unitOfWork.ProductGeneralRepository.Find(X => X.Id ==id, ["Catigory", "ProductImages"]);

            if (product != null)
            {
                IEnumerable<Product> RelatedProducts = await unitOfWork.ProductGeneralRepository
                    .FindAllAsync(X => X.CatigoryId == product.CatigoryId, 4, 0);
                IEnumerable<ProductVartion> RelatedProductVartions = await unitOfWork.ProductVartionGeneralRepository
                    .FindAllAsync(X => X.ProductID == product.Id);
                
               Vartion vartion = await unitOfWork.VartionGeneralRepository.FindAsync(X => X.CatigoryId == product.CatigoryId);
                if (vartion!=null)
                {
                    #region Mapping
                    var productDTO = _mapper.Map<ProductDTO>(product);
                    productDTO.TotalNumberOfStock =
                            unitOfWork.ProductVartionGeneralRepository
                            .TotalForCeratinCondition(PV => PV.ProductID == id,
                            PV => PV.Quantity_In_Stock);
                    productDTO.RelatedProduct = _mapper.Map<List<ProductDTO>>(RelatedProducts);
                    productDTO.RelatedProductVartions = _mapper.Map<List<ProductVartionDTO>>(RelatedProductVartions);
                    productDTO.VartionName = vartion.Name;
                    #endregion
                    return Ok(productDTO);
                }

            }
            return BadRequest("The Product Is Not Found");
        }


        [HttpPost("Insert")]

        public IActionResult Insert(ProductInsertDTO productInsertDTO)
        {
            if (productInsertDTO != null)
            {

                Product product = _mapper.Map<Product>(productInsertDTO);
                if (product != null)
                {

                    unitOfWork.ProductGeneralRepository.Add(product);
                    unitOfWork.Compelete();
                    string url = Url.Link("GetByIDPRdTheUrl", new { product.Id });
                    ProductInsertDTO output = _mapper.Map<ProductInsertDTO>(product);
                    return Created(url, output);
                }
                return BadRequest("The Product Is not found");

            }
            return BadRequest("The Model Is Empty");
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(ProductInsertDTO newPrdDTO)
        {

            if (newPrdDTO != null)
            {
                Product product = _mapper.Map<Product>(newPrdDTO);
                if (product != null)
                {
                    unitOfWork.ProductGeneralRepository.Update(product);
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
                Product product = await unitOfWork.ProductGeneralRepository.FindAsync(X => X.Id == id);
                if (product != null)
                {
                    unitOfWork.ProductGeneralRepository.Delete(product);
                    unitOfWork.Compelete();
                    return Ok();
                }
                return BadRequest("The the product not found");
            }
            return BadRequest("The id is invalid");
        }

        [HttpGet("GetByIdOnlyProduct/{id:int}")]
        public async Task<IActionResult> GetByIdOnlyProduct(int id)
        {
            if (id != 0) { 
            
                Product product = await unitOfWork.ProductGeneralRepository.FindAsync(X=>X.Id == id);
                if (product != null) { 
                    ProductInsertDTO output=_mapper.Map<ProductInsertDTO>(product);
                    return Ok(output);
                }
                return BadRequest("The Product Is Not Found");
            }
            return BadRequest("The ID Is Invalid");

        }

        [HttpGet("GetRelatedProudctImage/{productid:int}")]
        public async Task<IActionResult> GetRelatedProudctImage(int productid)
        {

            if (productid != 0)
            {
                IEnumerable<ProductImage> productImages = await unitOfWork.ProductImageRepository.FindAllAsync(X => X.ProductId == productid);
                if (productImages.Any())
                {
                    IEnumerable<ProductImageInsertDTO> output = _mapper.Map<IEnumerable<ProductImageInsertDTO>>(productImages);
                    return Ok(output);
                }
                return BadRequest("The Related Is Not Found");
            }
            return BadRequest("The id is invalid");
        }

    }
}
