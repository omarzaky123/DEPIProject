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
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll() { 
        
            IEnumerable<Order> orders =  await unitOfWork.OrderGeneralRepository.FindAllAsync(X => X.Id != 0, ["Guset.User"]);
            if (orders.Any()) { 
                List<OrderDTO> orderDTOs = mapper.Map<List<OrderDTO>>(orders);
                return Ok(orderDTOs);
            }
            return BadRequest("The Orders Is null");
        }


        [HttpGet("{id:int}", Name = "GetByIDOrderdynamicSearchTheUrl")]
        public IActionResult GetById(int id)
        {
            if (id != 0)
            {
                Order Order = unitOfWork.OrderGeneralRepository.GetById(id);
                var OrderDTO = mapper.Map<OrderDTO>(Order);
                return Ok(OrderDTO);
            }
            return BadRequest("The Id Is Invalid");
        }
        [HttpGet("GetByIdWithItems/{id:int}")]
        public async Task<IActionResult> GetByIdWithItems(int id)
        {
            if (id != 0)
            {
                Order Order = await unitOfWork.OrderGeneralRepository
                    .FindAsync(X => X.Id == id, ["OrderItems", "OrderItems.ProductVartion", "OrderItems.ProductVartion.Product", "OrderItems.ProductVartion.Product.Catigory", "Guset.User"]);
                OrderDTO OrderDTO = mapper.Map<OrderDTO>(Order);
                return Ok(OrderDTO);
            }
            return BadRequest("The Id Is Invalid");
        }

        [HttpPost("Insert")]
        public IActionResult Insert(OrderDTOInsert OrderDTO)
        {
            if (ModelState.IsValid)
            {
                Order Order = mapper.Map<Order>(OrderDTO);
                unitOfWork.OrderGeneralRepository.Add(Order);
                unitOfWork.Compelete();
                string url = Url.Link("GetByIDdynamicSearchTheUrl", new { Order.Id });
                OrderDTOInsert output =mapper.Map<OrderDTOInsert>(Order);    
                return Created(url, output);
            }
            return BadRequest(OrderDTO);

        }
        [HttpGet("GetAllByGuset/{gusetid:int}")]
        public async Task<IActionResult> GetAllByGuset(int gusetid) {

            if (gusetid != 0) {

                IEnumerable<Order> orders = await unitOfWork.OrderGeneralRepository
                    .FindAllAsync(X => X.GusetId == gusetid, ["OrderItems"]);
                if (orders.Any())
                {
                    IEnumerable<OrderDTO> orderDTOs = mapper.Map<IEnumerable<OrderDTO>>(orders);
                    return Ok(orderDTOs);
                }
                return BadRequest("Not Found the orders");
            
            }
            return BadRequest("The ID is Invalid");
        }
        [HttpDelete("Delete/{id:int}")]
        public IActionResult Delete(int id) {

            if (id != 0)
            {
                Order order = unitOfWork.OrderGeneralRepository.GetById(id);
                if (order != null)
                {
                    unitOfWork.OrderGeneralRepository.Delete(order);
                    unitOfWork.Compelete();
                    return Ok();
                }
                return BadRequest("The Order is Invalid");
            }
            return BadRequest("The ID is Invalid");
        
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(OrderDTOInsert neworderDTO)
        {

            if (neworderDTO != null)
            {
                Order order = mapper.Map<Order>(neworderDTO);
                if (order != null)
                {
                    unitOfWork.OrderGeneralRepository.Update(order);
                    unitOfWork.Compelete();
                    return Ok();
                }
            }
            return BadRequest("The Model Is Invalid");
        }
    }
}
