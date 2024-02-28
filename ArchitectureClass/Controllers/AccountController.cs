using BusinessAccessLayer.Dto;
using BusinessAccessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        //UserManager
        private readonly UserManager<User> _userManager;
        //SignInManager
        private readonly SignInManager<User> _signInManager;
        //TokenService
        public readonly ITokenService _tokenService;

        //Dependency Injection
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        //Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto login)
        {
            //Login

            //1. Check if user exists
            ////1.a. If user does not exist, return error
            //if (!UserExists(login.Username)) return BadRequest("Username or password incorrect");
            
            var user = _userManager.Users.SingleOrDefault(x => x.UserName == login.Username);
            if (user == null) return Unauthorized("Username or password incorrect");
            
            //2. Check if password is correct
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            ////2.a. If password is incorrect, return error
            if (!result.Succeeded) return Unauthorized("Username or password incorrect");
            
            //3. Generate token
            var token = await _tokenService.CreateToken(user);
            var userDto = new UserDto()
            {
                Username = user.UserName,
                Token = token
            };

            //4. Return token
            return Ok(userDto);
        }

        //Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto user)
        {
            //Register

            //1. Check if user exists
            ////1.a. If user exists, return error
            if (UserExists(user.Username)) return BadRequest("User already exists");

            //2. Create user
            var newUser = new User()
            {
                UserName = user.Username,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Username,
                Birthday = user.Birthday
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            
            //2.1 Add user to role
            var roleResult = await _userManager.AddToRoleAsync(newUser, "Owner");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            //3. Generate token
            var token = await _tokenService.CreateToken(newUser);

            var userDto = new UserDto()
            {
                Username = newUser.UserName,
                Token = token
            };

            //4. Return token
            return Ok(userDto);
        }

        //Create a method to check if user exists
        private bool UserExists(string username)
        {
            //Add code to check if user exists
            //return false;
            return _userManager.Users.Any(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
