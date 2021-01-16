using RgCidadao.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IEstadoRepository
    {
        List<Estado> GetAll(string ibge);
        Estado GetEstadoById(string ibge, int id);
    }
}
