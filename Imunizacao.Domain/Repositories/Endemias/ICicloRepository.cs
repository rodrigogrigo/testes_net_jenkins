using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.ViewModels.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Endemias
{
    public interface ICicloRepository
    {
        int GetCountAll(string ibge, string filtro);
        List<Ciclo> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        int GetNewId(string ibge);
        void Insert(string ibge, Ciclo model);
        Ciclo GetCicloById(string ibge, int id);
        void Update(string ibge, Ciclo model);
        void Excluir(string ibge, int id);
        List<Ciclo> GetAllCiclosAtivos(string ibge);
        List<CiclosUtilizadosViewModel> GetciclosUtilizados(string ibge);
        List<Ciclo> ValidaExistenciaCicloPeriodo(string ibge, DateTime? datainicial, DateTime? datafinal);
        List<Ciclo> GetCicloByData(string ibge, DateTime? data);
        void InserirLogCiclo(string ibge, CicloLog model);
        List<CicloLog> GetLogCicloByCiclo(string ibge, int id_ciclo);
        List<Ciclo> GetAll(string ibge);

        int CountVisitasCiclo(string ibge, int id_ciclo, DateTime? data_inicial, DateTime? data_final);
        DateTime? GetDataMaximaCiclo(string ibge, int id_ciclo);
        DateTime? GetDataMinimaCiclo(string ibge, int id_ciclo);
    }
}
