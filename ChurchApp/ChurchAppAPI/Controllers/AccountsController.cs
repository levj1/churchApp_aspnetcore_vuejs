using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Extensions.Error;
using ChurchAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly JwtSettings _jwtSettings = null;

        public AccountsController(UserManager<AppUser> userManager,
            ChurchAppContext context,
            SignInManager<AppUser> signInManager,
            IMapper mapper,
            JwtSettings jwtSettings)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
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
            var existingUserClaims = await _userManager.GetClaimsAsync(userInDb);
            var claims = existingUserClaims.Select(x => x.Type).ToList();
            var token = BuildJwtToken(userInDb.UserName, existingUserClaims);

            if (userInDb == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<AppUserDTo>(userInDb);
            userDto.IsAuthenticated = true;
            userDto.BearerToken = token;
            userDto.UserClaims = claims;

            return Ok(userDto);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> Claims([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
                return NotFound();

            var existingUserClaims = await _userManager.GetClaimsAsync(user);


            return Ok(existingUserClaims);
        }

        [HttpPost("createClaim")]
        public async Task<IActionResult> CreateClaim([FromBody]ClaimDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
                return NotFound();

            var existingUserClaims = await _userManager.AddClaimAsync(user, new Claim(model.ClaimType, model.ClaimValue));

            return Ok();
        }


        private string BuildJwtToken(string userName, IList<Claim> existingUserClaims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            // Create standard JWT Claims 
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub,
                userName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            // Add Custom Claim
            foreach (var claim in existingUserClaims)
            {
                jwtClaims.Add(new Claim(claim.Type, claim.Value));
            }

            // Create the JwtSecurityToken object 
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: jwtClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
