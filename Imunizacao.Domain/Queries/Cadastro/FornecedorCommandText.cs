using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class FornecedorCommandText : IFornecedorCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) CSI_CODFOR, CSI_NOMFOR,
                                                    CSI_TELFOR, CSI_TIPFOR, CSI_CGCFOR
                                                FROM TSI_CADFOR
                                                WHERE EXCLUIDO != 'T'
                                                    @filtro
                                                ORDER BY CSI_NOMFOR";

        string IFornecedorCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetAll = $@"SELECT CSI_CODFOR, CSI_NOMFOR, CSI_CGCFOR
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

        public string sqlValidaExistenciaFornecedorCNPJ = $@"SELECT * FROM TSI_CADFOR F
                                                             WHERE F.CSI_CGCFOR = @cpfcnpj";
        string IFornecedorCommand.ValidaExistenciaFornecedorCNPJ { get => sqlValidaExistenciaFornecedorCNPJ; }

        public string sqlGetPrestadoresVigencia = $@"SELECT DISTINCT F.CSI_CODFOR CODIGO, F.CSI_NOMFOR NOME,
                                                           F.CSI_TELFOR TELEFONE, COALESCE(CAST((SELECT SUM(I.CSI_QTDE * I.CSI_VALOR)
                                                                                        FROM TSI_VIGENCIAGRUPO G
                                                                                        INNER JOIN TSI_VIGENCIATABPREEXAME E ON (E.CSI_CODVIG = G.CSI_CODVIG)
                                                                                        INNER JOIN TSI_ILIBEXAMES I ON (I.CSI_CODVIG = E.CSI_CODVIG)
                                                                                        WHERE I.CSI_CODPRESTADOR = F.CSI_CODFOR AND
                                                                                              E.CSI_CODUNI = @cod_uni AND
                                                                                              E.CSI_DATAINI <= @data_ini AND E.CSI_DATAFIN >= @data_fim) AS NUMERIC(18,2)),0) LIBERADO,
                                                          (SELECT FIRST (1) CSI_ID_CONSORCIO || ' - ' || CSI_NOMFOR
                                                           FROM TSI_CADFOR
                                                           WHERE CSI_TIPFOR = 'Consórcio' AND
                                                           CSI_ID_CONSORCIO = F.CSI_ID_CONSORCIO AND
                                                           CSI_ID_PRESTADOR_CONSORCIO IS NULL)  CONSORCIO,
                                                          C.CSI_NOMCID||' - '||C.CSI_SIGEST MUNICIPIO, CSI_TIPFOR TIPO
                                                     FROM TSI_VIGENCIATABPREEXAME VE
                                                     INNER JOIN TSI_VIGENCIA_FOR_PROC V ON (V.CSI_CODVIG = VE.CSI_CODVIG)
                                                     INNER JOIN TSI_CADFOR F ON (F.CSI_CODFOR = V.CSI_CODFOR)
                                                     INNER JOIN TSI_MEDICOS_FORNECEDOR MF ON (MF.CSI_CODFOR = F.CSI_CODFOR)
                                                     LEFT JOIN TSI_CIDADE C ON (C.CSI_CODCID = F.CSI_CODCID)
                                                     WHERE MF.CSI_CODMED = @cod_med
                                                     AND F.CSI_TIPFOR = 'Prestador do Consórcio'
                                                     AND F.CSI_ID_CONSORCIO IS NOT NULL
                                                     AND F.CSI_ID_PRESTADOR_CONSORCIO IS NOT NULL
                                                     AND VE.CSI_DATAINI <= @data_ini
                                                     AND VE.CSI_DATAFIN >= @data_fim
                                                     AND VE.CSI_CODUNI = @cod_uni";
        string IFornecedorCommand.GetPrestadoresVigencia { get => sqlGetPrestadoresVigencia; }
    }
}
