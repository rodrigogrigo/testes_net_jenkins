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
    public class VisitaDomiciliarRepository : IVisitaDomiciliarRepository
    {
        private IVisitaDomiciliarCommand _command;

        public VisitaDomiciliarRepository(IVisitaDomiciliarCommand command)
        {
            _command = command;       
        }

        public List<VisitaDomiciliarViewModel> GetAllPagination(string ibge, int? page, int? pagesize, string filtro)
        {
            try
            {
                string sql = _command.getAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var Itens = Helpers.HelperConnection.ExecuteCommand<List<VisitaDomiciliarViewModel>>(ibge, conn =>
                            conn.Query<VisitaDomiciliarViewModel>(sql, new
                            {
                                @pagesize = pagesize,
                                @page = page,
                                
                            }).ToList());

                return Itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro)
        {
            try
            {

                string sql = _command.getCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var Itens = Helpers.HelperConnection.ExecuteCommand<int>(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(sql));

                return Itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public VisitaDomiciliar GetById(string ibge, int Id)
        {
            try
            {
                string sql = _command.getById;

                var Itens = Helpers.HelperConnection.ExecuteCommand<VisitaDomiciliar>(ibge, conn => 
                           conn.QueryFirstOrDefault<VisitaDomiciliar>(sql, new { @id = Id}));

                return Itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, VisitaDomiciliar model)
        {
            try
            {
                string sql = _command.Insert;
                Helpers.HelperConnection.ExecuteCommand(ibge, conn => conn.Execute(sql, new
                {
                    @id = model.id,
                    @id_profissional = model.id_profissional,
                    @turno = model.turno,
                    @competencia = model.competencia,
                    @data_visita = model.data_visita,
                    @id_domicilio = model.id_domicilio,
                    @visita_compartilhada = model.visita_compartilhada,
                    @visita_periodica = model.visita_periodica,
                    @ba_consulta = model.ba_consulta,
                    @ba_exame = model.ba_exame,
                    @ba_vacina = model.ba_vacina,
                    @ba_bolsafamilia = model.ba_bolsafamilia,
                    @mv_gestante = model.mv_gestante,
                    @mv_puerpera = model.mv_puerpera,
                    @mv_recem_nascido = model.mv_recem_nascido,
                    @mv_crianca = model.mv_crianca,
                    @mv_desnutricao = model.mv_desnutricao,
                    @mv_reabilitacao_deficiencia = model.mv_reabilitacao_deficiencia,
                    @mv_hipertencao = model.mv_hipertencao,
                    @mv_diabetes = model.mv_diabetes,
                    @mv_asma = model.mv_asma,
                    @mv_dpoc = model.mv_dpoc,
                    @mv_cancer = model.mv_cancer,
                    @mv_doenca_cronica = model.mv_doenca_cronica,
                    @mv_hanseniase = model.mv_hanseniase,
                    @mv_tuberculose = model.mv_tuberculose,
                    @mv_domiciliado_acamado = model.mv_domiciliado_acamado,
                    @mv_vulnerabilidade_social = model.mv_vulnerabilidade_social,
                    @mv_bolsa_familia = model.mv_bolsa_familia,
                    @mv_saude_mental = model.mv_saude_mental,
                    @mv_alcool = model.mv_alcool,
                    @mv_outras_drogas = model.mv_outras_drogas,
                    @mv_internacao = model.mv_internacao,
                    @mv_controle_ambientes = model.mv_controle_ambientes,
                    @mv_atv_coletiva = model.mv_atv_coletiva,
                    @mv_orientacao_prevencao = model.mv_orientacao_prevencao,
                    @mv_outros = model.mv_outros,
                    @desfecho = model.desfecho,
                    @cadastramento_atualizacao = model.cadastramento_atualizacao,
                    @id_cidadao = model.id_cidadao,
                    @uuid = model.uuid,
                    @exportado_esus = model.exportado_esus,
                    @data_alteracao_serv = model.data_alteracao_serv,
                    @uuid_registro_mobile = model.uuid_registro_mobile,
                    @csi_nomusualter = model.csi_nomusualter,
                    @mv_sint_respiratorios = model.mv_sint_respiratorios,
                    @mv_tabagista = model.mv_tabagista,
                    @latitude = model.latitude,
                    @longitude = model.longitude,
                    @codigo_microarea = model.codigo_microarea,
                    @tipo_imovel = model.tipo_imovel,
                    @mv_acao_educativa = model.mv_acao_educativa,
                    @mv_imovel_foco = model.mv_imovel_foco,
                    @mv_acao_mecanica = model.mv_acao_mecanica,
                    @mv_trat_focal = model.mv_trat_focal,
                    @peso = model.peso,
                    @altura = model.altura,
                    @fora_area = model.fora_area,
                    @id_esus_exportacao_item = model.id_esus_exportacao_item,
                    @id_sexo = model.id_sexo,
                    @data_nascimento = model.data_nascimento,
                    @id_unidade = model.id_unidade,
                    @id_usuario = model.id_usuario,
                    @id_estabelecimento = model.id_estabelecimento,
                    @id_equipe = model.id_equipe,
                    @id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote
                }));
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void Update(string ibge, VisitaDomiciliar model)
        {
            try
            {
                string sql = _command.Update;
                Helpers.HelperConnection.ExecuteCommand(ibge, conn => conn.Execute(sql, new
                {
                    @id = model.id,
                    @id_profissional = model.id_profissional,
                    @turno = model.turno,
                    @competencia = model.competencia,
                    @data_visita = model.data_visita,
                    @id_domicilio = model.id_domicilio,
                    @visita_compartilhada = model.visita_compartilhada,
                    @visita_periodica = model.visita_periodica,
                    @ba_consulta = model.ba_consulta,
                    @ba_exame = model.ba_exame,
                    @ba_vacina = model.ba_vacina,
                    @ba_bolsafamilia = model.ba_bolsafamilia,
                    @mv_gestante = model.mv_gestante,
                    @mv_puerpera = model.mv_puerpera,
                    @mv_recem_nascido = model.mv_recem_nascido,
                    @mv_crianca = model.mv_crianca,
                    @mv_desnutricao = model.mv_desnutricao,
                    @mv_reabilitacao_deficiencia = model.mv_reabilitacao_deficiencia,
                    @mv_hipertencao = model.mv_hipertencao,
                    @mv_diabetes = model.mv_diabetes,
                    @mv_asma = model.mv_asma,
                    @mv_dpoc = model.mv_dpoc,
                    @mv_cancer = model.mv_cancer,
                    @mv_doenca_cronica = model.mv_doenca_cronica,
                    @mv_hanseniase = model.mv_hanseniase,
                    @mv_tuberculose = model.mv_tuberculose,
                    @mv_domiciliado_acamado = model.mv_domiciliado_acamado,
                    @mv_vulnerabilidade_social = model.mv_vulnerabilidade_social,
                    @mv_bolsa_familia = model.mv_bolsa_familia,
                    @mv_saude_mental = model.mv_saude_mental,
                    @mv_alcool = model.mv_alcool,
                    @mv_outras_drogas = model.mv_outras_drogas,
                    @mv_internacao = model.mv_internacao,
                    @mv_controle_ambientes = model.mv_controle_ambientes,
                    @mv_atv_coletiva = model.mv_atv_coletiva,
                    @mv_orientacao_prevencao = model.mv_orientacao_prevencao,
                    @mv_outros = model.mv_outros,
                    @desfecho = model.desfecho,
                    @cadastramento_atualizacao = model.cadastramento_atualizacao,
                    @id_cidadao = model.id_cidadao,
                    @uuid = model.uuid,
                    @exportado_esus = model.exportado_esus,
                    @data_alteracao_serv = model.data_alteracao_serv,
                    @uuid_registro_mobile = model.uuid_registro_mobile,
                    @csi_nomusualter = model.csi_nomusualter,
                    @mv_sint_respiratorios = model.mv_sint_respiratorios,
                    @mv_tabagista = model.mv_tabagista,
                    @latitude = model.latitude,
                    @longitude = model.longitude,
                    @codigo_microarea = model.codigo_microarea,
                    @tipo_imovel = model.tipo_imovel,
                    @mv_acao_educativa = model.mv_acao_educativa,
                    @mv_imovel_foco = model.mv_imovel_foco,
                    @mv_acao_mecanica = model.mv_acao_mecanica,
                    @mv_trat_focal = model.mv_trat_focal,
                    @peso = model.peso,
                    @altura = model.altura,
                    @fora_area = model.fora_area,
                    @id_esus_exportacao_item = model.id_esus_exportacao_item,
                    @id_sexo = model.id_sexo,
                    @data_nascimento = model.data_nascimento,
                    @id_unidade = model.id_unidade,
                    @id_usuario = model.id_usuario,
                    @id_estabelecimento = model.id_estabelecimento,
                    @id_equipe = model.id_equipe,
                    @id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote
                }));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Delete(string ibge, int? Id)
        {
            try
            {
                string sql = _command.Delete;
                Helpers.HelperConnection.ExecuteCommand(ibge, conn => conn.Execute(sql, new { @id = Id}));
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
                string sql = _command.GetNewId;
                return Helpers.HelperConnection.ExecuteCommand<int>(ibge, conn => conn.QueryFirstOrDefault<int>(sql));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
