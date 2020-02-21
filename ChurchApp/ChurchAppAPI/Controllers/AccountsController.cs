using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Extensions.Error;
using ChurchAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController: ControllerBase
    {
        private readonly ChurchAppContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,
            ChurchAppContext context,
            SignInManager<AppUser> signInManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] AppUserDTo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<AppUserDTo>(user);
            return CreatedAtAction("Post", new { id = user.Id }, userDto);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            var attemptSignIn = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

            if (!attemptSignIn.Succeeded)
            {
                return BadRequest();
            }

            var userInDb = _userManager.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            if(userInDb == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<AppUserDTo>(userInDb);

            return Ok(userDto);
        }
    }
}
