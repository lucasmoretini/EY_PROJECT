using System.Text.Json.Serialization;

namespace EY_Project.UseCases.Vagas.Ports.Inputs;
public class SelectiveProcessInput
{
    [JsonPropertyName("descricao_etapa_processo_seletivo")]
    public string? Description { get; set; }

    [JsonPropertyName("link_util")]
    public string? UtilLink { get; set; }
}
