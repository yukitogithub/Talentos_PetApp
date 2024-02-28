using DataAccessLayer.BootcampDbContext;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BootcampDbContext _db;
        public UsersController(BootcampDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<User> GetUser(int userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }

        [HttpPost("PostUser")]
        public async Task<ActionResult> PostUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok();
        }
    }
}
