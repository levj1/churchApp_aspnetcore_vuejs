using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Extensions.Error;
using ChurchAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChurchAppAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController: ControllerBase
    {
        private readonly ChurchAppContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,
            ChurchAppContext context,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
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

            var userDto = _mapper.Map<AppUserDTo>(user);
            return CreatedAtAction("Post", new { id = user.Id }, userDto);

        }
    }
}
