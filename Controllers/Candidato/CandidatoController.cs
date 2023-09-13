using EY_Project.Infrastructure.Repositories;
using EY_Project.UseCases.Candidato.Ports.Input;
using EY_Project.UseCases.Vagas.Ports.Inputs;
using Microsoft.AspNetCore.Mvc;

namespace EY_Project.Controllers.Candidato;

public class CandidatoController : ControllerBase
{
    private const string _cluster = "Cluster0";
    private const string _collection = "candidatos";

    private readonly IMongoRepository _mongoHelper;

    public CandidatoController(IMongoRepository mongo) =>
        _mongoHelper = mongo;


    [HttpGet("/lista-candidato")]
    public async Task<IActionResult> GetAllCandidatos()
    {
        var candidatos = await _mongoHelper.GetAllDocuments<CandidatoInput>(_cluster, _collection);
        return Ok(candidatos);
    }

    [HttpPost("/cadastra-candidato")]
    public async Task<IActionResult> PostCandidatos([FromBody] CandidatoInput input)
    {
        await _mongoHelper.CreateDocument<CandidatoInput>(_cluster, _collection, input);
        return Ok("recrutador criado com sucesso");
    }

    [HttpPost("/atrela-candidato-vaga")]
    public async Task<IActionResult> PostCandidatos([FromQuery] long idCandidato, [FromQuery] long idVaga)
    {
        var candidatos = await _mongoHelper.GetAllDocuments<CandidatoInput>(_cluster, _collection);
        var candidatoSelecionado = candidatos.Where(x => x.Id == idCandidato).FirstOrDefault();

        var vagas = await _mongoHelper.GetAllDocuments<PositionsInput>(_cluster, "vagas");
        var vaga = candidatos.Where(x => x.Id == idVaga).FirstOrDefault();

        return Ok("recrutador criado com sucesso");
    }
}
