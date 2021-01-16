namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IProfissionalCommand
    {
        string GetAll { get; }
        string GetProfissionalByUnidade { get; }
        string GetProfissionalAtivoByUnidade { get; }
        string GetListaCBO { get; }
        string GetProfissionalCboByUnidade { get; }
        string GetProfissionalByUnidades { get; }
        string GetProfissionalByEquipe { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
        string GetProfissionalByIdAndUnidade { get; }

        string GetAllPaginationProfissionalWithCBO { get; }
        string GetCountAllProfissionalWithCBO { get; }

        string GetProfissionalByUnidadeWithCBO { get; }

        string GetProfissionalById { get; }
        string GetACSByEstabelecimentoSaude { get; }

        #region Agendamento de Consulta
        string GetCBOByMedicoUnidade { get; }
        #endregion
    }
}
