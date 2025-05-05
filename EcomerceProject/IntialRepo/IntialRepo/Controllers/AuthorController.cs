using IntialRepo.Core.IRepositorys;
using IntialRepo.Core.Models;
using IntialRepo.Core.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntialRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork _baseRepository;

        public AuthorController(IUnitOfWork baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #region Actions

        [HttpGet]
        public IActionResult GetById() {

            return Ok(_baseRepository.AuthorGeneralRepository.GetById(1));
        }
        [HttpGet("GetAsync")]
        public IActionResult GetByIdAsync()
        {

            return Ok(_baseRepository.AuthorGeneralRepository.GetById(1));
        }

        #endregion


    }
}
