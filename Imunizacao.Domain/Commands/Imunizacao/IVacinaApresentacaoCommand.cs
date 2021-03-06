﻿namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IVacinaApresentacaoCommand
    {
        string GetAll { get; }
        string GetById { get; }
        string GetIdVacinaApresentacao { get; }
        string GetInsertVacinaApresentacao { get; }
        string GetAtualizaVacinaApresentacao { get; }
        string GetExcluirVacinaApresentacao { get; }
    }
}
