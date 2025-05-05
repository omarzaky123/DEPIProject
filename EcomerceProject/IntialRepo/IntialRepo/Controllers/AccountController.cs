using DEPI.Core.Models;
using DEPIAPI.DTO;
using IntialRepo.Core.IRepositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DEPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager
            ,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.roleManager = roleManager;
            this.unitOfWork = unitOfWork;
        }
        #region Register
        //This is For Admin Or For Guset
        [HttpPost("Register/{Role}")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model,string Role="Guset")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists");
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                Address = model.Address,
                PasswordHash = model.Password
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                List<string> descriptions = new List<string>();
                foreach (var error in result.Errors)
                {
                    descriptions.Add(error.Description);
                }
                return BadRequest(descriptions);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, Role);
            if (!roleResult.Succeeded)
            {
                return BadRequest("Failed to assign Guest role");
            }

            if (Role == "Guset")
            {
                #region Add Guest
                Guset guest = new Guset()
                {
                    UserId = user.Id
                };
                unitOfWork.GusetGeneralRepository.Add(guest);
                unitOfWork.Compelete();
                #endregion
            }
            var roles = await _userManager.GetRolesAsync(user);
            var response = new
            {
                Id = user.Id,
                UserName = user.UserName,
                Address = user.Address,
                Email = user.Email,
                Role = roles[0]
            };
            return Ok(response);
        } 
        #endregion


        #region Login

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized("Invalid username or password");
            }
            var roles = await _userManager.GetRolesAsync(user);

            var response = new
            {
                Id = user.Id,
                UserName = user.UserName,
                Address = user.Address,
                Email = user.Email,
                Role = roles[0] 
            };

            return Ok(response);
        } 
        #endregion

        #region Logout
        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully");
        }
        #endregion

        #region Role

        [HttpPost("Role")]
        public async Task<IActionResult> Role(Role roleVm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleVm.RoleName;
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {

                    return Ok(role);
                }
                else
                {
                    return BadRequest("The Role Is not Added");
                }
            }

            return BadRequest("The RoleInput Is Not Valid");
        }
        #endregion
    }
}



