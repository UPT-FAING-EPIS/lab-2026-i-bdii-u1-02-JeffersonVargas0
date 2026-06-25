using System.Text.Json.Serialization;

namespace ClienteAPI.Models;

public partial class ClientesDocumento
{
    public int IdCliente { get; set; }

    public byte IdTipoDocumento { get; set; }

    public string NumDocumento { get; set; } = null!;

    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }

    [JsonIgnore]
    public virtual TiposDocumento? IdTipoDocumentoNavigation { get; set; }
}
