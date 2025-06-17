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
    public class CarrinhoItemsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CarrinhoItemsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/CarrinhoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCarrinhoItem()
        {
            var itens = await _context.CarrinhoItem
                .Include(ci => ci.Produto)
                .Select(ci => new
                {
                    ci.CarrinhoItemId,
                    ci.CarrinhoId,
                    ci.ProdutoId,
                    ci.Quantidade,
                    Produto = ci.Produto == null ? null : new
                    {
                        ci.Produto.Nome,
                        ci.Produto.Preco,
                        ci.Produto.Imagem
                    }
                })
                .ToListAsync();

            return Ok(itens);
        }

        // GET: api/CarrinhoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrinhoItem>> GetCarrinhoItem(Guid id)
        {
            var carrinhoItem = await _context.CarrinhoItem.FindAsync(id);

            if (carrinhoItem == null)
            {
                return NotFound();
            }

            return carrinhoItem;
        }

        // PUT: api/CarrinhoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrinhoItem(Guid id, CarrinhoItem carrinhoItem)
        {
            if (id != carrinhoItem.CarrinhoItemId)
            {
                return BadRequest();
            }

            _context.Entry(carrinhoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoItemExists(id))
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

        // POST: api/CarrinhoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarrinhoItem>> PostCarrinhoItem(CarrinhoItem carrinhoItem)
        {
            _context.CarrinhoItem.Add(carrinhoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrinhoItem", new { id = carrinhoItem.CarrinhoItemId }, carrinhoItem);
        }

        // DELETE: api/CarrinhoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrinhoItem(Guid id)
        {
            var carrinhoItem = await _context.CarrinhoItem.FindAsync(id);
            if (carrinhoItem == null)
            {
                return NotFound();
            }

            _context.CarrinhoItem.Remove(carrinhoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarrinhoItemExists(Guid id)
        {
            return _context.CarrinhoItem.Any(e => e.CarrinhoItemId == id);
        }
    }
}
