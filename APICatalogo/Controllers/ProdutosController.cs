using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")] // /produtos
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // api/produtos/primeiros
        [HttpGet("primeiro")]
        public ActionResult<Produto> GetPimeiro()
        {
            var produto = _context.Produtos.FirstOrDefault();
            if (produto is null)
            {
                return NotFound();
            }
            return produto;
        }

        // api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get2() 
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        // A ação do resultado é usado para suporta o retorno do produto ou o erro

        // api/produtos/1
        [HttpGet("{id:int}", Name ="ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produtos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProdutoId ==  id);

            if(produto == null)
            {
                return NotFound();
            }
            return produto;
        }

        //A ação do resultado só está indicanto que vai retornar somente as mensagens de status http

        // /produtos
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if(produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new {id = produto.ProdutoId}, produto);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto) 
        {
            if(id != produto.ProdutoId) 
            {
                return BadRequest();
            }

            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null) 
            {
                return NotFound("Produto não encontrado");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
