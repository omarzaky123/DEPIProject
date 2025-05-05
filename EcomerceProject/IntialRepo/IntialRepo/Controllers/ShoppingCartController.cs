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
    public class ShoppingCartController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet("{id:int}", Name = "GetByIDdynamicSearchTheUrlCart")]
        public IActionResult GetById(int id)
        {
            if (id != 0) {
                ShoppingCart shoppinCart = unitOfWork.ShoppingCartGeneralRepository.GetById(id);
                var shoppingDTO = _mapper.Map<ShoppinCartDTO>(shoppinCart);
                return Ok(shoppingDTO);
            }
            return BadRequest("The Id Is Invalid");
        }

        [HttpGet("ByGusetId/{gusetId:int}")]
        public IActionResult GetByGusetId(int gusetId)
        {
            if (gusetId != 0)
            {
                ShoppingCart shoppinCart = unitOfWork.ShoppingCartGeneralRepository
                    .Find(X => X.GusetId == gusetId);
                return Ok(shoppinCart);
            }
            return BadRequest("The Id Is Invalid");
        }

        [HttpPost("Insert")]
        public IActionResult Insert(ShoppinCartDTO shoppinCartDTO)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart shoppinCart = _mapper.Map<ShoppingCart>(shoppinCartDTO);
                unitOfWork.ShoppingCartGeneralRepository.Add(shoppinCart);
                unitOfWork.Compelete();
                string url = Url.Link("GetByIDdynamicSearchTheUrl", new { shoppinCart.Id });
                ShoppinCartDTO output = _mapper.Map<ShoppinCartDTO>(shoppinCart);
                return Created(url, output);
            }
            return BadRequest(shoppinCartDTO);

        }
        [HttpGet("GusetId/{gusetId:int}")]
        public IActionResult GetByGusetid(int gusetId)
        {
            if (gusetId != 0)
            {
                ShoppingCart shoppingCart = unitOfWork.ShoppingCartGeneralRepository.Find(X => X.GusetId == gusetId);
                ShoppinCartDTO shoppingCartDTO=_mapper.Map<ShoppinCartDTO>(shoppingCart); 
                return Ok(shoppingCartDTO);
            }
            return BadRequest("The Guset Is not Found");
        }

        [HttpGet("RelatedCard/{shopcartid:int}")]
        public async Task<IActionResult> RelatedCard(int shopcartid) {
            if (shopcartid != 0)
            {
                IEnumerable<CartItem> cartItems = await unitOfWork.CartItemGeneralRepository.FindAllAsync(X => X.ShoppingCartId == shopcartid, ["ProductVartion", "ProductVartion.Product"]);
                IEnumerable<CartItemDTO> cartItemDTOs = _mapper.Map<IEnumerable<CartItemDTO>>(cartItems);

                return Ok(cartItemDTOs);
            }
            return BadRequest("The CartItems Does Not Exsists");
        }
        [HttpDelete("Delete/{shopcartid:int}")]
        public async Task<IActionResult> Delete(int shopcartid) {

            if (shopcartid != 0)
            {
                ShoppingCart shoppingCart = await unitOfWork.ShoppingCartGeneralRepository.GetByIdAsync(shopcartid);
                if (shoppingCart != null)
                {
                    unitOfWork.ShoppingCartGeneralRepository.Delete(shoppingCart);
                    unitOfWork.Compelete();
                    return Ok();
                }
            }
            return BadRequest("The Id Is Invalid");
        }


    }
}

