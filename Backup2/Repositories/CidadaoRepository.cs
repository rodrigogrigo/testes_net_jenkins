using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class CidadaoRepository : ICidadaoRepository
    {
        private readonly ICidadaoCommand _cidadaocommand;

        public CidadaoRepository(ICidadaoCommand commandText)
        {
            _cidadaocommand = commandText;
        }

        public void Excluir(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Execute(_cidadaocommand.Delete, new
                       {
                           @csi_codpac = id
                       }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cidadao> GetAll(string ibge, string filtro)
        {
            try
            {
                var cidadao = new List<Cidadao>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    cidadao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Cidadao>(_cidadaocommand.GetAll.Replace("@filtro", "")).ToList());
                }
                else
                {
                    cidadao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Cidadao>(_cidadaocommand.GetAll.Replace("@filtro", $" {filtro} ")).ToList());
                }

                return cidadao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cidadao> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                var cid = new List<Cidadao>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    cid = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Cidadao>(_cidadaocommand.GetAllPagination.Replace("@filtro", ""), new { @pagesize = pagesize, @page = page }).ToList());
                }
                else
                {
                    cid = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Cidadao>(_cidadaocommand.GetAllPagination.Replace("@filtro", $" {filtro} "), new { @pagesize = pagesize, @page = page }).ToList());
                }

                return cid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cidadao GetCidadaoById(string ibge, int id)
        {
            try
            {
                var cidadao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<Cidadao>(_cidadaocommand.GetCidadaoById, new { @csi_codpac = id }));

                return cidadao;
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
                int count = 0;

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_cidadaocommand.GetCountAll.Replace("@filtro", "")));

                }
                else
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<int>(_cidadaocommand.GetCountAll.Replace("@filtro", filtro)));
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GetFotoByCidadao(string ibge, int id)
        {
            try
            {
                var foto = Helpers.HelperConnection.ExecuteCommandFoto(ibge, conn =>
                          conn.QueryFirstOrDefault<byte[]>(_cidadaocommand.GetFotoByCidadao, new { @id = id }));

                return foto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrauParentesco> GetGrauParentesco(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<GrauParentesco>(_cidadaocommand.GetGrauParentesco)).ToList();

                return lista;
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
                int id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(_cidadaocommand.GetNewId));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(Cidadao model, string ibge)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_cidadaocommand.Insert, new
                        {
                            @csi_codpac = model.csi_codpac,
                            @csi_nompac = model.csi_nompac,
                            @csi_sexpac = model.csi_sexpac,
                            @csi_corpac = model.csi_corpac,
                            @csi_dtnasc = model.csi_dtnasc,
                            @csi_celular = model.csi_celular,
                            @nacionalidade = model.nacionalidade,
                            @csi_codnat = model.csi_codnat,
                            @csi_ncartao = model.csi_ncartao,
                            @csi_cpfpac = model.csi_cpfpac,
                            @csi_idepac = model.csi_idepac,
                            @csi_orgide = model.csi_orgide,
                            @csi_estide = model.csi_estide,
                            @csi_pispac = model.csi_pispac,
                            @emial = model.emial,
                            @csi_maepac = model.csi_maepac,
                            @csi_paipac = model.csi_paipac,
                            @csi_id_nacionalidade = model.csi_id_nacionalidade,
                            @csi_naturalidade_data = model.csi_naturalidade_data,
                            @csi_naturalidade_portaria = model.csi_naturalidade_portaria
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertCadIndividual(string ibge, Cidadao model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_cidadaocommand.InsertCadIndividual, new
                      {
                          @csi_codpac = model.csi_codpac,
                          @csi_nompac = model.csi_nompac.ToUpper(),
                          @nome_social = model.nome_social?.ToUpper(),
                          @csi_sexpac = model.csi_sexpac?.ToUpper(),
                          @csi_corpac = model.csi_corpac?.ToUpper(),
                          @csi_dtnasc = model.csi_dtnasc,
                          @csi_celular = model.csi_celular,
                          @nacionalidade = model.nacionalidade,
                          @csi_codnat = model.csi_codnat?.ToUpper(),
                          @csi_ncartao = model.csi_ncartao,
                          @csi_cpfpac = model.csi_cpfpac,
                          @csi_idepac = model.csi_idepac,
                          @csi_orgide = model.csi_orgide?.ToUpper(),
                          @csi_estide = model.csi_estide?.ToUpper(),
                          @csi_pispac = model.csi_pispac?.ToUpper(),
                          @emial = model.emial?.ToUpper(),
                          @csi_maepac = model.csi_maepac?.ToUpper(),
                          @csi_paipac = model.csi_paipac?.ToUpper(),
                          @csi_id_nacionalidade = model.csi_id_nacionalidade,
                          @csi_naturalidade_data = model.csi_naturalidade_data,
                          @csi_naturalidade_portaria = model.csi_naturalidade_portaria,
                          @csi_codgrau = model.csi_codgrau,
                          @cod_ageesus = model.cod_ageesus,
                          @esus_cns_responsavel_domicilio = model.esus_cns_responsavel_domicilio,
                          @esus_responsavel_domicilio = model.esus_responsavel_domicilio,
                          @fora_area = model.fora_area,
                          @etnia = model.etnia?.ToUpper(),
                          @csi_codpro = model.csi_codpro,
                          @sit_mercado_trab = model.sit_mercado_trab,
                          @csi_escpac = model.csi_escpac?.ToUpper(),
                          @esus_crianca_adulto = model.esus_crianca_adulto?.ToUpper(),
                          @esus_crianca_outra_crianca = model.esus_crianca_outra_crianca?.ToUpper(),
                          @esus_crianca_adolescente = model.esus_crianca_adolescente?.ToUpper(),
                          @esus_crianca_sozinha = model.esus_crianca_sozinha?.ToUpper(),
                          @esus_crianca_creche = model.esus_crianca_creche?.ToUpper(),
                          @esus_crianca_outro = model.esus_crianca_outro?.ToUpper(),
                          @csi_estudando = model.csi_estudando?.ToUpper(),
                          @freq_curandeiro = model.freq_curandeiro?.ToUpper(),
                          @possui_plano_saude = model.possui_plano_saude?.ToUpper(),
                          @grupo_comunitario = model.grupo_comunitario?.ToUpper(),
                          @comunidade_tradic = model.comunidade_tradic?.ToUpper(),
                          @desc_comunidade = model.desc_comunidade?.ToUpper(),
                          @verifica_deficiencia = model.verifica_deficiencia,
                          @def_auditiva = model.def_auditiva?.ToUpper(),
                          @def_visual = model.def_visual?.ToUpper(),
                          @def_intelectul = model.def_intelectual?.ToUpper(),
                          @def_fisica = model.def_fisica?.ToUpper(),
                          @def_outra = model.def_outra?.ToUpper(),
                          @verifica_ident_sex = model.verifica_ident_sex?.ToUpper(),
                          @orientacao_sexual = model.orientacao_sexual,
                          @esus_verifica_ident_genero = model.esus_verifica_ident_genero,
                          @esus_ident_genero = model.esus_ident_genero,
                          @esus_saida_cidadao_cadastro = model.esus_saida_cidadao_cadastro,
                          @csi_data_obito = model.csi_data_obito,
                          @esus_numero_do = model.esus_numero_do?.ToUpper(),
                          @verifica_cardiaca = model.verifica_cardiaca?.ToUpper(),
                          @insulf_cardiaca = model.insulf_cardiaca?.ToUpper(),
                          @cardiaca_nsabe = model.cardiaca_nsabe?.ToUpper(),
                          @cardiaca_outro = model.cardiaca_outro?.ToUpper(),
                          @verifica_rins = model.verifica_rins,
                          @rins_insulficiencia = model.rins_insulficiencia?.ToUpper(),
                          @rins_nsabe = model.rins_nsabe?.ToUpper(),
                          @rins_outros = model.rins_outros?.ToUpper(),
                          @doenca_respiratoria = model.doenca_respiratoria?.ToUpper(),
                          @resp_asma = model.resp_asma?.ToUpper(),
                          @resp_enfisema = model.resp_enfisema?.ToUpper(),
                          @resp_nsabe = model.resp_nsabe?.ToUpper(),
                          @resp_outro = model.resp_outro?.ToUpper(),
                          @internacao = model.internacao?.ToUpper(),
                          @internacao_causa = model.internacao_causa?.ToUpper(),
                          @plantas_medicinais = model.plantas_medicinais?.ToUpper(),
                          @quais_plantas = model.quais_plantas?.ToUpper(),
                          @tratamento_psiq = model.tratamento_psiq?.ToUpper(),
                          @situacao_peso = model.situacao_peso,
                          @domiciliado = model.domiciliado?.ToUpper(),
                          @acamado = model.acamado?.ToUpper(),
                          @cancer = model.cancer?.ToUpper(),
                          @fumante = model.fumante?.ToUpper(),
                          @drogas = model.drogas?.ToUpper(),
                          @alcool = model.alcool?.ToUpper(),
                          @diabetes = model.diabetes?.ToUpper(),
                          @avc_derrame = model.avc_derrame?.ToUpper(),
                          @hipertenso = model.hipertenso?.ToUpper(),
                          @infarto = model.infarto?.ToUpper(),
                          @turberculose = model.tuberculose?.ToUpper(),
                          @hanseniase = model.hanseniase?.ToUpper(),
                          @praticas_complem = model.praticas_complem?.ToUpper(),
                          @outras_condic_01 = model.outras_condic_01?.ToUpper(),
                          @verif_situacao_rua = model.verif_situacao_rua?.ToUpper(),
                          @tempo_situacao_rua = model.tempo_situacao_rua,
                          @outra_instituicao = model.outra_instituicao?.ToUpper(),
                          @desc_instituicao = model.desc_instituicao?.ToUpper(),
                          @visita_familiar = model.visita_familiar?.ToUpper(),
                          @grau_parentesco = model.grau_parentesco?.ToUpper(),
                          @acesso_higientep = model.acesso_higientep?.ToUpper(),
                          @banho = model.banho?.ToUpper(),
                          @acesso_sanit = model.acesso_sanit?.ToUpper(),
                          @higiene_bucal = model.higiene_bucal?.ToUpper(),
                          @higiene_outros = model.higiene_outros?.ToUpper(),
                          @sit_rua_beneficio = model.sit_rua_beneficio?.ToUpper(),
                          @sit_rua_familiar = model.sit_rua_familiar?.ToUpper(),
                          @vezes_alimenta = model.vezes_alimenta,
                          @restaurante_popu = model.restaurante_popu?.ToUpper(),
                          @doac_restaurante = model.doac_restaurante?.ToUpper(),
                          @doac_grp_relig = model.doac_grup_relig?.ToUpper(),
                          @doacao_popular = model.doacao_popular?.ToUpper(),
                          @doacao_outros = model.doacao_outros?.ToUpper()
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Cidadao model, string ibge)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_cidadaocommand.Update, new
                        {
                            @csi_nompac = model.csi_nompac,
                            @csi_sexpac = model.csi_sexpac,
                            @csi_corpac = model.csi_corpac,
                            @csi_dtnasc = model.csi_dtnasc?.ToString("dd.MM.yyyy"),
                            @csi_celular = model.csi_celular,
                            @nacionalidade = model.nacionalidade,
                            @csi_codnat = model.csi_codnat,
                            @csi_ncartao = model.csi_ncartao,
                            @csi_cpfpac = model.csi_cpfpac,
                            @csi_idepac = model.csi_idepac,
                            @csi_orgide = model.csi_orgide,
                            @csi_estide = model.csi_estide,
                            @csi_pispac = model.csi_pispac,
                            @emial = model.emial,
                            @csi_maepac = model.csi_maepac,
                            @csi_paipac = model.csi_paipac,
                            @csi_id_nacionalidade = model.csi_id_nacionalidade,
                            @csi_naturalidade_data = model.csi_naturalidade_data,
                            @csi_naturalidade_portaria = model.csi_naturalidade_portaria,
                            @csi_codpac = model.csi_codpac
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCadIndividual(string ibge, Cidadao model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_cidadaocommand.UpdateCadIndividual, new
                     {
                         @csi_codpac = model.csi_codpac,
                         @csi_nompac = model.csi_nompac,
                         @nome_social = model.nome_social,
                         @csi_sexpac = model.csi_sexpac,
                         @csi_corpac = model.csi_corpac,
                         @csi_dtnasc = model.csi_dtnasc,
                         @csi_celular = model.csi_celular,
                         @nacionalidade = model.nacionalidade,
                         @csi_codnat = model.csi_codnat,
                         @csi_ncartao = model.csi_ncartao,
                         @csi_cpfpac = model.csi_cpfpac,
                         @csi_idepac = model.csi_idepac,
                         @csi_orgide = model.csi_orgide,
                         @csi_estide = model.csi_estide,
                         @csi_pispac = model.csi_pispac,
                         @emial = model.emial,
                         @csi_maepac = model.csi_maepac,
                         @csi_paipac = model.csi_paipac,
                         @csi_id_nacionalidade = model.csi_id_nacionalidade,
                         @csi_naturalidade_data = model.csi_naturalidade_data,
                         @csi_naturalidade_portaria = model.csi_naturalidade_portaria,
                         @csi_codgrau = model.csi_codgrau,
                         @cod_ageesus = model.cod_ageesus,
                         @esus_cns_responsavel_domicilio = model.esus_cns_responsavel_domicilio,
                         @esus_responsavel_domicilio = model.esus_responsavel_domicilio,
                         @fora_area = model.fora_area,
                         @etnia = model.etnia,
                         @csi_codpro = model.csi_codpro,
                         @sit_mercado_trab = model.sit_mercado_trab,
                         @csi_escpac = model.csi_escpac,
                         @esus_crianca_adulto = model.esus_crianca_adulto,
                         @esus_crianca_outra_crianca = model.esus_crianca_outra_crianca,
                         @esus_crianca_adolescente = model.esus_crianca_adolescente,
                         @esus_crianca_sozinha = model.esus_crianca_sozinha,
                         @esus_crianca_creche = model.esus_crianca_creche,
                         @esus_crianca_outro = model.esus_crianca_outro,
                         @csi_estudando = model.csi_estudando,
                         @freq_curandeiro = model.freq_curandeiro,
                         @possui_plano_saude = model.possui_plano_saude,
                         @grupo_comunitario = model.grupo_comunitario,
                         @comunidade_tradic = model.comunidade_tradic,
                         @desc_comunidade = model.desc_comunidade,
                         @verifica_deficiencia = model.verifica_deficiencia,
                         @def_auditiva = model.def_auditiva,
                         @def_visual = model.def_visual,
                         @def_intelectual = model.def_intelectual,
                         @def_fisica = model.def_fisica,
                         @def_outra = model.def_outra,
                         @verifica_ident_sex = model.verifica_ident_sex,
                         @orientacao_sexual = model.orientacao_sexual,
                         @esus_verifica_ident_genero = model.esus_verifica_ident_genero,
                         @esus_ident_genero = model.esus_ident_genero,
                         @esus_saida_cidadao_cadastro = model.esus_saida_cidadao_cadastro,
                         @csi_data_obito = model.csi_data_obito,
                         @esus_numero_do = model.esus_numero_do,
                         @verifica_cardiaca = model.verifica_cardiaca,
                         @insulf_cardiaca = model.insulf_cardiaca,
                         @cardiaca_nsabe = model.cardiaca_nsabe,
                         @cardiaca_outro = model.cardiaca_outro,
                         @verifica_rins = model.verifica_rins,
                         @rins_insulficiencia = model.rins_insulficiencia,
                         @rins_nsabe = model.rins_nsabe,
                         @rins_outros = model.rins_outros,
                         @doenca_respiratoria = model.doenca_respiratoria,
                         @resp_asma = model.resp_asma,
                         @resp_enfisema = model.resp_enfisema,
                         @resp_nsabe = model.resp_nsabe,
                         @resp_outro = model.resp_outro,
                         @internacao = model.internacao,
                         @internacao_causa = model.internacao_causa,
                         @plantas_medicinais = model.plantas_medicinais,
                         @quais_plantas = model.quais_plantas,
                         @tratamento_psiq = model.tratamento_psiq,
                         @situacao_peso = model.situacao_peso,
                         @domiciliado = model.domiciliado,
                         @acamado = model.acamado,
                         @cancer = model.cancer,
                         @fumante = model.fumante,
                         @drogas = model.drogas,
                         @alcool = model.alcool,
                         @diabetes = model.diabetes,
                         @avc_derrame = model.avc_derrame,
                         @hipertenso = model.hipertenso,
                         @infarto = model.infarto,
                         @tuberculose = model.tuberculose,
                         @hanseniase = model.hanseniase,
                         @praticas_complem = model.praticas_complem,
                         @outras_condic_01 = model.outras_condic_01,
                         @verif_situacao_rua = model.verif_situacao_rua,
                         @tempo_situacao_rua = model.tempo_situacao_rua,
                         @outra_instituicao = model.outra_instituicao,
                         @desc_instituicao = model.desc_instituicao,
                         @visita_familiar = model.visita_familiar,
                         @grau_parentesco = model.grau_parentesco,
                         @acesso_higientep = model.acesso_higientep,
                         @banho = model.banho,
                         @acesso_sanit = model.acesso_sanit,
                         @higiene_bucal = model.higiene_bucal,
                         @higiene_outros = model.higiene_outros,
                         @sit_rua_beneficio = model.sit_rua_beneficio,
                         @sit_rua_familiar = model.sit_rua_familiar,
                         @vezes_alimenta = model.vezes_alimenta,
                         @restaurante_popu = model.restaurante_popu,
                         @doac_restaurante = model.doac_restaurante,
                         @doac_grup_relig = model.doac_grup_relig,
                         @doacao_popular = model.doacao_popular,
                         @doacao_outros = model.doacao_outros?.ToUpper()
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<bool, int?, string> ValidaExistenciaCidadao(string ibge, string filtro)
        {
            try
            {
                var cidadao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.QueryFirstOrDefault<dynamic>(_cidadaocommand.ValidaExistenciaCidadao.Replace("@filtro", filtro)));

                if (cidadao != null)
                    return new Tuple<bool, int?, string>(true, cidadao.CSI_CODPAC, cidadao.CSI_NOMPAC);
                else
                    return new Tuple<bool, int?, string>(false, null, string.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<string, string> GetLoginCadSus(string ibge)
        {
            try
            {
                var login = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<dynamic>(_cidadaocommand.GetLoginCadSus));

                return new Tuple<string, string>(login.LOGIN, login.SENHA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
