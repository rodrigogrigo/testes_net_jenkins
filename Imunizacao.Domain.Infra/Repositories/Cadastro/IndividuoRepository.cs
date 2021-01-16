using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class IndividuoRepository : IIndividuoRepository
    {
        private readonly IIndividuoCommand _indidividuoCommand;

        public IndividuoRepository(IIndividuoCommand commandText)
        {
            _indidividuoCommand = commandText;
        }

        public int GetNewId(string ibge)
        {
            try
            {
                int id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(_indidividuoCommand.GetNewId));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Individuo model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_indidividuoCommand.Insert, new
                      {
                          @id_usuario = model.id_usuario,

                          // Identificação
                          @csi_codpac = model.csi_codpac,
                          @csi_nompac = model.csi_nompac?.ToUpper(),
                          @nome_social = model.nome_social?.ToUpper(),
                          @csi_sexpac = model.csi_sexpac,
                          @csi_corpac = model.csi_corpac,
                          @etnia = model.etnia?.ToUpper(),
                          @csi_dtnasc = model.csi_dtnasc,
                          @emial = model.emial?.ToUpper(),
                          @csi_celular = model.csi_celular?.ToUpper(),
                          @csi_maepac = model.csi_maepac?.ToUpper(),
                          @csi_paipac = model.csi_paipac?.ToUpper(),
                          @nacionalidade = model.nacionalidade,
                          @csi_codnat = model.csi_codnat?.ToUpper(),
                          @csi_naturalidade_data = model.csi_naturalidade_data,
                          @csi_naturalidade_portaria = model.csi_naturalidade_portaria?.ToUpper(),
                          @csi_id_nacionalidade = model.csi_id_nacionalidade,
                          @csi_data_entrada_pais = model.csi_data_entrada_pais,
                          @csi_sanguegrupo = model.csi_sanguegrupo,
                          @csi_sanguefator = model.csi_sanguefator,
                          @csi_codhemoes = model.csi_codhemoes?.ToUpper(),
                          @csi_dsangue = model.csi_dsangue?.ToUpper(),
                          @verif_situacao_rua = model.verif_situacao_rua?.ToUpper(),

                          // Documentos
                          @csi_cpfpac = model.csi_cpfpac?.ToUpper(),
                          @csi_ncartao = model.csi_ncartao?.ToUpper(),
                          @csi_pispac = model.csi_pispac?.ToUpper(),
                          @csi_idepac = model.csi_idepac?.ToUpper(),
                          @csi_orgide = model.csi_orgide,
                          @csi_comide = model.csi_comide?.ToUpper(),
                          @csi_expide = model.csi_expide,
                          @csi_estide = model.csi_estide?.ToUpper(),
                          @csi_dt_primeira_cnh = model.csi_dt_primeira_cnh,
                          @csi_tipcer = model.csi_tipcer,
                          @numero_dnv = model.numero_dnv,
                          @csi_numcer = model.csi_numcer?.ToUpper(),
                          @csi_folivr = model.csi_folivr?.ToUpper(),
                          @csi_livcer = model.csi_livcer?.ToUpper(),
                          @csi_certidao_termo = model.csi_certidao_termo?.ToUpper(),
                          @csi_emicer = model.csi_emicer,
                          @csi_nova_certidao = model.csi_nova_certidao?.ToUpper(),
                          @csi_carcer = model.csi_carcer?.ToUpper(),
                          @csi_cidcerc = model.csi_cidcerc,
                          @csi_titele = model.csi_titele?.ToUpper(),
                          @csi_zontit = model.csi_zontit?.ToUpper(),
                          @csi_sectit = model.csi_sectit?.ToUpper(),
                          @csi_ctpsas = model.csi_ctpsas?.ToUpper(),
                          @csi_ctps_serie = model.csi_ctps_serie,
                          @csi_ctps_dtemis = model.csi_ctps_dtemis,
                          @csi_ctps_uf = model.csi_ctps_uf?.ToUpper(),
                          @csi_acerres = model.csi_acerres?.ToUpper(),
                          @csi_acarvac = model.csi_acarvac?.ToUpper(),
                          @csi_acomres = model.csi_acomres?.ToUpper(),
                          @csi_ncerres = model.csi_ncerres?.ToUpper(),
                          @num_processo_estado = model.num_processo_estado?.ToUpper(),
                          @csi_cre = model.csi_cre?.ToUpper(),

                          // Endereço
                          @csi_codend = model.csi_codend,
                          @csi_endpac = model.csi_endpac?.ToUpper(),
                          @csi_numero_logradouro = model.csi_numero_logradouro?.ToUpper(),
                          @complemento = model.complemento?.ToUpper(),
                          @csi_baipac = model.csi_baipac?.ToUpper(),
                          @csi_ceppac = model.csi_ceppac?.ToUpper(),
                          @csi_codcid = model.csi_codcid?.ToUpper(),
                          @id_estabelecimento_saude = model.id_estabelecimento_saude,
                          @cod_ageesus = model.cod_ageesus,
                          @fora_area = model.fora_area?.ToUpper(),

                          // Informações Sociodemográficas
                          @csi_estciv = model.csi_estciv,
                          @csi_situacao_familiar = model.csi_situacao_familiar?.ToUpper(),
                          @csi_codgrau = model.csi_codgrau,
                          @csi_codpro = model.csi_codpro,
                          @csi_escpac = model.csi_escpac?.ToUpper(),
                          @sit_mercado_trab = model.sit_mercado_trab,
                          @comunidade_tradic = model.comunidade_tradic?.ToUpper(),
                          @desc_comunidade = model.desc_comunidade?.ToUpper(),
                          @esus_verifica_ident_genero = model.esus_verifica_ident_genero?.ToUpper(),
                          @esus_ident_genero = model.esus_ident_genero,
                          @verifica_ident_sex = model.verifica_ident_sex?.ToUpper(),
                          @orientacao_sexual = model.orientacao_sexual,
                          @esus_crianca_adulto = model.esus_crianca_adulto?.ToUpper(),
                          @esus_crianca_outra_crianca = model.esus_crianca_outra_crianca?.ToUpper(),
                          @esus_crianca_outro = model.esus_crianca_outro?.ToUpper(),
                          @esus_crianca_adolescente = model.esus_crianca_adolescente?.ToUpper(),
                          @esus_crianca_sozinha = model.esus_crianca_sozinha?.ToUpper(),
                          @esus_crianca_creche = model.esus_crianca_creche?.ToUpper(),
                          @verifica_deficiencia = model.verifica_deficiencia,
                          @def_auditiva = model.def_auditiva?.ToUpper(),
                          @def_intelectual = model.def_intelectual?.ToUpper(),
                          @def_visual = model.def_visual?.ToUpper(),
                          @def_fisica = model.def_fisica?.ToUpper(),
                          @def_outra = model.def_outra?.ToUpper(),
                          @csi_estudando = model.csi_estudando?.ToUpper(),
                          @freq_curandeiro = model.freq_curandeiro?.ToUpper(),
                          @grupo_comunitario = model.grupo_comunitario?.ToUpper(),
                          @possui_plano_saude = model.possui_plano_saude?.ToUpper(),

                          // Condições de Saúde
                          @situacao_peso = model.situacao_peso,
                          @internacao = model.internacao?.ToUpper(),
                          @internacao_causa = model.internacao_causa?.ToUpper(),
                          @plantas_medicinais = model.plantas_medicinais?.ToUpper(),
                          @quais_plantas = model.quais_plantas?.ToUpper(),
                          @verifica_cardiaca = model.verifica_cardiaca?.ToUpper(),
                          @insulf_cardiaca = model.insulf_cardiaca?.ToUpper(),
                          @cardiaca_outro = model.cardiaca_outro?.ToUpper(),
                          @cardiaca_nsabe = model.cardiaca_nsabe?.ToUpper(),
                          @verifica_rins = model.verifica_rins,
                          @rins_insulficiencia = model.rins_insulficiencia?.ToUpper(),
                          @rins_outros = model.rins_outros?.ToUpper(),
                          @rins_nsabe = model.rins_nsabe?.ToUpper(),
                          @doenca_respiratoria = model.doenca_respiratoria?.ToUpper(),
                          @resp_asma = model.resp_asma?.ToUpper(),
                          @resp_enfisema = model.resp_enfisema?.ToUpper(),
                          @resp_outro = model.resp_outro?.ToUpper(),
                          @resp_nsabe = model.resp_nsabe?.ToUpper(),
                          @fumante = model.fumante?.ToUpper(),
                          @alcool = model.alcool?.ToUpper(),
                          @drogas = model.drogas?.ToUpper(),
                          @hipertenso = model.hipertenso?.ToUpper(),
                          @diabetes = model.diabetes?.ToUpper(),
                          @avc_derrame = model.avc_derrame?.ToUpper(),
                          @infarto = model.infarto?.ToUpper(),
                          @hanseniase = model.hanseniase?.ToUpper(),
                          @tuberculose = model.tuberculose?.ToUpper(),
                          @cancer = model.cancer?.ToUpper(),
                          @tratamento_psiq = model.tratamento_psiq?.ToUpper(),
                          @acamado = model.acamado?.ToUpper(),
                          @domiciliado = model.domiciliado?.ToUpper(),
                          @praticas_complem = model.praticas_complem?.ToUpper(),
                          @outras_condic_01 = model.outras_condic_01?.ToUpper(),
                          @outras_condic_02 = model.outras_condic_02?.ToUpper(),
                          @outras_condic_03 = model.outras_condic_03?.ToUpper(),

                          // Situação de Rua
                          @tempo_situacao_rua = model.tempo_situacao_rua,
                          @vezes_alimenta = model.vezes_alimenta,
                          @outra_instituicao = model.outra_instituicao?.ToUpper(),
                          @desc_instituicao = model.desc_instituicao?.ToUpper(),
                          @grau_parentesco = model.grau_parentesco?.ToUpper(),
                          @visita_familiar = model.visita_familiar?.ToUpper(),
                          @sit_rua_beneficio = model.sit_rua_beneficio?.ToUpper(),
                          @sit_rua_familiar = model.sit_rua_familiar?.ToUpper(),
                          @banho = model.banho?.ToUpper(),
                          @higiene_bucal = model.higiene_bucal?.ToUpper(),
                          @acesso_sanit = model.acesso_sanit?.ToUpper(),
                          @higiene_outros = model.higiene_outros?.ToUpper(),
                          @doac_restaurante = model.doac_restaurante?.ToUpper(),
                          @restaurante_popu = model.restaurante_popu?.ToUpper(),
                          @doac_grup_relig = model.doac_grup_relig?.ToUpper(),
                          @doacao_popular = model.doacao_popular?.ToUpper(),
                          @doacao_outros = model.doacao_outros?.ToUpper(),
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Individuo model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_indidividuoCommand.Update, new
                     {
                         @id_usuario = model.id_usuario,

                         // Identificação
                         @csi_codpac = model.csi_codpac,
                         @csi_nompac = model.csi_nompac?.ToUpper(),
                         @nome_social = model.nome_social?.ToUpper(),
                         @csi_sexpac = model.csi_sexpac,
                         @csi_corpac = model.csi_corpac,
                         @etnia = model.etnia?.ToUpper(),
                         @csi_dtnasc = model.csi_dtnasc,
                         @emial = model.emial?.ToUpper(),
                         @csi_celular = model.csi_celular?.ToUpper(),
                         @csi_maepac = model.csi_maepac?.ToUpper(),
                         @csi_paipac = model.csi_paipac?.ToUpper(),
                         @nacionalidade = model.nacionalidade,
                         @csi_codnat = model.csi_codnat?.ToUpper(),
                         @csi_naturalidade_data = model.csi_naturalidade_data,
                         @csi_naturalidade_portaria = model.csi_naturalidade_portaria?.ToUpper(),
                         @csi_id_nacionalidade = model.csi_id_nacionalidade,
                         @csi_data_entrada_pais = model.csi_data_entrada_pais,
                         @csi_sanguegrupo = model.csi_sanguegrupo,
                         @csi_sanguefator = model.csi_sanguefator,
                         @csi_codhemoes = model.csi_codhemoes?.ToUpper(),
                         @csi_dsangue = model.csi_dsangue?.ToUpper(),
                         @verif_situacao_rua = model.verif_situacao_rua?.ToUpper(),

                         // Documentos
                         @csi_cpfpac = model.csi_cpfpac?.ToUpper(),
                         @csi_ncartao = model.csi_ncartao?.ToUpper(),
                         @csi_pispac = model.csi_pispac?.ToUpper(),
                         @csi_idepac = model.csi_idepac?.ToUpper(),
                         @csi_orgide = model.csi_orgide,
                         @csi_comide = model.csi_comide?.ToUpper(),
                         @csi_expide = model.csi_expide,
                         @csi_estide = model.csi_estide?.ToUpper(),
                         @csi_dt_primeira_cnh = model.csi_dt_primeira_cnh,
                         @csi_tipcer = model.csi_tipcer,
                         @numero_dnv = model.numero_dnv,
                         @csi_numcer = model.csi_numcer?.ToUpper(),
                         @csi_folivr = model.csi_folivr?.ToUpper(),
                         @csi_livcer = model.csi_livcer?.ToUpper(),
                         @csi_certidao_termo = model.csi_certidao_termo?.ToUpper(),
                         @csi_emicer = model.csi_emicer,
                         @csi_nova_certidao = model.csi_nova_certidao?.ToUpper(),
                         @csi_carcer = model.csi_carcer?.ToUpper(),
                         @csi_cidcerc = model.csi_cidcerc,
                         @csi_titele = model.csi_titele?.ToUpper(),
                         @csi_zontit = model.csi_zontit?.ToUpper(),
                         @csi_sectit = model.csi_sectit?.ToUpper(),
                         @csi_ctpsas = model.csi_ctpsas?.ToUpper(),
                         @csi_ctps_serie = model.csi_ctps_serie,
                         @csi_ctps_dtemis = model.csi_ctps_dtemis,
                         @csi_ctps_uf = model.csi_ctps_uf?.ToUpper(),
                         @csi_acerres = model.csi_acerres?.ToUpper(),
                         @csi_acarvac = model.csi_acarvac?.ToUpper(),
                         @csi_acomres = model.csi_acomres?.ToUpper(),
                         @csi_ncerres = model.csi_ncerres?.ToUpper(),
                         @num_processo_estado = model.num_processo_estado?.ToUpper(),
                         @csi_cre = model.csi_cre?.ToUpper(),

                         // Endereço
                         @csi_codend = model.csi_codend,
                         @csi_endpac = model.csi_endpac?.ToUpper(),
                         @csi_numero_logradouro = model.csi_numero_logradouro?.ToUpper(),
                         @complemento = model.complemento?.ToUpper(),
                         @csi_baipac = model.csi_baipac?.ToUpper(),
                         @csi_ceppac = model.csi_ceppac?.ToUpper(),
                         @csi_codcid = model.csi_codcid?.ToUpper(),
                         @id_estabelecimento_saude = model.id_estabelecimento_saude,
                         @cod_ageesus = model.cod_ageesus,
                         @fora_area = model.fora_area?.ToUpper(),

                         // Informações Sociodemográficas
                         @csi_estciv = model.csi_estciv,
                         @csi_situacao_familiar = model.csi_situacao_familiar?.ToUpper(),
                         @csi_codgrau = model.csi_codgrau,
                         @csi_codpro = model.csi_codpro,
                         @csi_escpac = model.csi_escpac?.ToUpper(),
                         @sit_mercado_trab = model.sit_mercado_trab,
                         @comunidade_tradic = model.comunidade_tradic?.ToUpper(),
                         @desc_comunidade = model.desc_comunidade?.ToUpper(),
                         @esus_verifica_ident_genero = model.esus_verifica_ident_genero?.ToUpper(),
                         @esus_ident_genero = model.esus_ident_genero,
                         @verifica_ident_sex = model.verifica_ident_sex?.ToUpper(),
                         @orientacao_sexual = model.orientacao_sexual,
                         @esus_crianca_adulto = model.esus_crianca_adulto?.ToUpper(),
                         @esus_crianca_outra_crianca = model.esus_crianca_outra_crianca?.ToUpper(),
                         @esus_crianca_outro = model.esus_crianca_outro?.ToUpper(),
                         @esus_crianca_adolescente = model.esus_crianca_adolescente?.ToUpper(),
                         @esus_crianca_sozinha = model.esus_crianca_sozinha?.ToUpper(),
                         @esus_crianca_creche = model.esus_crianca_creche?.ToUpper(),
                         @verifica_deficiencia = model.verifica_deficiencia,
                         @def_auditiva = model.def_auditiva?.ToUpper(),
                         @def_intelectual = model.def_intelectual?.ToUpper(),
                         @def_visual = model.def_visual?.ToUpper(),
                         @def_fisica = model.def_fisica?.ToUpper(),
                         @def_outra = model.def_outra?.ToUpper(),
                         @csi_estudando = model.csi_estudando?.ToUpper(),
                         @freq_curandeiro = model.freq_curandeiro?.ToUpper(),
                         @grupo_comunitario = model.grupo_comunitario?.ToUpper(),
                         @possui_plano_saude = model.possui_plano_saude?.ToUpper(),

                         // Condições de Saúde
                         @situacao_peso = model.situacao_peso,
                         @internacao = model.internacao?.ToUpper(),
                         @internacao_causa = model.internacao_causa?.ToUpper(),
                         @plantas_medicinais = model.plantas_medicinais?.ToUpper(),
                         @quais_plantas = model.quais_plantas?.ToUpper(),
                         @verifica_cardiaca = model.verifica_cardiaca?.ToUpper(),
                         @insulf_cardiaca = model.insulf_cardiaca?.ToUpper(),
                         @cardiaca_outro = model.cardiaca_outro?.ToUpper(),
                         @cardiaca_nsabe = model.cardiaca_nsabe?.ToUpper(),
                         @verifica_rins = model.verifica_rins,
                         @rins_insulficiencia = model.rins_insulficiencia?.ToUpper(),
                         @rins_outros = model.rins_outros?.ToUpper(),
                         @rins_nsabe = model.rins_nsabe?.ToUpper(),
                         @doenca_respiratoria = model.doenca_respiratoria?.ToUpper(),
                         @resp_asma = model.resp_asma?.ToUpper(),
                         @resp_enfisema = model.resp_enfisema?.ToUpper(),
                         @resp_outro = model.resp_outro?.ToUpper(),
                         @resp_nsabe = model.resp_nsabe?.ToUpper(),
                         @fumante = model.fumante?.ToUpper(),
                         @alcool = model.alcool?.ToUpper(),
                         @drogas = model.drogas?.ToUpper(),
                         @hipertenso = model.hipertenso?.ToUpper(),
                         @diabetes = model.diabetes?.ToUpper(),
                         @avc_derrame = model.avc_derrame?.ToUpper(),
                         @infarto = model.infarto?.ToUpper(),
                         @hanseniase = model.hanseniase?.ToUpper(),
                         @tuberculose = model.tuberculose?.ToUpper(),
                         @cancer = model.cancer?.ToUpper(),
                         @tratamento_psiq = model.tratamento_psiq?.ToUpper(),
                         @acamado = model.acamado?.ToUpper(),
                         @domiciliado = model.domiciliado?.ToUpper(),
                         @praticas_complem = model.praticas_complem?.ToUpper(),
                         @outras_condic_01 = model.outras_condic_01?.ToUpper(),
                         @outras_condic_02 = model.outras_condic_02?.ToUpper(),
                         @outras_condic_03 = model.outras_condic_03?.ToUpper(),

                         // Situação de Rua
                         @tempo_situacao_rua = model.tempo_situacao_rua,
                         @vezes_alimenta = model.vezes_alimenta,
                         @outra_instituicao = model.outra_instituicao?.ToUpper(),
                         @desc_instituicao = model.desc_instituicao?.ToUpper(),
                         @grau_parentesco = model.grau_parentesco?.ToUpper(),
                         @visita_familiar = model.visita_familiar?.ToUpper(),
                         @sit_rua_beneficio = model.sit_rua_beneficio?.ToUpper(),
                         @sit_rua_familiar = model.sit_rua_familiar?.ToUpper(),
                         @banho = model.banho?.ToUpper(),
                         @higiene_bucal = model.higiene_bucal?.ToUpper(),
                         @acesso_sanit = model.acesso_sanit?.ToUpper(),
                         @higiene_outros = model.higiene_outros?.ToUpper(),
                         @doac_restaurante = model.doac_restaurante?.ToUpper(),
                         @restaurante_popu = model.restaurante_popu?.ToUpper(),
                         @doac_grup_relig = model.doac_grup_relig?.ToUpper(),
                         @doacao_popular = model.doacao_popular?.ToUpper(),
                         @doacao_outros = model.doacao_outros?.ToUpper(),
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Individuo GetById(string ibge, int id)
        {
            try
            {
                var individuo = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<Individuo>(_indidividuoCommand.GetById, new { @csi_codpac = id }));

                return individuo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetUltimoLog(string ibge, int id_individuo)
        {
            try
            {
                var log = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault(_indidividuoCommand.GetUltimoLog, new { id_individuo = id_individuo }));

                return new
                {
                    id = log.ID,
                    usuario = new
                    {
                        nome = log.NOME,
                        login = log.LOGIN,
                    },
                    data = log.DATA,
                    tipo = log.TIPO,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<bool, int?, string> CheckIndividuoExiste(string ibge, string filtro)
        {
            try
            {
                var individuo = Helpers.HelperConnection.ExecuteCommand(
                    ibge, conn =>
                        conn.QueryFirstOrDefault<dynamic>(
                            _indidividuoCommand.CheckIndividuoMesmoNome.Replace("@filtro", filtro)
                        )
                );

                if (individuo != null)
                    return new Tuple<bool, int?, string>(true, individuo.CSI_CODPAC, individuo.CSI_NOMPAC);
                else
                    return new Tuple<bool, int?, string>(false, null, string.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Individuo> CheckIndividuoMesmoNome(string ibge, string filtro)
        {
            try
            {
                var individuos = Helpers.HelperConnection.ExecuteCommand(
                    ibge, conn =>
                        conn.Query<Individuo>(
                            _indidividuoCommand.CheckIndividuoMesmoNome.Replace("@filtro", filtro)
                        ).ToList());

                return individuos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckVinculoMicroarea(string ibge, int id, string sql_estrutura)
        {
            try
            {
                var possuiVinculo = Helpers.HelperConnection.ExecuteCommand(
                    ibge, conn =>
                        conn.QueryFirstOrDefault<dynamic>(
                            _indidividuoCommand.CheckVinculoMicroarea.Replace("@sql_estrutura", sql_estrutura),
                            new { @csi_codpac = id }
                        )
                );

                if (possuiVinculo != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
