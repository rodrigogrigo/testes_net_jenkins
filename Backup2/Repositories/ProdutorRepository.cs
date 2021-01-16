using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class ProdutorRepository : IProdutorRepository
    {
        public IProdutorCommand _command;
        public ProdutorRepository(IProdutorCommand command)
        {
            _command = command;
        }

        public List<Produtor> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<Produtor>(_command.GetAll)).ToList();
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
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<int?>(_command.GetNewId));
                if (id == null)
                    id = 1;
                return (int)id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Produtor model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.Insert, new
                        {
                            @id = model.id,
                            @nome = model.nome,
                            @abreviatura = model.abreviatura
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
