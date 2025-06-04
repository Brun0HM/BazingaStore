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
    public class AvaliacaosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public AvaliacaosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Avaliacaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> GetAvaliacao()
        {
            return await _context.Avaliacao.ToListAsync();
        }

        // GET: api/Avaliacaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(Guid id)
        {
            var avaliacao = await _context.Avaliacao.FindAsync(id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            return avaliacao;
        }

        // PUT: api/Avaliacaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvaliacao(Guid id, Avaliacao avaliacao)
        {
            if (id != avaliacao.AvaliacaoId)
            {
                return BadRequest();
            }

            _context.Entry(avaliacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvaliacaoExists(id))
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

        // POST: api/Avaliacaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Controllers/AvaliacaosController.cs
        [HttpPost]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(Avaliacao avaliacao)
        {
            // Verifica se o produto existe
            var produto = await _context.Produto.FindAsync(avaliacao.ProdutoId);
            if (produto == null)
                return NotFound("Produto não encontrado.");

            _context.Avaliacao.Add(avaliacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvaliacao", new { id = avaliacao.AvaliacaoId }, avaliacao);
        }




        // DELETE: api/Avaliacaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvaliacao(Guid id)
        {
            var avaliacao = await _context.Avaliacao.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            _context.Avaliacao.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AvaliacaoExists(Guid id)
        {
            return _context.Avaliacao.Any(e => e.AvaliacaoId == id);
        }
    }
}
