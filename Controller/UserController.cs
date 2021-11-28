using IAAPIUsers.Data;
using IAAPIUsers.Models;
using IAAPIUsers.Services;
using IAAPIUsers.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
                Username = modelUser.Username,
                Age = modelUser.Age,
                Password = modelUser.Password,
                Role = modelUser.Role
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
                user.Username = modelUser.Username;
                user.Age = modelUser.Age;
                user.Password = modelUser.Password;
                user.Role = modelUser.Role;

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

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(
            [FromServices] AppDbContext context, 
            [FromBody] GetUserViewModel model)
        {
            var user = await context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => 
                u.Username.ToLower() == model.Username.ToLower() && 
                u.Password == model.Password.ToLower());

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado - {User.Identity.Name}";

        [HttpGet("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}
