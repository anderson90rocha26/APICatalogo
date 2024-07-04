
using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APICatalogo.Controllers;


//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context; 
    private readonly IConfiguration _configuration;


    public CategoriasController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet("LerArquivoConfiguracao")]

    public string GetValores() 
    {
        var valor1 = _configuration ["chave1"];
        var valor2 = _configuration ["chave2"];

        var secao1 = _configuration ["secao1:chave2"];

          return $"Chave1 = {valor1} \nChave2 = {valor2} \nSeção1 => Chave2 = {secao1}";
    }


    // Nunca retorne objetos rellcionados sem aplicar um filtro como está no codigo comentado, o correto é antes do ToList usar exemplo .Where(c=> c.CategriaId<=5), pq dependendo da entidade sobrecarega a aplicação

    [HttpGet("produtos")]

    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
    {
        //return _context.Categorias.Include(p=> p.Produtos).ToList();
        return _context.Categorias.Include(p=> p.Produtos).Where(c=> c.CategoriaId <= 5).ToList();
    }

    // A função AsNoTracking é usada em consulta somente leitura, não fica rastreada e melhora o desenpenho, o dado só não pode ser alterado
    // Nunca retorne todos os registros em uma consulta como no exemplo, use o "Tak(10)" antes do "ToList"
    


    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
    {
        try
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
  
    //A ação do resultado é usado para suporta o retorno do produto ou o erro
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {

         throw new Exception("Exceção ao rtornar o produto pelo Id");

        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }
        return Ok(categoria);
    }

    //A ação do resultado só está indicanto que vai retornar somente as mensagens de status http
    [HttpPost]
    public ActionResult Post(CategoriaDTO categoria)
    {
        if (categoria is null)
        
            return BadRequest();
        
        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);

    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, CategoriaDTO categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada");
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }
}