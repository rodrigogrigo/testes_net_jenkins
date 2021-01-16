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
    public class ImunobiologicoRepository : IImunobiologicoRepository
    {
        private readonly IImunobiologicoCommand _imunobiologicocommand;
        public ImunobiologicoRepository(IImunobiologicoCommand commandText)
        {
            _imunobiologicocommand = commandText;
        }        

      
    }
}
