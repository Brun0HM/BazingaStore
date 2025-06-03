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
            // Verificar se os itens do carrinho não são nulos  
            if (carrinho.Itens == null || !carrinho.Itens.Any())
            {
                return BadRequest("O carrinho deve conter pelo menos um item.");
            }

            // Carregar os produtos e atualizar os preços e totais dos itens  
            foreach (var item in carrinho.Itens)
            {
                var produto = await _context.Produto.FindAsync(item.ProdutoId);
                if (produto == null)
                    return BadRequest($"Produto {item.ProdutoId} não encontrado.");

                // Atualizar o preço do item com o preço do produto  
                item.Preco = produto;
            }

            // Calcular o total do carrinho  
            carrinho.Total = carrinho.Itens.Sum(i => i.Preco!.Preco * i.Quantidade);

            // Se o carrinho já existe, atualiza; senão, adiciona  
            var carrinhoExistente = await _context.Carrinho
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.CarrinhoId == carrinho.CarrinhoId);

            if (carrinhoExistente != null)
            {
                // Atualiza os itens e o total  
                carrinhoExistente.Itens = carrinho.Itens;
                carrinhoExistente.Total = carrinho.Total;
                _context.Entry(carrinhoExistente).State = EntityState.Modified;
            }
            else
            {
                _context.Carrinho.Add(carrinho);
            }

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

        // Novo endpoint para atualizar a quantidade de produtos no carrinho




        public class AtualizarQuantidadeRequest
        {
            public Guid CarrinhoItemId { get; set; }
            public int Quantidade { get; set; }
        }
    }
}
