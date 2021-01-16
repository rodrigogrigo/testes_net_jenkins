namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class AcertoEstoque
    {
        public int? ID { get; set; }
        public int? ANO_APURACAO { get; set; }
        public int? MES_APURACAO { get; set; }
        public int? ID_UNIDADE { get; set; }
        public string LOTE { get; set; }
        public int? ID_PRODUTO { get; set; }
        public int? ID_PRODUTOR { get; set; }
        public int? ID_APRESENTACAO { get; set; }
        public string TIPO_LANCAMENTO { get; set; }
        public int? QTDE { get; set; }
        public int? ID_USUARIO { get; set; }
        public string DATA { get; set; }
        public int? ID_FORNECEDOR { get; set; }
        public string OBSERVACAO { get; set; }

    }
}
