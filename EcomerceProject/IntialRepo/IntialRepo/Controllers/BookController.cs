using IntialRepo.Core.IRepositorys;
using IntialRepo.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntialRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _baseRepository;

        public BookController(IUnitOfWork baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public IActionResult GetById() {

            return Ok(_baseRepository.BookGeneralRepository.GetById(1));
        }
        [HttpGet("GetSpecial")]
        public IActionResult GetSpecial()
        {

            return Ok(_baseRepository.BookGeneralRepository.SpecialFunctionOfBook());
        }
        [HttpPost]
        public IActionResult Insert()
        {
            var book = _baseRepository.BookGeneralRepository.Add(new Book() { AuthorId = 1, Title = "Try" });
            _baseRepository.Compelete();
            return Ok(book);
        }

    }
}
