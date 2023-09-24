
	using System;
namespace EY_Project.UseCases.Empresa.Ports.Input
{
	public class EmpresaInput
	{
        public long? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Descricao { get; set; }
        public string? Cnpj { get; set; }
        public string? Setor { get; set; }
        public string? Localizacao { get; set; }
        public int? Fundacao { get; set; }
        public string? Site { get; set; }
        public string? ImageUri { get; set; }
    }
}

