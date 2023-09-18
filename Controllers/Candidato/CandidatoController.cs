using EY_Project.Infrastructure.Repositories;
using EY_Project.UseCases.Candidato.Ports.Input;
using EY_Project.UseCases.Vagas.Ports.Inputs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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
        return Ok("Candidato criado com sucesso");
    }

    [HttpPost("/atrela-candidato-vaga")]
    public async Task<IActionResult> PostCandidatos([FromQuery] long idCandidato, [FromQuery] long idVaga, [FromQuery] long idEtapa)
    {
        var candidatos = await _mongoHelper.GetAllDocuments<CandidatoInput>(_cluster, _collection);
        var candidatoSelecionado = candidatos.Where(x => x.Id == idCandidato).FirstOrDefault();

        var vagas = await _mongoHelper.GetAllDocuments<PositionsInput>(_cluster, "vagas");
        var vaga = vagas.Where(x => x.Id == idVaga).FirstOrDefault();

        candidatoSelecionado?.VagasSelecionadas.Add(new VagaSelecionada(idEtapa, vaga));
        
        var update = Builders<CandidatoInput>.Update.Set(x => x.VagasSelecionadas, candidatoSelecionado?.VagasSelecionadas);
        var filter = Builders<CandidatoInput>.Filter.Eq("Id", candidatoSelecionado.Id);

        vaga?.Candidatos?.Add(candidatoSelecionado.Id);

        var filterVaga = Builders<PositionsInput>.Filter.Eq("Id", vaga.Id);
        var updateVaga = Builders<PositionsInput>.Update.Set(x => x.Candidatos, vaga?.Candidatos);

        await _mongoHelper.UpdateDocument<CandidatoInput>(_cluster, _collection, filter, update);
        await _mongoHelper.UpdateDocument<PositionsInput>(_cluster, "vagas", filterVaga, updateVaga);

        return Ok("Candidato atrelado a vaga");
    }
}
