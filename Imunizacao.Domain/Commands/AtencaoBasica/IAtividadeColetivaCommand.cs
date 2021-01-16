using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IAtividadeColetivaCommand
    {
        string GetAll { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetAtividadeColetivaById { get; }
        string Insert { get; }
        string Update { get; }
        string GetNewId { get; }
        string Delete { get; }

        //JAKISON
        string GetNewIdProf { get; }
        string InsertProfissionalParticipante { get; }

        string GetNewIdPartic { get; }
        string InsertPessoaParticipante { get; }
        //JAKISON

        string GetProfissionalByAtividadeColetiva { get; }
        string GetPacienteByAtividadeColetiva { get; }

        string DeleteProfissional { get; }
        string DeleteParticipante { get; }
        string DeleteProfissionalByAtividade { get; }
        string DeleteParticipanteByAtividade { get; }
    }
}
