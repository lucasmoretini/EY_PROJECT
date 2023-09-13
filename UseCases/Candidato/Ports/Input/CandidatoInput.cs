namespace EY_Project.UseCases.Candidato.Ports.Input;

public class CandidatoInput
{
    public long? Id { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public string? NomeSocial { get; set; }
    public string? DataNascimento { get; set; }
    public string? Sexo { get; set; }
    public string? OrientacaoSexual { get; set; }
    public string? Etnia { get; set; }
    public string? ClasseSocial { get; set; }
    public string? Deficiencia { get; set; }
    public string? Profissao { get; set; }
    public string? ModeloTrabalho { get; set; }
    public string? ModeloContratacao { get; set; }
    public decimal? PretensaoSalarial { get; set; }
    public IEnumerable<long?> VagasSelecionadas { get; set; } = new List<long>();
}
