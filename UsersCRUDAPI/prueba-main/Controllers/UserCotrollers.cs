using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppUsersAPI.DTOs;
using SystemAdminAPI.Entitties;
using AppSystemAdmin = AppUsersAPI.DbContextUsers.AppDbContext;

namespace SystemAdminAPI.Controllers  
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppSystemAdmin _db;

        public UsersController(AppSystemAdmin context)
        {
            _db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetAllUsers()
        {
            var users = _db.Users.AsNoTracking().ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<Users> GetUserById(int id)
        {
            var user = _db.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UsersDTOs>> CreateUser([FromBody] UsersDTOs userDTO)
        {
            var user = new Users
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Phone = userDTO.Phone
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var response = new UsersDTOs
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone
            };

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UsersDTOs userDTO)
        {
            var user = await _db.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;
            user.Phone = userDTO.Phone;

            _db.Entry(user).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Users.Any(u => u.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}