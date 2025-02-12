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
    public class CarrinhosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CarrinhosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Carrinhos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrinho>>> GetCarrinho()
        {
            return await _context.Carrinho.ToListAsync();
        }

        // GET: api/Carrinhos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrinho>> GetCarrinho(Guid id)
        {
            var carrinho = await _context.Carrinho.FindAsync(id);

            if (carrinho == null)
            {
                return NotFound();
            }

            return carrinho;
        }

        // PUT: api/Carrinhos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrinho(Guid id, Carrinho carrinho)
        {
            if (id != carrinho.CarrinhoId)
            {
                return BadRequest();
            }

            _context.Entry(carrinho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoExists(id))
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

        // POST: api/Carrinhos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrinho>> PostCarrinho(Carrinho carrinho)
        {
            _context.Carrinho.Add(carrinho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinho", new { id = carrinho.CarrinhoId }, carrinho);
        }

        // DELETE: api/Carrinhos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrinho(Guid id)
        {
            var carrinho = await _context.Carrinho.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }

            _context.Carrinho.Remove(carrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrinhoExists(Guid id)
        {
            return _context.Carrinho.Any(e => e.CarrinhoId == id);
        }

        // Novo endpoint para calcular o valor total do item
        [HttpPost("calcular-valor-item")]
        public async Task<ActionResult<decimal>> CalcularValorItem(Guid produtoId, int quantidade)
        {
            var produto = await _context.Produto.FindAsync(produtoId);
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            decimal valorTotalItem = produto.Preco * quantidade;
            return Ok(valorTotalItem);
        }

        // Novo endpoint para calcular o valor total do carrinho
        [HttpPost("calcular-valor-carrinho")]
        public async Task<ActionResult<decimal>> CalcularValorCarrinho(Guid carrinhoId)
        {
            var carrinho = await _context.Carrinho
                .Include(c => c.Itens)
                .ThenInclude(i => i.ProdutoId)
                .FirstOrDefaultAsync(c => c.CarrinhoId == carrinhoId);

            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado.");
            }

            decimal valorTotalCarrinho = carrinho.Itens.Sum(item => item.PrecoUnitario * item.Quantidade);
            return Ok(valorTotalCarrinho);
        }

        // Novo endpoint para adicionar um item ao carrinho
        [HttpPost("{carrinhoId}/adicionar-item")]
        public async Task<IActionResult> AdicionarItem(Guid carrinhoId, [FromBody] CarrinhoItem item)
        {
            var carrinho = await _context.Carrinho
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.CarrinhoId == carrinhoId);

            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado.");
            }

            var produto = await _context.Produto.FindAsync(item.ProdutoId);
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            item.PrecoUnitario = produto.Preco;
            carrinho.Itens.Add(item);
            carrinho.Total = carrinho.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);

            await _context.SaveChangesAsync();

            return Ok(carrinho);
        }

        // Novo endpoint para remover um item do carrinho
        [HttpDelete("{carrinhoId}/remover-item/{itemId}")]
        public async Task<IActionResult> RemoverItem(Guid carrinhoId, Guid itemId)
        {
            var carrinho = await _context.Carrinho
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.CarrinhoId == carrinhoId);

            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado.");
            }

            var item = carrinho.Itens.FirstOrDefault(i => i.CarrinhoItemId == itemId);
            if (item == null)
            {
                return NotFound("Item não encontrado.");
            }

            carrinho.Itens.Remove(item);
            carrinho.Total = carrinho.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);

            await _context.SaveChangesAsync();

            return Ok(carrinho);
        }
    }
}
