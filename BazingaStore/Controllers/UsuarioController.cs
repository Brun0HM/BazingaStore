using BazingaStore.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BazingaStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            try
            {
                var usuarios = _userManager.Users
                    .Where(u => u.UserName != null && u.Email != null) // evita null
                    .Select(u => new
                    {
                        u.Id,
                        u.UserName,
                        u.Email,
                        u.NomeCompleto
                    })
                    .ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }


    }
}
