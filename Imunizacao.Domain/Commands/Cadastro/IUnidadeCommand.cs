namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IUnidadeCommand
    {
        string GetAll { get; }
        string GetUnidadesByUser { get; }

        #region Agendamento de Consultas
        string GetLocaisAtendimentoByUnidade { get; }
        #endregion
    }
}
