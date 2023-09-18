using System;
using EY_Project.UseCases.Vagas.Ports.Inputs;

namespace EY_Project.UseCases.Candidato.Ports.Input
{
	public class VagaSelecionada
	{
        public VagaSelecionada(long? etapaId, PositionsInput vaga)
        {
            EtapaId = etapaId;
            Vaga = vaga;
        }

        public long? EtapaId { get; set; }
        public PositionsInput? Vaga { get; set; } = new PositionsInput();
    }
}

