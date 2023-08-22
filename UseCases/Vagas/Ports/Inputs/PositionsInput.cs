using System.Text.Json.Serialization;

namespace EY_Project.UseCases.Vagas.Ports.Inputs;
public class PositionsInput
{
    [JsonPropertyName("id_vaga")]
    public long? Id { get; set; }

    [JsonPropertyName("titulo_vaga")]
    public string? Title { get; set; }

    [JsonPropertyName("descricao")]
    public string? Description { get; set; }

    [JsonPropertyName("modelo_trabalho")]
    public string? Employer { get; set; }

    [JsonPropertyName("modelo_contratacao")]
    public string? JobType { get; set; }

    [JsonPropertyName("faixa_salarial")]
    public string? Compensation { get; set; }

    [JsonPropertyName("situacao_vulnerabilidade")]
    public string? Vulnerability { get; set; }

    [JsonPropertyName("etapas_processo_seletivo")]
    public IEnumerable<SelectiveProcessInput>? SelectiveProcess { get; set; }
}
