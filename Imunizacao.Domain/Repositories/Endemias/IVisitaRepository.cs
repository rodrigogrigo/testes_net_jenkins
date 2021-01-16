using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.ViewModels.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Endemias
{
    public interface IVisitaRepository
    {
        int GetCountAll(string ibge, string filtro);
        List<Visita> GetAllPagination(string ibge, int page, int pagesize, string filtro);

        int GetNewIdVisita(string ibge);
        void InsertVisita(string ibge, Visita model); //ver se model ta certo
        int GetNewIdColeta(string ibge);
        void InsertColeta(string ibge, Coleta model);
        Visita GetVisitaById(string ibge, int? id);
        List<Coleta> GetColetaByVisita(string ibge, int visita);
        void UpdateVisita(string ibge, Visita model);
        void UpdateColeta(string ibge, Coleta model);
        void DeleteVisita(string ibge, int? visita);
        void DeleteColeta(string ibge, int? coleta);
        Visita GetVisitaByEstabelecimento(string ibge, DateTime? datainicial, DateTime? datafinal, int? estabelecimento, int? id_ciclo);
        List<VisitaBairroViewModel> GetQuarteiraoEstabelecimentoByBairro(string ibge, int bairro, int id_ciclo, string filtro);
        List<Visita> GetVisitasByCiclo(string ibge, int id);
    }
}
