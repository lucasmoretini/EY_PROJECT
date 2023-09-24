using System;
using EY_Project.Infrastructure.Repositories;
using EY_Project.UseCases.Candidato.Ports.Input;
using EY_Project.UseCases.Empresa.Ports.Input;
using EY_Project.UseCases.Login.Ports.Input;
using EY_Project.UseCases.Recrutador.Ports.Input;
using EY_Project.UseCases.Vagas.Ports.Inputs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EY_Project.Controllers.Login
{
    public class LoginController : ControllerBase
    {
        private const string _cluster = "Cluster0";

        private readonly IMongoRepository _mongoHelper;

        public LoginController(IMongoRepository mongo) =>
            _mongoHelper = mongo;

        [HttpGet("/login")]
        public async Task<IActionResult> Login([FromQuery] String email, [FromQuery] String senha)
        {
            var filterEmpresa = Builders<EmpresaInput>.Filter.Eq("Email", email);
            var empresas = await _mongoHelper.GetFilteredDocuments<EmpresaInput>(_cluster, "empresas", filterEmpresa);
            var empresa = empresas.FirstOrDefault();

            if (empresa != null)
            {
                if (empresa.Senha == senha)
                    return Ok(new LoginInput(empresa.Id, "empresa"));

                return BadRequest("Senha incorreta");
            }

            var filterRecrutador = Builders<RecruiterInput>.Filter.Eq("Email", email);
            var recrutadores = await _mongoHelper.GetFilteredDocuments<RecruiterInput>(_cluster, "recrutador", filterRecrutador);
            var recrutador = recrutadores.FirstOrDefault();

            if (recrutador != null)
            {
                if (recrutador.Senha == senha)
                    return Ok(new LoginInput(recrutador.Id, "recrutador"));

                return BadRequest("Senha incorreta");
            }

            var filterCandidato = Builders<CandidatoInput>.Filter.Eq("Email", email);
            var candidatos = await _mongoHelper.GetFilteredDocuments<CandidatoInput>(_cluster, "candidatos", filterCandidato);
            var candidato = candidatos.FirstOrDefault();

            if (candidato != null)
            {
                if (candidato.Senha == senha)
                    return Ok(new LoginInput(candidato.Id, "candidato"));

                return BadRequest("Senha incorreta");
            }

            return NotFound();
        }
    }
}

