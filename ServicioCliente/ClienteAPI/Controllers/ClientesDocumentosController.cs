using ClienteAPI.Data;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesDocumentosController : ControllerBase
{
    private readonly BdClientesContext _context;

    public ClientesDocumentosController(BdClientesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientesDocumento>>> GetClientesDocumentos()
    {
        return await _context.ClientesDocumentos.AsNoTracking().ToListAsync();
    }

    [HttpGet("{idCliente:int}/{idTipoDocumento:int}")]
    public async Task<ActionResult<ClientesDocumento>> GetClientesDocumento(int idCliente, byte idTipoDocumento)
    {
        var documento = await _context.ClientesDocumentos
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.IdCliente == idCliente && d.IdTipoDocumento == idTipoDocumento);

        return documento is null ? NotFound() : documento;
    }

    [HttpPost]
    public async Task<ActionResult<ClientesDocumento>> PostClientesDocumento(ClientesDocumento clientesDocumento)
    {
        _context.ClientesDocumentos.Add(clientesDocumento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetClientesDocumento),
            new { idCliente = clientesDocumento.IdCliente, idTipoDocumento = clientesDocumento.IdTipoDocumento },
            clientesDocumento);
    }

    [HttpPut("{idCliente:int}/{idTipoDocumento:int}")]
    public async Task<IActionResult> PutClientesDocumento(int idCliente, byte idTipoDocumento, ClientesDocumento clientesDocumento)
    {
        if (idCliente != clientesDocumento.IdCliente || idTipoDocumento != clientesDocumento.IdTipoDocumento)
        {
            return BadRequest();
        }

        _context.Entry(clientesDocumento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ClientesDocumentoExists(idCliente, idTipoDocumento))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{idCliente:int}/{idTipoDocumento:int}")]
    public async Task<IActionResult> DeleteClientesDocumento(int idCliente, byte idTipoDocumento)
    {
        var documento = await _context.ClientesDocumentos.FindAsync(idCliente, idTipoDocumento);
        if (documento is null)
        {
            return NotFound();
        }

        _context.ClientesDocumentos.Remove(documento);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private Task<bool> ClientesDocumentoExists(int idCliente, byte idTipoDocumento)
    {
        return _context.ClientesDocumentos.AnyAsync(e => e.IdCliente == idCliente && e.IdTipoDocumento == idTipoDocumento);
    }
}
