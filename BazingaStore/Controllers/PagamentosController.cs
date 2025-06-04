using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BazingaStore.Data;
using BazingaStore.Model;

namespace BazingaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PagamentosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Pagamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamento()
        {
            return await _context.Pagamento.ToListAsync();
        }

        // GET: api/Pagamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(Guid id)
        {
            var pagamento = await _context.Pagamento.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return pagamento;
        }

        // PUT: api/Pagamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(Guid id, Pagamento pagamento)
        {
            if (id != pagamento.PagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
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

        // POST: api/Pagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Controllers/PagamentosController.cs

        // Controllers/PagamentosController.cs

        [HttpPost]
        public async Task<ActionResult<Pagamento>> CreatePagamento(Pagamento pagamento)
        {
            // Adiciona o pagamento
            _context.Pagamento.Add(pagamento);
            await _context.SaveChangesAsync();

            // Busca o pedido relacionado ao pagamento
            var pedido = await _context.Pedido.FindAsync(pagamento.PedidoId);

            if (pedido == null)
            {
                // Se não existir pedido, cria um novo pedido (ajuste conforme sua lógica de negócio)
                pedido = new Pedido
                {
                    PedidoId = pagamento.PedidoId,
                    DataPedido = DateTime.Now
                    // Adicione outros campos obrigatórios conforme necessário
                };
                _context.Pedido.Add(pedido);
                await _context.SaveChangesAsync();
            }

            // Busca o carrinho relacionado ao pagamento (usando CarrinhoId do pagamento)
            var carrinho = await _context.Carrinho
                .FirstOrDefaultAsync(c => c.CarrinhoId == pagamento.CarrinhoId);



            return CreatedAtAction("GetPagamento", new { id = pagamento.PagamentoId }, pagamento);
        }



        // DELETE: api/Pagamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(Guid id)
        {
            var pagamento = await _context.Pagamento.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamento.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoExists(Guid id)
        {
            return _context.Pagamento.Any(e => e.PagamentoId == id);
        }
    }
}
