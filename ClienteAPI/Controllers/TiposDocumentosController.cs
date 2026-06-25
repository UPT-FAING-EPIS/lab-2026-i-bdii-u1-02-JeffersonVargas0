using ClienteAPI.Data;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TiposDocumentosController : ControllerBase
{
    private readonly BdClientesContext _context;

    public TiposDocumentosController(BdClientesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TiposDocumento>>> GetTiposDocumentos()
    {
        return await _context.TiposDocumentos.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TiposDocumento>> GetTiposDocumento(byte id)
    {
        var tipoDocumento = await _context.TiposDocumentos.AsNoTracking().FirstOrDefaultAsync(t => t.IdTipoDocumento == id);
        return tipoDocumento is null ? NotFound() : tipoDocumento;
    }
}
