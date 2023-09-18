using EY_Project.Infrastructure.Repositories;
using EY_Project.UseCases.Candidato.Ports.Input;
using EY_Project.UseCases.Empresa.Ports.Input;
using EY_Project.UseCases.Vagas.Ports.Inputs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EY_Project.Controllers.Empresa;

public class EmpresaController : ControllerBase
{
    private const string _cluster = "Cluster0";
    private const string _collection = "empresas";

    private readonly IMongoRepository _mongoHelper;

    public EmpresaController(IMongoRepository mongo) =>
        _mongoHelper = mongo;


    [HttpGet("/lista-empresa")]
    public async Task<IActionResult> GetAllEmpresas()
    {
        var empresas = await _mongoHelper.GetAllDocuments<EmpresaInput>(_cluster, _collection);
        return Ok(empresas);
    }

    [HttpPost("/cadastra-empresa")]
    public async Task<IActionResult> PostEmpresa([FromBody] EmpresaInput input)
    {
        await _mongoHelper.CreateDocument<EmpresaInput>(_cluster, _collection, input);
        return Ok("Empresa criado com sucesso");
    }
}
