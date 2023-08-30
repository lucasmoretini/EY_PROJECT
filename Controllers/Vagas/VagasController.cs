using EY_Project.Infrastructure.Repositories;
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
        return Ok(positions);
    }

    [HttpPost("/cadastra-vaga")]
    public async Task<IActionResult> PostPostions([FromBody] PositionsInput input)
    {
        await _mongoHelper.CreateDocument<PositionsInput>(_cluster, _collection, input);
        return Ok("vaga criada com sucesso");
    }

    [HttpPut("/atualiza-vaga")]
    public async Task<IActionResult> PutPostions([FromBody] PositionsInput input)
    {
        var filter = Builders<PositionsInput>.Filter.Eq("id_vaga", input.Id);
        var update = Builders<PositionsInput>.Update.Set(x => x.Description, input.Description);

        await _mongoHelper.UpdateDocument<PositionsInput>(_cluster, _collection, filter, update);
        return Ok("vaga atualizada com sucesso");
    }

    [HttpPut("/deleta-vaga")]
    public async Task<IActionResult> DeletePostions([FromQuery] long id)
    {
        var filter = Builders<PositionsInput>.Filter.Eq("id_vaga", id);
        await _mongoHelper.DeleteDocument<PositionsInput>(_cluster, _collection, filter);
        return Ok("vaga deletada com sucesso");
    }
}
