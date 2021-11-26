using IAAPIUsers.Data;
using IAAPIUsers.Models;
using IAAPIUsers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IAAPIUsers.Controller
{
    [ApiController]
    [Route("v1")]
    public class UserController: ControllerBase
    {
        [HttpGet("users")]
        public async Task<IActionResult> GetAsync(
            [FromServices]AppDbContext context)
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return Ok(users);
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute]int id)
        {
            var user = await context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return user == null
                ? NotFound()
                : Ok(user);
        }

        [HttpPost("users")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody]CreateUserViewModel modelUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                Name = modelUser.Name,
                Age = modelUser.Age,
                Password = modelUser.Password
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Created($"v1/users/{user.Id}", user);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }
        [HttpPut("users/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] UpdateUserViewModel modelUser,
            [FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                NotFound();

            try
            {
                user.Name = modelUser.Name;
                user.Age = modelUser.Age;
                user.Password = modelUser.Password;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Id == id);

            try
            {
                context
                    .Users
                    .Remove(user);
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

    }
}
