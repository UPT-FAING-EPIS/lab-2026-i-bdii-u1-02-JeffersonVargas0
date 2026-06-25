using System.Text.Json.Serialization;

namespace ClienteAPI.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NomCliente { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ClientesDocumento> ClientesDocumentos { get; set; } = new List<ClientesDocumento>();
}
