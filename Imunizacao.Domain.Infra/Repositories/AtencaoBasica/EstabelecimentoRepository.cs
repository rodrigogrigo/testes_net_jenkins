using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        private IEstabelecimentoCommand _command;
        public EstabelecimentoRepository(IEstabelecimentoCommand command)
        {
            _command = command;
        }

        public int GetCountEstabelecimentosByArea(string ibge, string filtro, int microarea)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetCountEstabelecimentosByArea.Replace("@filtros", filtro);
                else
                    sql = _command.GetCountEstabelecimentosByArea.Replace("@filtros", string.Empty);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                              conn.QueryFirstOrDefault<int>(sql, new
                              {
                                  @id_microarea = microarea
                              }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Estabelecimento GetEstabelecimentoById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.QueryFirstOrDefault<Estabelecimento>(_command.GetEstabelecimentoById, new
                   {
                       @id = id
                   }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstabelecimentoViewModel> GetEstabelecimentosByArea(string ibge, int page, int pagesize, string filtro, int microarea)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetEstabelecimentosByArea.Replace("@filtros", filtro);
                else
                    sql = _command.GetEstabelecimentosByArea.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                              conn.Query<EstabelecimentoViewModel>(sql, new
                              {
                                  @id_microarea = microarea,
                                  @pagesize = pagesize,
                                  @page = page
                              }).ToList());
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewId(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<int>(_command.GetNewId));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Estabelecimento model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                              conn.Execute(_command.Insert, new
                              {
                                  @id = model.id,
                                  @id_profissional = model.id_profissional,
                                  @id_microarea = model.id_microarea,
                                  @id_logradouro = model.id_logradouro,
                                  @tipo_imovel = model.tipo_imovel,
                                  @numero_logradouro = model.numero_logradouro,
                                  @complemento_logradouro = model.complemento_logradouro,
                                  @telefone_fixo = model.telefone_fixo,
                                  @telefone_movel = model.telefone_movel,
                                  @zona = model.zona,
                                  @tipo_domicilio = model.tipo_domicilio,
                                  @qtd_comodos = model.qtd_comodos,
                                  @tipo_acesso_domic = model.tipo_acesso_domic,
                                  @mat_predominante = model.mat_predominante,
                                  @abastecimento_agua = model.abastecimento_agua,
                                  @escoamento_sanita = model.escoamento_sanita,
                                  @disponib_energia = model.disponib_energia,
                                  @nome_inst_permanencia = model.nome_inst_permanencia,
                                  @outros_profi_instituicao = model.outros_profi_instituicao,
                                  @nome_resp_instit = model.nome_resp_instit,
                                  @cns_resp_instit = model.cns_resp_instit,
                                  @cargo_resp_instit = model.cargo_resp_instit,
                                  @tel_resp_instit = model.tel_resp_instit,
                                  @data_cadastro = model.data_cadastro,
                                  @id_usuario = model.id_usuario
                              }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Estabelecimento model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.Execute(_command.Update, new
                             {
                                 @id_profissional = model.id_profissional,
                                 @id_microarea = model.id_microarea,
                                 @id_logradouro = model.id_logradouro,
                                 @tipo_imovel = model.tipo_imovel,
                                 @numero_logradouro = model.numero_logradouro,
                                 @complemento_logradouro = model.complemento_logradouro,
                                 @telefone_fixo = model.telefone_fixo,
                                 @telefone_movel = model.telefone_movel,
                                 @zona = model.zona,
                                 @tipo_domicilio = model.tipo_domicilio,
                                 @qtd_comodos = model.qtd_comodos,
                                 @tipo_acesso_domic = model.tipo_acesso_domic,
                                 @mat_predominante = model.mat_predominante,
                                 @abastecimento_agua = model.abastecimento_agua,
                                 @escoamento_sanita = model.escoamento_sanita,
                                 @disponib_energia = model.disponib_energia,
                                 @nome_inst_permanencia = model.nome_inst_permanencia,
                                 @outros_profi_instituicao = model.outros_profi_instituicao,
                                 @nome_resp_instit = model.nome_resp_instit,
                                 @cns_resp_instit = model.cns_resp_instit,
                                 @cargo_resp_instit = model.cargo_resp_instit,
                                 @tel_resp_instit = model.tel_resp_instit,
                                 @id = model.id
                             }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
