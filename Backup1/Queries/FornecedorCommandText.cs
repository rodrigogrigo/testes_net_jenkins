using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class FornecedorCommandText : IFornecedorCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) CSI_CODFOR, CSI_NOMFOR,
                                                    CSI_TELFOR, CSI_TIPFOR
                                                FROM TSI_CADFOR
                                                WHERE EXCLUIDO != 'T'
                                                    @filtro
                                                ORDER BY CSI_NOMFOR";

        string IFornecedorCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetAll = $@"SELECT CSI_CODFOR, CSI_NOMFOR
                                     FROM TSI_CADFOR
                                     WHERE EXCLUIDO != 'T'
                                     ORDER BY CSI_NOMFOR";

        string IFornecedorCommand.GetAll { get => sqlGetAll; }

        public string sqlGetCountAll = $@"SELECT count(*)
                                          FROM TSI_CADFOR
                                          WHERE EXCLUIDO != 'T'
                                                 @filtro";
        string IFornecedorCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetById = $@"SELECT CSI_NOMFAN, CSI_PESSOA, CSI_ENDFOR, CSI_BAIFOR, CSI_CEPFOR, CSI_TELFOR,
                                             CSI_TIPFOR, NUM_CNES, CSI_NOMFOR, CSI_CGCFOR, CSI_INSFOR, CSI_EMAFOR
                                     FROM TSI_CADFOR
                                     where CSI_CODFOR = @id";
        string IFornecedorCommand.GetById { get => sqlGetById; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_TSI_CADFOR, 1) AS VLR FROM RDB$DATABASE";

        string IFornecedorCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO TSI_CADFOR (CSI_CODFOR, CSI_NOMFAN, CSI_PESSOA, CSI_DATCAD, CSI_ENDFOR, CSI_BAIFOR, CSI_CEPFOR, CSI_TELFOR,
                                                             CSI_CGCFOR, CSI_INSFOR, CSI_NOMUSU, CSI_NOMFOR,
                                                             CSI_FABRICANTE, CSI_DATAINC, EXCLUIDO,
                                                             CSI_TIPFOR, NUM_CNES, CSI_EMAFOR)
                                     VALUES (@csi_codfor, @csi_nomfan, @csi_pessoa, @csi_datcad, @csi_endfor, @csi_baifor, @csi_cepfor, @csi_telfor,
                                             @csi_cgcfor, @csi_insfor, @csi_nomusu, @csi_nomfor,
                                             @csi_fabricante, @csi_datainc, @excluido,
                                             @csi_tipfor, @num_cnes, @csi_emafor)";
        string IFornecedorCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE TSI_CADFOR
                                     SET CSI_NOMFAN = @csi_nomfan,
                                         CSI_PESSOA = @csi_pessoa,
                                         CSI_ENDFOR = @csi_endfor,
                                         CSI_BAIFOR = @csi_baifor,
                                         CSI_CEPFOR = @csi_cepfor,
                                         CSI_TELFOR = @csi_telfor,
                                         CSI_CGCFOR = @csi_cgcfor,
                                         CSI_INSFOR = @csi_insfor,
                                         CSI_NOMUSU = @csi_nomusu,
                                         CSI_NOMFOR = @csi_nomfor,
                                         CSI_FABRICANTE = @csi_fabricante,
                                         CSI_DATAALT = @csi_dataalt,
                                         CSI_TIPFOR = @csi_tipfor,
                                         NUM_CNES = @num_cnes,
                                         CSI_EMAFOR = @csi_emafor
                                     WHERE CSI_CODFOR = @csi_codfor";

        string IFornecedorCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"UPDATE TSI_CADFOR
                                     SET EXCLUIDO = 'T',
                                         CSI_DATAEXC = @csi_dataexc
                                     WHERE CSI_CODFOR = @csi_codfor";

        string IFornecedorCommand.Delete { get => sqlDelete; }
    }
}
