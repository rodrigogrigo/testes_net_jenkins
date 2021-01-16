namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface ICidadaoCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }

        string GetAllPaginationWithFamilia { get; }
        string GetCountAllWithFamilia { get; }

        string GetNewId { get; }
        string GetCidadaoById { get; }
        string ValidaExistenciaCidadao { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
        string GetFotoByCidadao { get; }
        string GetGrauParentesco { get; }
        string InsertCadIndividual { get; }
        string UpdateCadIndividual { get; }

        string GetLoginCadSus { get; }

        string GetIndividuosToFamilia { get; }
        string GetCountIndividuosToFamilia { get; }

        string GetCidadaoByIdProntuarioIdAgente { get; }


        string VerificaExisteEsusFamilia { get; }
    }
}
