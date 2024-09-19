using APICatalogo.DTO;
using APICatalogo.DTO.Mappings;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace APICatalogo.Controllers;

[Route("/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork uof)
    {
        _logger = logger;
        _uof = uof;
    }

    [HttpGet]

    public ActionResult<IEnumerable<CategoriaDTO>> Get()

    {
        var categorias = _uof.CategoriaRepository.GetAll();

        if (categorias is null)
            return NotFound("Não existem categoria...");

        var categoriasDto = categorias.ToList();

        return Ok(categoriasDto);
    }

    // A função AsNoTracking é usada em consulta somente leitura, não fica rastreada e melhora o desenpenho, o dado só não pode ser alterado
    // Nunca retorne todos os registros em uma consulta como no exemplo, use o "Tak(10)" antes do "ToList"
    //A ação do resultado é usado para suporta o retorno do produto ou o erro

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c=> c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id = {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }

        var categoriaDto = categoria.ToCategoriaDTO();

        return Ok(categoria);
    }

    //A ação do resultado só está indicanto que vai retornar somente as mensagens de status http
    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning($"Dados inválidos....");
            return BadRequest("Dados inválidos");
        }

        var categoria = categoriaDto.ToCategoria();
        
        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();

        var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();

        return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);

    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = categoriaDto.ToCategoria();

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDTO();

        return Ok(categoriaAtualizadaDto);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c=> c.CategoriaId == id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id } não encontrada...");
        }
        var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        var catgoiaExcluidaDto = categoriaExcluida.ToCategoriaDTO();

        return Ok(catgoiaExcluidaDto);
    }
}