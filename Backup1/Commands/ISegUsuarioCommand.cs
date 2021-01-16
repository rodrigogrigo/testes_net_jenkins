namespace Imunizacao.Domain.Commands
{
    public interface ISegUsuarioCommand
    {
        string GetSegUsuarioById { get; }
        string GetLogin { get; }
        string GetTelefoneByUser { get; }
        string AtualizarSenhaProvisoria { get; }
        string AtualizarInfoUsuario { get; }
        string GetConfigUsuario { get; }
        string GetNewIdConfiguration { get; }
        string InsertOrUpdateConfigUsuario { get; }

        string sqlTestePerformance { get; }

    }
}
