﻿using AspNetCore.CacheOutput;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OPS.Core.DAO;
using OPS.Core.DTO;
using System;
using System.Threading.Tasks;

namespace OPS.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    [CacheOutput(ServerTimeSpan = 43200 /* 12h */)]
    public class DeputadoController : Controller
    {
        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        DeputadoDao dao;

        public DeputadoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;

            dao = new DeputadoDao();
        }

        [HttpGet("{id:int}")]
        public async Task<dynamic> Consultar(int id)
        {
            return await dao.Consultar(id);
        }

        [HttpPost("Lista")]
        [CacheOutput(ClientTimeSpan = 43200 /* 12h */, ServerTimeSpan = 43200 /* 12h */)]
        public async Task<dynamic> Lista(FiltroParlamentarDTO filtro)
        {
            return await dao.Lista(filtro);
        }

        [HttpGet("Pesquisa")]
        [CacheOutput(ClientTimeSpan = 43200 /* 12h */, ServerTimeSpan = 43200 /* 12h */)]
        public async Task<dynamic> Pesquisa()
        {
            return await dao.Pesquisa();
        }

        [HttpGet("Lancamentos")]
        public async Task<dynamic> Lancamentos([FromQuery]FiltroParlamentarDTO filtro)
        {
            return await dao.Lancamentos(filtro);
        }

        [HttpGet("TipoDespesa")]
        [CacheOutput(ClientTimeSpan = 43200 /* 12h */, ServerTimeSpan = 43200 /* 12h */)]
        public async Task<dynamic> TipoDespesa()
        {
            return await dao.TipoDespesa();
        }

        [HttpGet("Secretarios")]
        public async Task<dynamic> Secretarios([FromQuery]FiltroSecretarioDTO filtro)
        {
            return await dao.Secretarios(filtro);
        }

        [HttpGet("{id:int}/Secretarios")]
        public async Task<dynamic> SecretariosPorDeputado(int id, [FromQuery]FiltroSecretarioDTO filtro)
        {
            return await dao.SecretariosPorDeputado(id, filtro);
        }

        [HttpGet("{id:int}/GastosMensaisPorAno")]
        public async Task<dynamic> GastosMensaisPorAno(int id)
        {
            return await dao.GastosMensaisPorAno(id);
        }

        [HttpGet("Documento/{id:int}")]
        public async Task<dynamic> Documento(int id)
        {
            var result = await dao.Documento(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("DocumentosDoMesmoDia/{id:int}")]
        public async Task<dynamic> DocumentosDoMesmoDia(int id)
        {
            var result = await dao.DocumentosDoMesmoDia(id);

            return Ok(result);
        }

        [HttpGet("DocumentosDaSubcotaMes/{id:int}")]
        public async Task<dynamic> DocumentosDaSubcotaMes(int id)
        {
            var result = await dao.DocumentosDaSubcotaMes(id);

            return Ok(result);
        }

        [HttpGet("{id:int}/MaioresNotas")]
        public async Task<dynamic> MaioresNotas(int id)
        {
            return await dao.MaioresNotas(id);
        }

        [HttpGet("{id:int}/MaioresFornecedores")]
        public async Task<dynamic> MaioresFornecedores(int id)
        {
            return await dao.MaioresFornecedores(id);
        }

        [HttpGet("{id:int}/ResumoPresenca")]
        public async Task<dynamic> ResumoPresenca(int id)
        {
            return await dao.ResumoPresenca(id);
        }

        [HttpGet("CamaraResumoMensal")]
        public async Task<dynamic> CamaraResumoMensal()
        {
            return await dao.CamaraResumoMensal();
        }

        [HttpGet("CamaraResumoAnual")]
        public async Task<dynamic> CamaraResumoAnual()
        {
            return await dao.CamaraResumoAnual();
        }

        [HttpGet("Frequencia/{id:int}")]
        public async Task<dynamic> Frequencia(int id)
        {
            return await dao.Frequencia(id);
        }

        [HttpGet("Frequencia")]
        public async Task<dynamic> Frequencia([FromQuery]FiltroFrequenciaCamaraDTO filtro)
        {
            return await dao.Frequencia(filtro);
        }

        [HttpGet("Imagem/{id}")]
        public VirtualFileResult Imagem(string id)
        {
            var file = @"images/Parlamentares/DEPFEDERAL/" + id + ".jpg";
            var filePath = System.IO.Path.Combine(Environment.WebRootPath, file);

            if (System.IO.File.Exists(filePath))
            {
                return File(file, "image/jpeg");
            }
            else
            {
                return File(@"images/sem_foto.jpg", "image/jpeg");
            }
        }
    }
}
