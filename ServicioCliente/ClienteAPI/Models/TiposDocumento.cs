using System.Text.Json.Serialization;

namespace ClienteAPI.Models;

public partial class TiposDocumento
{
    public byte IdTipoDocumento { get; set; }

    public string DesTipoDocumento { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ClientesDocumento> ClientesDocumentos { get; set; } = new List<ClientesDocumento>();
}
