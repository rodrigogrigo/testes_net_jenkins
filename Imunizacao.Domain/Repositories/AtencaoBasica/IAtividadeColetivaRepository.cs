using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IAtividadeColetivaRepository
    {
        List<AtividadeColetivaViewModel> GetAll(string ibge);
        List<AtividadeColetivaViewModel> GetAllPagination(string ibge, string filtro, int page, int pagesize, int usuario);
        int GetCountAll(string ibge, string filtro, int usuario);
        AtividadeColetivaEditViewModel GetAtividadeColetivaById(string ibge, int id);
        void Insert(string ibge, AtividadeColetiva model);

        int GetNewId(string ibge);
        
        //JAKISON
        int GetNewIdPartic(string ibge);
        int GetNewIdProf(string ibge);
        //JAKISON

        void Update(string ibge, AtividadeColetiva model);
        void Excluir(string ibge, int id);

        List<ProfissionalAtivColetivaViewModel> GetProfissionalByAtividadeColetiva(string ibge, int id);

        List<PacienteAtivColetivaViewModel> GetPacienteByAtividadeColetiva(string ibge, int id);

        void DeleteProfissional(string ibge, int id);
        void DeleteParticipante(string ibge, int id);
        void DeleteProfissionalByAtividade(string ibge, int id);
        void DeleteParticipanteByAtividade(string ibge, int id);
    }
}
