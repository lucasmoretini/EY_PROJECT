using EY_Project.UseCases.Vagas.Ports.Inputs;

namespace EY_Project.UseCases.Recrutador.Ports.Input;

public class RecruiterInput
{
    public long? Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public List<PositionsInput>? Vagas { get; set; }
}
