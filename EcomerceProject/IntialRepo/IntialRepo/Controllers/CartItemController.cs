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
    public class CartItemController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public CartItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet("{id:int}", Name = "GetByIDdynamicSearchTheUrl")]
        public IActionResult GetById(int id)
        {
            if (id != 0)
            {
                CartItem cartItem = unitOfWork.CartItemGeneralRepository.GetById(id);
                var cartitemDto = _mapper.Map<CartItemDTO>(cartItem);
                return Ok(cartitemDto);
            }
            return BadRequest("The Id Is Invalid");
        }
        [HttpPost("Insert")]
        public IActionResult Insert(CartItemDTO CartDTO)
        {
            if (ModelState.IsValid)
            {
                CartItem cartItem = _mapper.Map<CartItem>(CartDTO);
                unitOfWork.CartItemGeneralRepository.Add(cartItem);
                unitOfWork.Compelete();
                string url = Url.Link("GetByIDdynamicSearchTheUrl", new { cartItem.Id });
                return Created(url, cartItem);
            }
            return BadRequest(CartDTO);

        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) {

            if (id != 0)
            {
                CartItem cartItem = await unitOfWork.CartItemGeneralRepository.FindAsync(X => X.Id == id);
                if (cartItem != null)
                {
                    unitOfWork.CartItemGeneralRepository.Delete(cartItem);
                    unitOfWork.Compelete();
                    return Ok();
                }
                return BadRequest("The the cartitem not found");
            }
            return BadRequest("The id is invalid");
        }

        
    }
}
