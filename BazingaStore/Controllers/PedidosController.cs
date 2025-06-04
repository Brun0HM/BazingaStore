using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BazingaStore.Data;
using BazingaStore.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Azure.Core;

namespace BazingaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PedidosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedido()
        {
            return await _context.Pedido.ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(Guid id)
        {
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(Guid id, Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {

            // Obter usuário autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Usuário não autenticado");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Usuário não encontrado");



            return CreatedAtAction("GetPedido", new { id = pedido.PedidoId }, pedido);
        }

        //[HttpPost]
        //public async Task<IActionResult> CriarPedido([FromBody] Pedido request)
        //{
        //    // Obter usuário autenticado
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);
        //    // Se nao exitir usuario, retorne NotFound()


        //    // Buscar produto e cupom
        //    var produto = await _context.Produto.FindAsync(request.ProdutoId);
        //    if (produto == null)
        //        return NotFound("Produto não encontrado");

        //    CupomDesconto cupom = null;
        //    decimal valorOriginal = produto.Preco;
        //    decimal valorDesconto = 0;

        //    if (request.CupomDescontoId.HasValue)
        //    {
        //        cupom = await _context.CupomDesconto.FindAsync(request.CupomDescontoId.Value);
        //        if (cupom == null || !cupom.Ativo || cupom.DataValidade < DateTime.UtcNow)
        //            return BadRequest("Cupom inválido");

        //        valorDesconto = valorOriginal * (cupom.PercentualDesconto / 100m);
        //    }

        //    decimal valorTotal = valorOriginal - valorDesconto;

        //    // Criar o pedido
        //    var pedido = new Pedido
        //    {
        //        UserId = Guid.Parse(userId),
        //        ProdutoId = request.ProdutoId,
        //        CupomDescontoId = cupom?.CupomDescontoId,
        //        ValorOriginal = valorOriginal,
        //        ValorDesconto = valorDesconto,
        //        ValorTotal = valorTotal,
        //        DataPedido = DateTime.Now
        //    };

        //    _context.Pedido.Add(pedido);
        //    await _context.SaveChangesAsync();

        //    // Retornar o pedido com os cálculos
        //    return Ok(new
        //    {
        //        pedido.PedidoId,
        //        Produto = produto.Nome,
        //        ValorOriginal = pedido.ValorOriginal,
        //        Desconto = pedido.ValorDesconto,
        //        ValorTotal = pedido.ValorTotal,
        //        Data = pedido.DataPedido
        //    });
        //}

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(Guid id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(Guid id)
        {
            return _context.Pedido.Any(e => e.PedidoId == id);
        }
    }
}
