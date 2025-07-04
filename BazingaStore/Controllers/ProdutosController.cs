﻿using System;
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
    public class ProdutosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ProdutosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetProduto()
        {
            var produtos = await _context.Produto
                .Include(p => p.Categoria)
                .Select(p => new
                {
                    p.ProdutoId,
                    p.Nome,
                    p.Descricao,
                    p.Preco,
                    p.Imagem,
                    p.Estoque,
                    p.CategoriaId,
                    CategoriaNome = p.Categoria != null ? p.Categoria.CategoriaNome : null
                })
                .ToListAsync();

            return Ok(produtos);
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProduto(Guid id)
        {
            var produto = await _context.Produto
                .Include(p => p.Categoria)
                .Where(p => p.ProdutoId == id)
                .Select(p => new
                {
                    p.ProdutoId,
                    p.Nome,
                    p.Descricao,
                    p.Preco,
                    p.Imagem,
                    p.Estoque,
                    p.CategoriaId,
                    CategoriaNome = p.Categoria != null ? p.Categoria.CategoriaNome : null
                })
                .FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(Guid id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            var categoria = await _context.Categoria.FindAsync(produto.CategoriaId);
            produto.Categoria = categoria;
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produto.Any(e => e.ProdutoId == id);
        }
        // Adicione este endpoint ao seu ProdutosController

        [HttpPost("{id}/imagem")]
        public async Task<IActionResult> UploadImagem(Guid id, IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
                return BadRequest("Nenhuma imagem enviada.");

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
                return NotFound();

            // Exemplo: salvar a imagem em wwwroot/imagens
            var pastaImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens");
            if (!Directory.Exists(pastaImagens))
                Directory.CreateDirectory(pastaImagens);

            var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
            var caminhoArquivo = Path.Combine(pastaImagens, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await imagem.CopyToAsync(stream);
            }

            // Atualiza o campo Imagem do produto com o caminho relativo
            produto.Imagem = $"/imagens/{nomeArquivo}";
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { imagemUrl = produto.Imagem });
        }
    }
}
