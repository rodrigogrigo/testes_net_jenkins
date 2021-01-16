using System;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels.Imunizacao;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class CartaoVacinaController : ControllerBase
    {
        public ICartaoVacinaRepository _repository;
        public IAprazamentoRepository _aprazamentorepository;
        private IConfiguration _config;
        public CartaoVacinaController(ICartaoVacinaRepository _cartaovacina, IAprazamentoRepository aprazamentorepository,
                                                                             IConfiguration configuration)
        {
            _repository = _cartaovacina;
            _aprazamentorepository = aprazamentorepository;
            _config = configuration;
        }

        [HttpGet("GetCartaoVacinaById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<CartaoVacina> GetCartaoVacinaById([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                CartaoVacina model = _repository.GetCartaoVacinaById(ibge, id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader]string ibge, [FromBody] ParametersCartaoVacinaViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.CartaoVacina.id = _repository.GetNewId(ibge);
                model.CartaoVacina.uuid = (Guid.NewGuid()).ToString().ToUpper();
                _repository.Insert(ibge, model.CartaoVacina);

                if (model.IdAprazamento != null) //update pni_aprazamentos
                {
                    _aprazamentorepository.UpdateVacinados(ibge, model.CartaoVacina.id, (int)model.IdAprazamento);
                }
                else //cria novo pni_aprazamentos
                {
                    var id_aprazamento = _aprazamentorepository.GetNewId(ibge);
                    var modelapraz = new Aprazamento
                    {
                        id_aprazamento = id_aprazamento,
                        id_individuo = model.CartaoVacina.id_paciente,
                        data_limite = DateTime.Now,
                        id_vacinados = model.CartaoVacina.id,
                        id_produto = model.CartaoVacina.id_produto,
                        id_dose = model.CartaoVacina.id_dose
                    };
                    _aprazamentorepository.Insert(ibge, modelapraz);
                }
                return Ok(model.CartaoVacina);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader]string ibge, [FromBody] CartaoVacina model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _repository.Update(ibge, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var model = _repository.GetCartaoVacinaById(ibge, id);
                if (model.id_esus_exportacao_item != null)
                    throw new Exception("A dose selecionada já foi enviada para o Ministério da Saúde e não pode ser cancelada.");

                _repository.Delete(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}