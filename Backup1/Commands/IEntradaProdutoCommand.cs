﻿namespace Imunizacao.Domain.Commands
{
    public interface IEntradaProdutoCommand
    {
        string GetEntradaById { get; }
        string GetNewId { get; }
        string InsertOrUpdate { get; }
        string Delete { get; }
        string AtualizaValor { get; }
        string GetCountEntradaVacina { get; }
        string GetEntradaVacinaByUnidade { get; }
    }
}
