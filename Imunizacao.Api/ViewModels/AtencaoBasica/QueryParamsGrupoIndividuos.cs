namespace RgCidadao.Api.ViewModels.AtencaoBasica
{
    public class QueryParamsGrupoIndividuos
    {
        public string ids_equipes { get; set; }
        public int? competencia { get; set; }
        public int page { get; set; }
        public int page_size { get; set; }

        //diabeticos visitados - 0; diabeticos não visitados - 1; ausentes/recusados - 2; todos - sem valor
        public int? status { get; set; }
    }
}
