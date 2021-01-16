namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface ISegUsuarioCommand
    {
        string GetSegUsuarioById { get; }
        string GetLogin { get; }
        string GetTelefoneByUser { get; }
        string AtualizarSenhaProvisoria { get; }
        string AtualizarInfoUsuario { get; }
        string Insert { get; }
        string Update { get; }
        string GetNewId { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }

        string GetConfigUsuario { get; }
        string GetNewIdConfiguration { get; }
        string InsertOrUpdateConfigUsuario { get; }
        string GetPermissaoUser { get; }

        string sqlTestePerformance { get; }

        string GetReportCabecalho { get; }
        string GetReportCabecalhoHorizontal { get; }

        string GetTipoUsuarioById { get; }


    }
}
