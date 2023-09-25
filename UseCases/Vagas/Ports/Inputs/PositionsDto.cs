using System;
using EY_Project.UseCases.Empresa.Ports.Input;

namespace EY_Project.UseCases.Vagas.Ports.Inputs
{
	public class PositionsDto
	{
        public PositionsInput? Vaga { get; set; }
        public EmpresaInput? Empresa { get; set; }

        public PositionsDto(PositionsInput? vaga, EmpresaInput? empresa)
        {
            Vaga = vaga;
            Empresa = empresa;
        }
    }
}

