using EY_Project.Infrastructure.Repositories;
using EY_Project.UseCases.Candidato.Ports.Input;
using EY_Project.UseCases.Empresa.Ports.Input;
using EY_Project.UseCases.Recrutador.Ports.Input;
using EY_Project.UseCases.Vagas.Ports.Inputs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EY_Project.Controllers.Vagas;

[ApiController]
[Route("[controller]")]
public class VagasController : ControllerBase
{
    private const string _cluster = "Cluster0";
    private const string _collection = "vagas";

    private readonly IMongoRepository _mongoHelper;

    public VagasController(IMongoRepository mongo) =>
        _mongoHelper = mongo;

    [HttpGet("/lista-vagas")]
    public async Task<IActionResult> GetAllPositions()
    {
        var positions = await _mongoHelper.GetAllDocuments<PositionsInput>(_cluster, _collection);
        var response = new List<PositionsDto>();
        foreach(var x in positions)
        {
            var filter = Builders<RecruiterInput>.Filter.Eq("Id", x.IdRecruiter);
            var recruiter = await _mongoHelper.GetFilteredDocuments(_cluster, "recrutador", filter);

            var filterEmpresa = Builders<EmpresaInput>.Filter.Eq("Id", recruiter!.FirstOrDefault()!.EmpresaId);
            var empresa = await _mongoHelper.GetFilteredDocuments(_cluster, "empresas", filterEmpresa);

            response.Add(new PositionsDto(x, empresa.FirstOrDefault()));
        };
        return Ok(response);
    }

    [HttpPost("/cadastra-vaga")]
    public async Task<IActionResult> PostPostions([FromBody] PositionsInput input)
    {
        await _mongoHelper.CreateDocument<PositionsInput>(_cluster, _collection, input);
        var filter = Builders<RecruiterInput>.Filter.Eq("Id", input.IdRecruiter);
        var recruiter = await _mongoHelper.GetFilteredDocuments<RecruiterInput>(_cluster, "recrutador", filter);
        var vagas = recruiter?.FirstOrDefault()?.Vagas ?? new List<PositionsInput>();
        vagas.Add(input);

        var update = Builders<RecruiterInput>.Update.Set(x => x.Vagas, vagas);

        await _mongoHelper.UpdateDocument<RecruiterInput>(_cluster, "recrutador", filter, update);
        return Ok("vaga criada com sucesso");
    }

    [HttpPut("/atualiza-vaga")]
    public async Task<IActionResult> PutPostions([FromBody] PositionsInput input)
    {
        var filter = Builders<PositionsInput>.Filter.Eq("Id", input.Id);
        var update = Builders<PositionsInput>.Update
                                             .Set(x => x.Description, input.Description)
                                             .Set(x => x.Id, input.Id)
                                             .Set(x => x.IdRecruiter, input.IdRecruiter)
                                             .Set(x => x.Title, input.Title)
                                             .Set(x => x.Employer, input.Employer)
                                             .Set(x => x.JobType, input.JobType)
                                             .Set(x => x.Compensation, input.Vulnerability)
                                             .Set(x => x.Candidatos, input.Candidatos)
                                             .Set(x => x.SelectiveProcess, input.SelectiveProcess);

        await _mongoHelper.UpdateDocument<PositionsInput>(_cluster, _collection, filter, update);

        var vaga = await _mongoHelper.GetFilteredDocuments<PositionsInput>(_cluster, _collection, filter);

        var candidatos = await _mongoHelper.GetAllDocuments<CandidatoInput>(_cluster, "candidatos");
        foreach (var candidato in candidatos)
        {
            var vagaSelecionada = candidato?.VagasSelecionadas?.Find(x => x?.Vaga?.Id == input.Id)?.Vaga;
            if(vagaSelecionada != null)
            {
                candidato!.VagasSelecionadas!.Find(x => x?.Vaga?.Id == input.Id)!.Vaga = vaga!.FirstOrDefault();
                var updateCandidato = Builders<CandidatoInput>.Update.Set(x => x.VagasSelecionadas, candidato.VagasSelecionadas);
                var filterCandidato = Builders<CandidatoInput>.Filter.Eq("Id", candidato.Id);
                await _mongoHelper.UpdateDocument<CandidatoInput>(_cluster, "candidatos", filterCandidato, updateCandidato);
            }

        }

        return Ok("vaga atualizada com sucesso");
    }

    [HttpDelete("/deleta-vaga")]
    public async Task<IActionResult> DeletePostions([FromQuery] long id)
    {
        var filter = Builders<PositionsInput>.Filter.Eq("Id", id);
        await _mongoHelper.DeleteDocument<PositionsInput>(_cluster, _collection, filter);

        var candidatos = await _mongoHelper.GetAllDocuments<CandidatoInput>(_cluster, "candidatos");
        foreach(var candidato in candidatos)
        {
            var vagaSelecionada = candidato.VagasSelecionadas?.Find(x => x?.Vaga?.Id == id);
            if(vagaSelecionada != null)
            {
                candidato?.VagasSelecionadas?.Remove(vagaSelecionada);
                var update = Builders<CandidatoInput>.Update.Set(x => x.VagasSelecionadas, candidato!.VagasSelecionadas);
                var filterCandidato = Builders<CandidatoInput>.Filter.Eq("Id", candidato.Id);

                await _mongoHelper.UpdateDocument<CandidatoInput>(_cluster, "candidatos", filterCandidato, update);
            }
        }

        return Ok("vaga deletada com sucesso");
    }

    [HttpGet("/relatorio-candidatos")]
    public async Task<IActionResult> getRelatorioCandidatos([FromQuery] long idVaga)
    {
        var filter = Builders<PositionsInput>.Filter.Eq("Id", idVaga);
        var vaga = await _mongoHelper.GetFilteredDocuments(_cluster, _collection, filter);

        var candidatos = vaga?.FirstOrDefault()?.Candidatos;

        return Ok("vaga deletada com sucesso");
    }
}
