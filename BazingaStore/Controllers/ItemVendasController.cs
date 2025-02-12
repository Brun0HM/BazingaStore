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
    public class ItemVendasController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ItemVendasController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemVendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemVenda>>> GetItemVenda()
        {
            return await _context.ItemVenda.ToListAsync();
        }

        // GET: api/ItemVendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemVenda>> GetItemVenda(Guid id)
        {
            var itemVenda = await _context.ItemVenda.FindAsync(id);

            if (itemVenda == null)
            {
                return NotFound();
            }

            return itemVenda;
        }

        // PUT: api/ItemVendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemVenda(Guid id, ItemVenda itemVenda)
        {
            if (id != itemVenda.ItemVendaId)
            {
                return BadRequest();
            }

            _context.Entry(itemVenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemVendaExists(id))
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

        // POST: api/ItemVendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemVenda>> PostItemVenda(ItemVenda itemVenda)
        {
            _context.ItemVenda.Add(itemVenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemVenda", new { id = itemVenda.ItemVendaId }, itemVenda);
        }

        // DELETE: api/ItemVendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemVenda(Guid id)
        {
            var itemVenda = await _context.ItemVenda.FindAsync(id);
            if (itemVenda == null)
            {
                return NotFound();
            }

            _context.ItemVenda.Remove(itemVenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemVendaExists(Guid id)
        {
            return _context.ItemVenda.Any(e => e.ItemVendaId == id);
        }
        [HttpGet("{id}/subtotal")]
        public async Task<ActionResult<decimal>> GetSubtotal(Guid id)
        {
            var itemVenda = await _context.ItemVenda.FindAsync(id);

            if (itemVenda == null)
            {
                return NotFound();
            }

            var subtotal = itemVenda.Quantidade * itemVenda.PrecoUnitario;

            return Ok(subtotal);
        }
    }
}
