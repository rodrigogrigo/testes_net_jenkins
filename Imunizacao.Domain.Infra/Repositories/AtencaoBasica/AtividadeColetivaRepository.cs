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
    public class AtividadeColetivaRepository : IAtividadeColetivaRepository
    {
        private IAtividadeColetivaCommand _command;
        private IExameFisicoCommand _exameCommand;
        //public IAtividadeColetivaRepository _repository;
        public AtividadeColetivaRepository(IAtividadeColetivaCommand command, IExameFisicoCommand exameCommand)
        {
            //IAtividadeColetivaRepository repository,
            //_repository = repository;
            _command = command;
            _exameCommand = exameCommand;
        }

        public List<AtividadeColetivaViewModel> GetAll(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand<List<AtividadeColetivaViewModel>>(ibge, conn =>
                         conn.Query<AtividadeColetivaViewModel>(_command.GetAll).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AtividadeColetivaViewModel> GetAllPagination(string ibge, string filtro, int page, int pagesize, int usuario)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand<List<AtividadeColetivaViewModel>>(ibge, conn =>
                           conn.Query<AtividadeColetivaViewModel>(sql, new
                           {
                               @pagesize = pagesize,
                               @page = page,
                               @user = usuario
                           }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AtividadeColetivaEditViewModel GetAtividadeColetivaById(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<AtividadeColetivaEditViewModel>(_command.GetAtividadeColetivaById, new { @codigo = id }));
                if (itens != null)
                {
                    itens.sigtap = new SigtapViewModel()
                    {
                        codigo = itens?.codigo_sigtap,
                        nome = itens?.nome_sigtap
                    };

                    itens.profissional = new profissionalAtividadeViewModel()
                    {
                        csi_cbo = itens?.csi_cbo,
                        csi_codmed = itens?.csi_codmed,
                        csi_inativo_profissional = itens?.csi_inativo_profissional,
                        csi_nommed = itens?.csi_nommed,
                        descricao = itens?.descricao,
                        equipe = itens?.equipe,
                        id_equipe = itens?.id_equipe,
                        id_lotacao = itens?.id_lotacao_responsavel
                    };
                }

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro, int usuario)
        {
            try
            {
                string sql = _command.GetCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(sql, new { @user = usuario }));

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PacienteAtivColetivaViewModel> GetPacienteByAtividadeColetiva(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Query<PacienteAtivColetivaViewModel>(_command.GetPacienteByAtividadeColetiva, new
                                     {
                                         @id_atividade = id
                                     }).ToList());

                foreach (var item in itens)
                {
                    var ultimopeso = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<dynamic>(_exameCommand.GetLastPesoByPaciente, new { @id_paciente = (int)item.csi_codpac }));

                    var ultimaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<dynamic>(_exameCommand.GetLastAlturaPaciente, new { @id_paciente = (int)item.csi_codpac }));

                    item.csi_peso = ultimopeso?.PESO;
                    item.csi_altura = ultimaaltura?.ALTURA;
                    item.csi_data_altura = ultimaaltura?.DATA_ALTURA;
                    item.csi_data_peso = ultimopeso?.DATA_PESO;
                    item.csi_dtnasc = item.dtnascimento?.ToString("yyyy-MM-dd HH:mm:ss");
                }

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalAtivColetivaViewModel> GetProfissionalByAtividadeColetiva(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Query<ProfissionalAtivColetivaViewModel>(_command.GetProfissionalByAtividadeColetiva, new
                                     {
                                         @id_atividade = id
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
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //JAKISON
        public int GetNewIdProf(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetNewIdProf));
                return itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetNewIdPartic(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetNewIdPartic));
                return itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //JAKISON

        public void Insert(string ibge, AtividadeColetiva model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Execute(_command.Insert, new
                                     {
                                         @id = model.id,
                                         @tipo_atividade = model.tipo_atividade,
                                         @numero_inep_escola = model.numero_inep_escola,
                                         @data_atividade = model.data_atividade,
                                         @hora_fim = model.hora_fim,
                                         @hora_inicio = model.hora_inicio,
                                         @local_atividade = model.local_atividade,
                                         @num_alteracoes = model.num_alteracoes,
                                         @num_participantes = model.num_participantes,
                                         @pratica_ali_saudavel = model.pratica_ali_saudavel,
                                         @pratica_aplica_fluor = model.pratica_aplica_fluor,
                                         @pratica_acuid_visual = model.pratica_acuid_visual,
                                         @pratica_autocuidado = model.pratica_autocuidado,
                                         @pratica_cidad_direitos = model.pratica_cidad_direitos,
                                         @pratica_saude_trabalha = model.pratica_saude_trabalha,
                                         @pratica_deped_quimica = model.pratica_deped_quimica,
                                         @pratica_envelhecimento = model.pratica_envelhecimento,
                                         @pratica_escova_dental = model.pratica_escova_dental,
                                         @pratica_planta_medici = model.pratica_planta_medici,
                                         @pratica_atividade_fisic = model.pratica_atividade_fisic,
                                         @pratica_corpo_mental = model.pratica_corpo_mental,
                                         @pratica_corp_ment_pic = model.pratica_corp_ment_pic,
                                         @pratica_prev_viol_cultu = model.pratica_prev_viol_cultu,
                                         @pratica_saude_ambiental = model.pratica_saude_ambiental,
                                         @pratica_saude_bucal = model.pratica_saude_bucal,
                                         @pratica_saude_mental = model.pratica_saude_mental,
                                         @pratica_sexual_reprodut = model.pratica_sexual_reprodut,
                                         @pratica_saude_escola = model.pratica_saude_escola,
                                         @pratica_agravo_negligen = model.pratica_agravo_negligen,
                                         @pratica_antropometria = model.pratica_antropometria,
                                         @pratica_outros = model.pratica_outros,
                                         @previsao_participantes = model.previsao_participantes,
                                         @publico_comunidade = model.publico_comunidade,
                                         @publico_crian_0_3_anos = model.publico_crian_0_3_anos,
                                         @publico_crian_4_5_anos = model.publico_crian_4_5_anos,
                                         @publico_crian_6_11_anos = model.publico_crian_6_11_anos,
                                         @publico_adolescente = model.publico_adolescente,
                                         @publico_mulher = model.publico_mulher,
                                         @publico_gestante = model.publico_mulher,
                                         @publico_homem = model.publico_homem,
                                         @publico_familiares = model.publico_familiares,
                                         @publico_idosos = model.publico_idosos,
                                         @publico_pess_doenca = model.publico_pess_doenca,
                                         @publico_usua_tabaco = model.publico_usua_tabaco,
                                         @publico_usua_alcool = model.publico_usua_alcool,
                                         @publico_usua_drogas = model.publico_usua_drogas,
                                         @publico_sofrim_mental = model.publico_sofrim_mental,
                                         @publico_prof_educacao = model.publico_prof_educacao,
                                         @publico_outros = model.publico_outros,
                                         @id_lotacao_responsavel = model.id_lotacao_responsavel,
                                         @situacao_envio = model.situacao_envio,
                                         @tema_questo_administr = model.tema_questo_administr,
                                         @tema_processo_trabalho = model.tema_processo_trabalho,
                                         @tema_diagnostico_territ = model.tema_diagnostico_territ,
                                         @tema_planeja_monitoram = model.tema_planeja_monitoram,
                                         @tema_discussao_casa = model.tema_discussao_casa,
                                         @tema_educa_permanent = model.tema_educa_permanent,
                                         @tema_outros = model.tema_outros,
                                         @exportado_esus = model.exportado_esus,
                                         @data_alteracao_serv = model.data_alteracao_serv,
                                         @saude_auditiva = model.saude_auditiva,
                                         @desenvolvimento_linguagem = model.desenvolvimento_linguagem,
                                         @verificacao_situacao_vacinal = model.verificacao_situacao_vacinal,
                                         @pnct_sessao_1 = model.pnct_sessao_1,
                                         @pnct_sessao_2 = model.pnct_sessao_2,
                                         @pnct_sessao_3 = model.pnct_sessao_3,
                                         @pnct_sessao_4 = model.pnct_sessao_4,
                                         @turno = model.turno,
                                         @codigo_sigtap = model.codigo_sigtap,
                                         @cnes = model.cnes,
                                         @id_esus_exportacao_item = model.id_esus_exportacao_item,
                                         @id_profissional = model.id_profissional,
                                         @id_equipe = model.id_equipe,
                                         @id_unidade = model.id_equipe,
                                         @flg_atendimento_especializado = model.flg_atendimento_especializado,
                                         @id_usuario = model.id_usuario,
                                         @flg_pratica_educativa = model.flg_pratica_educativa,
                                         @flg_pratica_saude = model.flg_pratica_saude,
                                         @id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote,
                                         @local_atendimento = model.local_atendimento,
                                         @id_escola = model.id_escola,
                                         @id_estabelecimento = model.id_estabelecimento
                                     }));

                //JAKISON
                foreach (ProfissionalParticipante profissional in model.profissionaisParticipantes)
                {
                    profissional.id = GetNewIdProf(ibge);

                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_command.InsertProfissionalParticipante, new
                    {
                        @id = profissional.id,
                        @id_atividade = model.id,
                        @id_medico = profissional.csi_codmed,
                        @id_cbo = profissional.csi_cbo,
                        @id_lotacao = profissional.id_lotacao
                    }));
                }

                foreach (PessoaParticipante pessoa in model.pessoasParticipantes)
                {
                    pessoa.id = GetNewIdPartic(ibge);

                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_command.InsertPessoaParticipante, new
                    {
                        @id = pessoa.id,
                        @id_cidadao = pessoa.csi_codpac,
                        @id_atividade = model.id,
                        @altura = pessoa.csi_altura,
                        @peso = pessoa.csi_peso,
                        @avalia_alterada = pessoa.avalia_alterada,
                        @cessou_habito_fumar = pessoa.cessou_habito_fumar,
                        @abandono_grupo = pessoa.abandonou_grupo,
                        @cns_cidadao = pessoa.csi_ncartao,
                        @dtnascimento = pessoa.csi_dtnasc,
                        @sexo_cidadao = pessoa.csi_sexpac
                    }));
                }
                //JAKISON
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, AtividadeColetiva model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_command.Update, new
                                    {
                                        @tipo_atividade = model.tipo_atividade,
                                        @numero_inep_escola = model.numero_inep_escola,
                                        @data_atividade = model.data_atividade,
                                        @hora_fim = model.hora_fim,
                                        @hora_inicio = model.hora_inicio,
                                        @local_atividade = model.local_atividade,
                                        @num_alteracoes = model.num_alteracoes,
                                        @num_participantes = model.num_participantes,
                                        @pratica_ali_saudavel = model.pratica_ali_saudavel,
                                        @pratica_aplica_fluor = model.pratica_aplica_fluor,
                                        @pratica_acuid_visual = model.pratica_acuid_visual,
                                        @pratica_autocuidado = model.pratica_autocuidado,
                                        @pratica_cidad_direitos = model.pratica_cidad_direitos,
                                        @pratica_saude_trabalha = model.pratica_saude_trabalha,
                                        @pratica_deped_quimica = model.pratica_deped_quimica,
                                        @pratica_envelhecimento = model.pratica_envelhecimento,
                                        @pratica_escova_dental = model.pratica_escova_dental,
                                        @pratica_planta_medici = model.pratica_planta_medici,
                                        @pratica_atividade_fisic = model.pratica_atividade_fisic,
                                        @pratica_corpo_mental = model.pratica_corpo_mental,
                                        @pratica_corp_ment_pic = model.pratica_corp_ment_pic,
                                        @pratica_prev_viol_cultu = model.pratica_prev_viol_cultu,
                                        @pratica_saude_ambiental = model.pratica_saude_ambiental,
                                        @pratica_saude_bucal = model.pratica_saude_bucal,
                                        @pratica_saude_mental = model.pratica_saude_mental,
                                        @pratica_sexual_reprodut = model.pratica_sexual_reprodut,
                                        @pratica_saude_escola = model.pratica_saude_escola,
                                        @pratica_agravo_negligen = model.pratica_agravo_negligen,
                                        @pratica_antropometria = model.pratica_antropometria,
                                        @pratica_outros = model.pratica_outros,
                                        @previsao_participantes = model.previsao_participantes,
                                        @publico_comunidade = model.publico_comunidade,
                                        @publico_crian_0_3_anos = model.publico_crian_0_3_anos,
                                        @publico_crian_4_5_anos = model.publico_crian_4_5_anos,
                                        @publico_crian_6_11_anos = model.publico_crian_6_11_anos,
                                        @publico_adolescente = model.publico_adolescente,
                                        @publico_mulher = model.publico_mulher,
                                        @publico_gestante = model.publico_mulher,
                                        @publico_homem = model.publico_homem,
                                        @publico_familiares = model.publico_familiares,
                                        @publico_idosos = model.publico_idosos,
                                        @publico_pess_doenca = model.publico_pess_doenca,
                                        @publico_usua_tabaco = model.publico_usua_tabaco,
                                        @publico_usua_alcool = model.publico_usua_alcool,
                                        @publico_usua_drogas = model.publico_usua_drogas,
                                        @publico_sofrim_mental = model.publico_sofrim_mental,
                                        @publico_prof_educacao = model.publico_prof_educacao,
                                        @publico_outros = model.publico_outros,
                                        @id_lotacao_responsavel = model.id_lotacao_responsavel,
                                        @situacao_envio = model.situacao_envio,
                                        @tema_questo_administr = model.tema_questo_administr,
                                        @tema_processo_trabalho = model.tema_processo_trabalho,
                                        @tema_diagnostico_territ = model.tema_diagnostico_territ,
                                        @tema_planeja_monitoram = model.tema_planeja_monitoram,
                                        @tema_discussao_casa = model.tema_discussao_casa,
                                        @tema_educa_permanent = model.tema_educa_permanent,
                                        @tema_outros = model.tema_outros,
                                        @exportado_esus = model.exportado_esus,
                                        @data_alteracao_serv = model.data_alteracao_serv,
                                        @saude_auditiva = model.saude_auditiva,
                                        @desenvolvimento_linguagem = model.desenvolvimento_linguagem,
                                        @verificacao_situacao_vacinal = model.verificacao_situacao_vacinal,
                                        @pnct_sessao_1 = model.pnct_sessao_1,
                                        @pnct_sessao_2 = model.pnct_sessao_2,
                                        @pnct_sessao_3 = model.pnct_sessao_3,
                                        @pnct_sessao_4 = model.pnct_sessao_4,
                                        @turno = model.turno,
                                        @codigo_sigtap = model.codigo_sigtap,
                                        @cnes = model.cnes,
                                        @id_esus_exportacao_item = model.id_esus_exportacao_item,
                                        @id_profissional = model.id_profissional,
                                        @id_equipe = model.id_equipe,
                                        @id_unidade = model.id_unidade,
                                        @flg_atendimento_especializado = model.flg_atendimento_especializado,
                                        @id_usuario = model.id_usuario,
                                        @flg_pratica_educativa = model.flg_pratica_educativa,
                                        @flg_pratica_saude = model.flg_pratica_saude,
                                        @id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote,
                                        @local_atendimento = model.local_atendimento,
                                        @id_escola = model.id_escola,
                                        @id_estabelecimento = model.id_estabelecimento,
                                        @id = model.id,
                                    }));

                //JAKISON
                foreach (ProfissionalParticipante profissional in model.profissionaisParticipantes)
                {
                    if (profissional.id == null)
                        profissional.id = GetNewIdProf(ibge);

                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_command.InsertProfissionalParticipante, new
                    {
                        @id = profissional.id,
                        @id_atividade = model.id,
                        @id_medico = profissional.csi_codmed,
                        @id_cbo = profissional.csi_cbo,
                        @id_lotacao = profissional.id_lotacao
                    }));
                }

                foreach (PessoaParticipante pessoa in model.pessoasParticipantes)
                {
                    if (pessoa.id == null)
                        pessoa.id = GetNewIdPartic(ibge);

                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_command.InsertPessoaParticipante, new
                    {
                        @id = pessoa.id,
                        @id_cidadao = pessoa.csi_codpac,
                        @id_atividade = model.id,
                        @altura = pessoa.csi_altura,
                        @peso = pessoa.csi_peso,
                        @avalia_alterada = pessoa.avalia_alterada,
                        @cessou_habito_fumar = pessoa.cessou_habito_fumar,
                        @abandono_grupo = pessoa.abandonou_grupo,
                        @cns_cidadao = pessoa.csi_ncartao,
                        @dtnascimento = pessoa.csi_dtnasc,
                        @sexo_cidadao = pessoa.csi_sexpac
                    }));
                }
                //JAKISON
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_command.Delete, new
                                    {
                                        @id = id
                                    }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProfissional(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Execute(_command.DeleteProfissional, new
                                   {
                                       @id = id
                                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteParticipante(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Execute(_command.DeleteParticipante, new
                                   {
                                       @id = id
                                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProfissionalByAtividade(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Execute(_command.DeleteProfissionalByAtividade, new
                                   {
                                       @id = id
                                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteParticipanteByAtividade(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.Execute(_command.DeleteParticipanteByAtividade, new
                                  {
                                      @id = id
                                  }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
