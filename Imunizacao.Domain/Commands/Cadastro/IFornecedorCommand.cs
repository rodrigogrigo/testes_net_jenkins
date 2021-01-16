namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IFornecedorCommand
    {
        string GetAll { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetById { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
        string ValidaExistenciaFornecedorCNPJ { get; }

        //Usado na tela de Vigencia da Atençaõ básica
        string GetPrestadoresVigencia { get; }
    }
}
