using EY_Project.Infrastructure.Repositories;
using EY_Project.UseCases.Recrutador.Ports.Input;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EY_Project.Controllers.Recrutador;

public class RecrutadorController : ControllerBase
{
    private const string _cluster = "Cluster0";
    private const string _collection = "recrutador";

    private readonly IMongoRepository _mongoHelper;

    public RecrutadorController(IMongoRepository mongo) =>
        _mongoHelper = mongo;

    [HttpGet("/lista-recrutador")]
    public async Task<IActionResult> GetAllRecruiter()
    {
        var positions = await _mongoHelper.GetAllDocuments<RecruiterInput>(_cluster, _collection);
        return Ok(positions); 
    }

    [HttpPost("/cadastra-recrutador")]
    public async Task<IActionResult> PostRecruiter([FromBody] RecruiterInput input)
    {
        await _mongoHelper.CreateDocument<RecruiterInput>(_cluster, _collection, input);
        return Ok("recrutador criado com sucesso");
    }

    [HttpDelete("/deleta-recrutador")]
    public async Task<IActionResult> DeletePostions([FromQuery] long id)
    {
        var filter = Builders<RecruiterInput>.Filter.Eq("Id", id);
        await _mongoHelper.DeleteDocument<RecruiterInput>(_cluster, _collection, filter);
        return Ok("recrutador deletado com sucesso");
    }
}
