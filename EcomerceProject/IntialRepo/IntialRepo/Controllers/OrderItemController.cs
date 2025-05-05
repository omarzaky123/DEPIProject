using AutoMapper;
using DEPI.Core.Models;
using DEPIAPI.DTO;
using IntialRepo.Core.IRepositorys;
using IntialRepo.EF.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DEPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderItemController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet("{id:int}", Name = "GetByIDOrderItemdynamicSearchTheUrl")]
        public async Task<IActionResult> GetById(int id) {
            if (id != 0) { 
                
               OrderItem orderItem = await unitOfWork.OrderItemGeneralRepository.GetByIdAsync(id);
               OrderItemDTO orderItemDTO = mapper.Map<OrderItemDTO>(orderItem);
                if(orderItemDTO != null) 
                    return Ok(orderItemDTO);    
            }
            return BadRequest("The Order Item Does Not Found");
        }    

        [HttpPost("Insert")]

        public IActionResult Insert(OrderItemInsert orderItemDTO) {

            if (ModelState.IsValid)
            {
                OrderItem orderItem= mapper.Map<OrderItem>(orderItemDTO);
                unitOfWork.OrderItemGeneralRepository.Add(orderItem);
                unitOfWork.Compelete();
                string url = Url.Link("GetByIDOrderITem", new { orderItem.Id });
                OrderItemDTO output=mapper.Map<OrderItemDTO>(orderItem);
                return Created(url, output);
            }
            return BadRequest(orderItemDTO);
        }


    }
}
