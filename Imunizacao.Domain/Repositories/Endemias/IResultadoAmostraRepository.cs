using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.ViewModels.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Endemias
{
    public interface IResultadoAmostraRepository
    {
        List<ResultadoAmostraViewModel> GetAllPagination(string ibge, int? page, int? pagesize, string filtro, string filtroAmostra);
        int GetCountResultadoAmostra(string ibge, string filtro);
        List<ResultadoColetaViewModel> GetResultadoColetaByProfissional(string ibge, int? id_profissional);
        void UpdateColetaResultado(string ibge, ColetaResultado model);
        void DeleteColetaResultado(string ibge, int id);
        List<ColetaResultado> GetColetaResultadoByColeta(string ibge, int coleta);
        void DeleteColetaResultadoByColeta(string ibge, int coleta);
        int GetNewIdResultadoAmostra(string ibge);
        void InsertColetaResultado(string ibge, ColetaResultado model);
        List<ResultadoColetaViewModel> GetResultadoColetaByVisita(string ibge, int? id_visita);
        List<Coleta> GetResultadoAmostraByVisita(string ibge, int? id_visita);
        List<Coleta> GetResultadoAmostraByProfissional(string ibge, int id_profissional, int? id_ciclo, DateTime? data_inicial, DateTime? data_final);
    }
}
