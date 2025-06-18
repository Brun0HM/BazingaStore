using BazingaStore.Data;
using BazingaStore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BazingaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuariosController(ApiDbContext context)
        {
            _context = context;

        }


        // Endpoint para listar todos os usuários do Identity junto com suas roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuarios(
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuarios = await _context.Users.ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            var usuariosComRoles = new List<object>();

            foreach (var usuario in usuarios)
            {
                var roles = await userManager.GetRolesAsync(usuario);
                usuariosComRoles.Add(new
                {
                    usuario.Id,
                    usuario.UserName,
                    usuario.Email,
                    Roles = roles
                });
            }

            return Ok(usuariosComRoles);
        }

        // Endpoint para pegar o Id do Identity User (usuário logado), email e role através do token recebido no Header da Request
        [HttpGet("meu-id")]
        public ActionResult<object> GetMeuId()
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            return Ok(new
            {
                Id = userId,
                Email = email,
                Role = role
            });
        }
        // Adicione este método dentro da classe UsuariosController



        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(
            string id,
            [FromServices] UserManager<IdentityUser> userManager)
        {
            var usuario = await userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var result = await userManager.DeleteAsync(usuario);
            if (!result.Succeeded)
            {
                return BadRequest("Erro ao excluir o usuário.");
            }

            return NoContent();
        }

        [HttpGet("por-email")]
        public async Task<IActionResult> GetUsuarioPorEmail(
    [FromQuery] string email,
    [FromServices] UserManager<IdentityUser> userManager)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("E-mail é obrigatório.");

            var usuario = await userManager.FindByEmailAsync(email);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            var roles = await userManager.GetRolesAsync(usuario);

            return Ok(new
            {
                usuario.Id,
                usuario.UserName,
                usuario.Email,
                Roles = roles
            });
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(
     string id,
     [FromBody] UsuarioUpdateDto usuarioAtualizado,
     [FromServices] UserManager<IdentityUser> userManager,
     [FromServices] RoleManager<IdentityRole> roleManager)
        {
            var usuario = await userManager.FindByIdAsync(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            if (!string.IsNullOrWhiteSpace(usuarioAtualizado.UserName))
                usuario.UserName = usuarioAtualizado.UserName;

            if (!string.IsNullOrWhiteSpace(usuarioAtualizado.Email))
                usuario.Email = usuarioAtualizado.Email;

            var result = await userManager.UpdateAsync(usuario);
            if (!result.Succeeded)
                return BadRequest("Erro ao atualizar o usuário.");

            // Atualizar roles, se informado
            if (usuarioAtualizado.Roles != null && usuarioAtualizado.Roles.Any())
            {
                var rolesAtuais = await userManager.GetRolesAsync(usuario);
                var removeResult = await userManager.RemoveFromRolesAsync(usuario, rolesAtuais);
                if (!removeResult.Succeeded)
                    return BadRequest("Erro ao remover roles antigas.");

                foreach (var role in usuarioAtualizado.Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        return BadRequest($"Role '{role}' não existe.");

                    var addResult = await userManager.AddToRoleAsync(usuario, role);
                    if (!addResult.Succeeded)
                        return BadRequest($"Erro ao adicionar a role '{role}'.");
                }
            }

            return NoContent();
        }



    }
}
