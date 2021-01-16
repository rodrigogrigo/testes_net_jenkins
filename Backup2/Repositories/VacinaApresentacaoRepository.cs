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
    public class VacinaApresentacaoRepository : IVacinaApresentacaoRepository
    {
        private readonly IVacinaApresentacaoCommand _vacinaapresentacaocommand;

        public VacinaApresentacaoRepository(IVacinaApresentacaoCommand commandText)
        {
            _vacinaapresentacaocommand = commandText;
        }

        public List<VacinaApresentacao> GetAll(string ibge, string filtro)
        {
            try
            {
                var vacina = new List<VacinaApresentacao>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    vacina = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.Query<VacinaApresentacao>(_vacinaapresentacaocommand.GetAll.Replace("@filtro", "")).ToList());
                }
                else
                {
                    vacina = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<VacinaApresentacao>(_vacinaapresentacaocommand.GetAll.Replace("@filtro", $" {filtro} ")).ToList());
                }

                return vacina;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetId(string ibge)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.QueryFirstOrDefault<int>(_vacinaapresentacaocommand.GetIdVacinaApresentacao));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirVacinaApresentacao(string ibge, int id, string descricao, int quantidade)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.Execute(_vacinaapresentacaocommand.GetInsertVacinaApresentacao, new { @id = id, @descricao = descricao, @quantidade = quantidade }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarVacinaApresentacao(string ibge, int id, string descricao, int quantidade)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.Execute(_vacinaapresentacaocommand.GetAtualizaVacinaApresentacao, new { @descricao = descricao, @quantidade = quantidade, @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirVacinaApresentacao(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.Execute(_vacinaapresentacaocommand.GetExcluirVacinaApresentacao, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VacinaApresentacao GetById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<VacinaApresentacao>(_vacinaapresentacaocommand.GetById, new { @id = id }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
