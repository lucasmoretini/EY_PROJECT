using System.Text.Json.Serialization;

namespace EY_Project.UseCases.Vagas.Ports.Inputs;
public class SelectiveProcessInput
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("descricao_etapa_processo_seletivo")]
    public string? Description { get; set; }

    [JsonPropertyName("link_util")]
    public string? UtilLink { get; set; }
}
