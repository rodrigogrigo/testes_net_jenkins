using Dapper;
using RgCidadao.Domain.Commands.Endemias;
using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.Repositories.Endemias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Endemias
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {
        private IEstabelecimentoCommand _command;
        public EstabelecimentoRepository(IEstabelecimentoCommand command)
        {
            _command = command;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_command.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Estabelecimento> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<Estabelecimento>(_command.GetAll).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Estabelecimento> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<Estabelecimento>(sql, new
                                    {
                                        @pagesize = pagesize,
                                        @page = page
                                    }).ToList());
                return lista;
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
                string sql = _command.GetCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.QueryFirstOrDefault<int>(sql));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Estabelecimento> GetEstabelecimentoByCiclo(string ibge, int id_ciclo)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.Query<Estabelecimento>(_command.GetEstabelecimentoByCiclo, new
                                  {
                                      @id_ciclo = id_ciclo
                                  }).ToList());
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
                                 conn.QueryFirstOrDefault<Estabelecimento>(_command.GetImovelById, new
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
                                  @razao_social_nome = model.razao_social_nome,
                                  @nome_fantasia_apelido = model.nome_fantasia_apelido,
                                  @cnpj_cpf = model.cnpj_cpf,
                                  @insc_estadual = model.insc_estadual,
                                  @insc_municipal = model.insc_municipal,
                                  @insc_produtor_rural = model.insc_produtor_rural,
                                  @natureza_juridica = model.natureza_juridica,
                                  @id_logradouro = model.id_logradouro,
                                  @numero_logradouro = model.numero_logradouro,
                                  @complemento_logradouro = model.complemento_logradouro,
                                  @telefone_fixo = model.telefone_fixo,
                                  @telefone_movel = model.telefone_movel,
                                  @fax = model.fax,
                                  @email = model.email,
                                  @website = model.website,
                                  @data_cadastro = DateTime.Now,
                                  @situacao_atual = model.situacao_atual,
                                  @data_situacao_atual = model.data_situacao_atual,
                                  @matriz_filial = model.matriz_filial,
                                  @cnpj_matriz = model.cnpj_matriz,
                                  @esfera_adm = model.esfera_adm,
                                  @natureza_organizacao = model.natureza_organizacao,
                                  @data_baixa_fechamento = model.data_baixa_fechamento,
                                  @descricao_cnae = model.descricao_cnae,
                                  @procedimento_cadastro = model.procedimento_cadastro,
                                  @temporario = model.temporario,
                                  @numero_visa = model.numero_visa,
                                  @cnes_estabelecimento = model.cnes_estabelecimento,
                                  @zona = model.zona,
                                  @metragem = model.metragem,
                                  @id_microarea = model.id_microarea,
                                  @num_prontuario_familiar = model.num_prontuario_familiar,
                                  @id_usuario = model.id_usuario,
                                  @ponto_referencia = model.ponto_referencia,
                                  @tipo_domicilio = model.tipo_domicilio,
                                  @qtd_comodos = model.qtd_comodos,
                                  @tipo_acesso_domic = model.tipo_acesso_domic,
                                  @mat_predominante = model.mat_predominante,
                                  @disponib_energia = model.disponib_energia,
                                  @abastecimento_agua = model.abastecimento_agua,
                                  @escoamento_sanita = model.escoamento_sanita,
                                  @latitude = model.latitude,
                                  @longitude = model.longitude,
                                  @qrcode = model.qrcode,
                                  @uuid = model.uuid,
                                  @uuid_alteracao = model.uuid_alteracao,
                                  @data_alteracao_serv = model.data_alteracao_serv,
                                  @flg_exportar_esus = model.flg_exportar_esus,
                                  @id_esus_exportacao_item = model.id_esus_exportacao_item,
                                  @tipo_imovel = model.tipo_imovel,
                                  @tipo_logradouro = model.tipo_logradouro,
                                  @cargo_resp_instit = model.cargo_resp_instit,
                                  @nome_resp_instit = model.nome_resp_instit,
                                  @outros_profi_instituicao = model.outros_profi_instituicao,
                                  @tel_resp_instit = model.tel_resp_instit,
                                  @id_profissional = model.id_profissional,
                                  @cns_resp_instit = model.cns_resp_instit,
                                  @nome_inst_permanencia = model.nome_inst_permanencia,
                                  @excluido = "F",
                                  @quarteirao_logradouro = model.quarteirao_logradouro,
                                  @sequencia_quarteirao = model.sequencia_quarteirao,
                                  @sequencia_numero = model.sequencia_numero
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
                                  @razao_social_nome = model.razao_social_nome,
                                  @nome_fantasia_apelido = model.nome_fantasia_apelido,
                                  @cnpj_cpf = model.cnpj_cpf,
                                  @insc_estadual = model.insc_estadual,
                                  @insc_municipal = model.insc_municipal,
                                  @insc_produtor_rural = model.insc_produtor_rural,
                                  @natureza_juridica = model.natureza_juridica,
                                  @id_logradouro = model.id_logradouro,
                                  @numero_logradouro = model.numero_logradouro,
                                  @complemento_logradouro = model.complemento_logradouro,
                                  @telefone_fixo = model.telefone_fixo,
                                  @telefone_movel = model.telefone_movel,
                                  @fax = model.fax,
                                  @email = model.email,
                                  @website = model.website,
                                  @data_cadastro = DateTime.Now,
                                  @situacao_atual = model.situacao_atual,
                                  @data_situacao_atual = model.data_situacao_atual,
                                  @matriz_filial = model.matriz_filial,
                                  @cnpj_matriz = model.cnpj_matriz,
                                  @esfera_adm = model.esfera_adm,
                                  @natureza_organizacao = model.natureza_organizacao,
                                  @data_baixa_fechamento = model.data_baixa_fechamento,
                                  @descricao_cnae = model.descricao_cnae,
                                  @procedimento_cadastro = model.procedimento_cadastro,
                                  @temporario = model.temporario,
                                  @numero_visa = model.numero_visa,
                                  @cnes_estabelecimento = model.cnes_estabelecimento,
                                  @zona = model.zona,
                                  @metragem = model.metragem,
                                  @id_microarea = model.id_microarea,
                                  @num_prontuario_familiar = model.num_prontuario_familiar,
                                  @id_usuario = model.id_usuario,
                                  @ponto_referencia = model.ponto_referencia,
                                  @tipo_domicilio = model.tipo_domicilio,
                                  @qtd_comodos = model.qtd_comodos,
                                  @tipo_acesso_domic = model.tipo_acesso_domic,
                                  @mat_predominante = model.mat_predominante,
                                  @disponib_energia = model.disponib_energia,
                                  @abastecimento_agua = model.abastecimento_agua,
                                  @escoamento_sanita = model.escoamento_sanita,
                                  @latitude = model.latitude,
                                  @longitude = model.longitude,
                                  @qrcode = model.qrcode,
                                  @uuid = model.uuid,
                                  @uuid_alteracao = model.uuid_alteracao,
                                  @data_alteracao_serv = model.data_alteracao_serv,
                                  @flg_exportar_esus = model.flg_exportar_esus,
                                  @id_esus_exportacao_item = model.id_esus_exportacao_item,
                                  @tipo_imovel = model.tipo_imovel,
                                  @tipo_logradouro = model.tipo_logradouro,
                                  @cargo_resp_instit = model.cargo_resp_instit,
                                  @nome_resp_instit = model.nome_resp_instit,
                                  @outros_profi_instituicao = model.outros_profi_instituicao,
                                  @tel_resp_instit = model.tel_resp_instit,
                                  @id_profissional = model.id_profissional,
                                  @cns_resp_instit = model.cns_resp_instit,
                                  @nome_inst_permanencia = model.nome_inst_permanencia,
                                  @quarteirao_logradouro = model.quarteirao_logradouro,
                                  @sequencia_quarteirao = model.sequencia_quarteirao,
                                  @sequencia_numero = model.sequencia_numero,
                                  @id = model.id,
                              }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
