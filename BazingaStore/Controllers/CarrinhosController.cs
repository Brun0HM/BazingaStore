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
    }
}
