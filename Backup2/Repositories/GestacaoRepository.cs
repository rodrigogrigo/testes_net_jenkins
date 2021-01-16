using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class GestacaoRepository : IGestacaoRepository
    {
        public IGestacaoCommand _command;
        public GestacaoRepository(IGestacaoCommand command)
        {
            _command = command;
        }

        public Gestacao IsGestante(string ibge, int id)
        {
            try
            {
                var gestacao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<Gestacao>(_command.IsGestante, new { @id = id }));

                return gestacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Gestacao> GetGestacaoByCidadao(string ibge, int id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Gestacao>(_command.GetGestacaoByCidadao, new { @id_cidadao = id })).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Gestacao_Item> GetGestacaoItensByGestacao(string ibge, int id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Gestacao_Item>(_command.GetGestacaoItensByGestacao, new { @id_gestacao = id })).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Gestacao GetUltimaGestacao(string ibge, int id)
        {
            try
            {
                //busca gestação
                var gestacao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<Gestacao>(_command.GetGestacaoByCidadao, new { @id_cidadao = id }));
                if (gestacao != null)
                {
                    var gestacaoitem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Gestacao_Item>(_command.GetUltimaGestacaoItem, new { @id_gestacao = gestacao.id }).ToList());

                    var item = gestacaoitem.OrderByDescending(x => x.dum).Take(1).FirstOrDefault();
                    gestacao.item = item;
                }

                return gestacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
