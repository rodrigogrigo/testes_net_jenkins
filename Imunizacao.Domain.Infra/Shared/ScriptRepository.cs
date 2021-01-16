using RgCidadao.Domain.Infra.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Shared
{
    public static class ScriptRepository
    {
        public static List<ScriptViewModel> GetScript(int ultimoscript)
        {
            try
            {
                var itens = new List<ScriptViewModel>();

                //PNI_CALENDARIO_BASICO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE PNI_CALENDARIO_BASICO (
                                  ID D_INTEGER NOT NULL,
                                  ID_FAIXA_ETARIA D_INTEGER NOT NULL,
                                  ID_PRODUTO D_INTEGER NOT NULL,
                                  ID_DOSE D_INTEGER NOT NULL,
                                  PUBLICO_ALVO D_INTEGER NOT NULL);

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD CONSTRAINT PK_PNI_CALENDARIO_BASICO
                                PRIMARY KEY (ID);

                                COMMENT ON COLUMN PNI_CALENDARIO_BASICO.PUBLICO_ALVO IS
                                '1=POPULACAO EM GERAL|2=GESTANTE|3=MULHER|4=HOMEM|5=PUERPERA|6=DEFICIENTE';

                                CREATE SEQUENCE GEN_PNI_CALENDARIO_BASICO_ID;

                                SET TERM ^ ;
                                CREATE TRIGGER PNI_CALENDARIO_BASICO_BI FOR PNI_CALENDARIO_BASICO
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_CALENDARIO_BASICO_ID,1);
                                END^
                                SET TERM ; ^

                                ALTER TABLE PNI_CALENDARIO_BASICO ALTER COLUMN PUBLICO_ALVO
                                SET DEFAULT 1;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD CONSTRAINT FK_PNI_CALENDARIO_BASICO_PROD
                                FOREIGN KEY (ID_PRODUTO)
                                REFERENCES PNI_PRODUTO(ID)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD CONSTRAINT FK_PNI_CALENDARIO_BASICO_DOSE
                                FOREIGN KEY (ID_DOSE)
                                REFERENCES PNI_DOSE(ID)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD ID_ESTRATEGIA INTEGER NOT NULL;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD CONSTRAINT FK_PNI_CALENDARIO_BASICO_ESTR
                                FOREIGN KEY (ID_ESTRATEGIA)
                                REFERENCES PNI_ESTRATEGIA(ID)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD DIAS_ANTES_APRAZAMENTO INTEGER;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD FLG_INATIVO SMALLINT;

                                ALTER TABLE PNI_CALENDARIO_BASICO ALTER COLUMN FLG_INATIVO
                                SET DEFAULT 0;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD VIGENCIA_INICIO DATE;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD VIGENCIA_FIM DATE;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD FLG_EXCLUIR_APRAZAMENTO SMALLINT;

                                COMMENT ON COLUMN PNI_CALENDARIO_BASICO.FLG_EXCLUIR_APRAZAMENTO IS
                                'Define se registros de aprazamento (PNI_APRAZAMENTO), vinculados ao registro de calendário básico podem ser excluídos.
                                0 = NÃO
                                1 = SIM';

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD IDADE_MINIMA D_NUMERICO;

                                ALTER TABLE PNI_CALENDARIO_BASICO
                                ADD IDADE_MAXIMA D_NUMERICO;

                                COMMENT ON COLUMN PNI_CALENDARIO_BASICO.IDADE_MINIMA IS
                                'Parte inteira representa o ano e a parte decimal representa os meses
                                Ex.: 3 anos e 11 meses 3,11';

                                ALTER TABLE PNI_CALENDARIO_BASICO DROP ID_FAIXA_ETARIA;",
                    codigo = 3
                });

                //PNI_APRAZAMENTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE PNI_APRAZAMENTO (
                                  ID D_INTEGER NOT NULL,
                                  ID_INDIVIDUO D_INTEGER NOT NULL,
                                  ID_CALENDARIO_BASICO D_INTEGER,
                                  DATA_LIMITE D_DATA_DATE,
                                  ID_VACINADOS D_INTEGER);

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD CONSTRAINT PK_PNI_APRAZAMENTO
                                PRIMARY KEY (ID);

                                CREATE SEQUENCE GEN_PNI_APRAZAMENTO_ID;

                                SET TERM ^ ;
                                CREATE TRIGGER PNI_APRAZAMENTO_BI FOR PNI_APRAZAMENTO
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_APRAZAMENTO_ID,1);
                                END^
                                SET TERM ; ^

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD CONSTRAINT FK_PNI_APRAZAMENTO_CALENDARIO
                                FOREIGN KEY (ID_CALENDARIO_BASICO)
                                REFERENCES PNI_CALENDARIO_BASICO(ID)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD CONSTRAINT FK_PNI_APRAZAMENTO_INDIVIDUO
                                FOREIGN KEY (ID_INDIVIDUO)
                                REFERENCES TSI_CADPAC(CSI_CODPAC)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD CONSTRAINT FK_PNI_APRAZAMENTO_VACINADOS
                                FOREIGN KEY (ID_VACINADOS)
                                REFERENCES PNI_VACINADOS(ID)
                                ON DELETE SET NULL
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD ID_GESTACAO_ITEM INTEGER;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD CONSTRAINT FK_PNI_APRAZAMENTO_GESTA_ITE
                                FOREIGN KEY (ID_GESTACAO_ITEM)
                                REFERENCES GESTACAO_ITEM(ID)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD ID_DOSE INTEGER NOT NULL;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD CONSTRAINT FK_PNI_APRAZAMENTO_DOSE
                                FOREIGN KEY (ID_DOSE)
                                REFERENCES PNI_DOSE(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD ID_PRODUTO INTEGER NOT NULL;

                                ALTER TABLE PNI_APRAZAMENTO
                                ADD CONSTRAINT FK_PNI_APRAZAMENTO_PRODUTO
                                FOREIGN KEY (ID_PRODUTO)
                                REFERENCES PNI_PRODUTO(ID)
                                ON UPDATE CASCADE;
                                ",
                    codigo = 4
                });

                //PNI_ACERTO_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE PNI_ACERTO_ESTOQUE (
                                  ID               INTEGER NOT NULL,
                                  ANO_APURACAO     INTEGER NOT NULL,
                                  MES_APURACAO     INTEGER NOT NULL,
                                  ID_UNIDADE       INTEGER NOT NULL,
                                  LOTE             VARCHAR(30) NOT NULL,
                                  ID_PRODUTO       INTEGER NOT NULL,
                                  ID_PRODUTOR      INTEGER NOT NULL,
                                  ID_APRESENTACAO  INTEGER NOT NULL,
                                  TIPO_LANCAMENTO  VARCHAR(5) NOT NULL,
                                  QTDE             INTEGER NOT NULL,
                                  ID_USUARIO       INTEGER NOT NULL,
                                  DATA             D_DATE NOT NULL /* D_DATE = TIMESTAMP */,
                                  ID_FORNECEDOR    D_INTEGER /* D_INTEGER = INTEGER */,
                                  OBSERVACAO       D_BLOB_TEXT_256 /* D_BLOB_TEXT_256 = BLOB SUB_TYPE 1 SEGMENT SIZE 256 */
                                );

                                ALTER TABLE PNI_ACERTO_ESTOQUE ADD CONSTRAINT PK_PNI_ACERTO_ESTOQUE PRIMARY KEY (ID);
                                ALTER TABLE PNI_ACERTO_ESTOQUE ADD CONSTRAINT FK_PNI_ACERTO_EST_APRES FOREIGN KEY (ID_APRESENTACAO) REFERENCES PNI_APRESENTACAO (ID) ON UPDATE CASCADE;
                                ALTER TABLE PNI_ACERTO_ESTOQUE ADD CONSTRAINT FK_PNI_ACERTO_EST_FOR FOREIGN KEY (ID_FORNECEDOR) REFERENCES TSI_CADFOR (CSI_CODFOR) ON UPDATE CASCADE;
                                ALTER TABLE PNI_ACERTO_ESTOQUE ADD CONSTRAINT FK_PNI_ACERTO_EST_PRODUTO FOREIGN KEY (ID_PRODUTO) REFERENCES PNI_PRODUTO (ID) ON UPDATE CASCADE;
                                ALTER TABLE PNI_ACERTO_ESTOQUE ADD CONSTRAINT FK_PNI_ACERTO_EST_PRODUTOR FOREIGN KEY (ID_PRODUTOR) REFERENCES PNI_PRODUTOR (ID) ON UPDATE CASCADE;
                                ALTER TABLE PNI_ACERTO_ESTOQUE ADD CONSTRAINT FK_PNI_ACERTO_EST_UNIDADE FOREIGN KEY (ID_UNIDADE) REFERENCES TSI_UNIDADE (CSI_CODUNI) ON UPDATE CASCADE;
                                ALTER TABLE PNI_ACERTO_ESTOQUE ADD CONSTRAINT FK_PNI_ACERTO_EST_USUARIO FOREIGN KEY (ID_USUARIO) REFERENCES SEG_USUARIO (ID) ON UPDATE CASCADE;

                                ALTER TABLE PNI_ACERTO_ESTOQUE
                                ADD ID_UNIDADE_ENVIO INTEGER;

                                ALTER TABLE PNI_ACERTO_ESTOQUE
                                ADD CONSTRAINT FK_PNI_ACERTO_ESTOQUE_1
                                FOREIGN KEY (ID_UNIDADE_ENVIO)
                                REFERENCES TSI_UNIDADE(CSI_CODUNI)
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_ACERTO_ESTOQUE
                                ADD TIPO_PERCA SMALLINT;

                                COMMENT ON COLUMN PNI_ACERTO_ESTOQUE.TIPO_LANCAMENTO IS
                                '2=PERCA|3=DOACAO|4=ENVIO';

                                COMMENT ON COLUMN PNI_ACERTO_ESTOQUE.TIPO_PERCA IS
                                'Se TIPO_LANCAMENTO = 2 Perca. Esse campo deve ser preenchido com o tipo de perca
                                1 = Quebra | 2 = Falta de Energia | 3 = Falha Equipamento | 4 = Vencimento | 5 = Transporte | 6 = Outros Motivos';

                                ALTER TABLE PNI_ACERTO_ESTOQUE
                                ADD QTDE_FRASCOS SMALLINT;

                                CREATE SEQUENCE GEN_PNI_ACERTO_ESTOQUE_ID;",
                    codigo = 5
                });

                //PNI_LOCAL_APLICACAO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE GENERATOR GEN_PNI_LOCAL_APLICACAO_ID;

                                CREATE TABLE PNI_LOCAL_APLICACAO (
                                    ID    D_INTEGER NOT NULL /* D_INTEGER = INTEGER */,
                                    NOME  D_NOME NOT NULL /* D_NOME = VARCHAR(200) */
                                );

                                ALTER TABLE PNI_LOCAL_APLICACAO ADD CONSTRAINT PK_PNI_LOCAL_APLICACAO PRIMARY KEY (ID);

                                SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_LOCAL_APLICACAO_BI FOR PNI_LOCAL_APLICACAO
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_LOCAL_APLICACAO_ID,1);
                                 END^

                                 SET TERM ; ^",
                    codigo = 6
                });

                //PNI_VIA_ADM
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE GENERATOR GEN_PNI_VIA_ADM_ID;

                                CREATE TABLE PNI_VIA_ADM (
                                    ID    D_INTEGER NOT NULL /* D_INTEGER = INTEGER */,
                                    NOME  D_NOME NOT NULL /* D_NOME = VARCHAR(200) */
                                );

                                ALTER TABLE PNI_VIA_ADM ADD CONSTRAINT PK_PNI_VIA_ADM PRIMARY KEY (ID);

                                SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_VIA_ADM_BI FOR PNI_VIA_ADM
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_VIA_ADM_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 7
                });

                //PNI_LOCAL_VIA_ADM
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE GENERATOR GEN_PNI_LOCAL_VIA_ADM_ID;

                                CREATE TABLE PNI_LOCAL_VIA_ADM (
                                    ID          D_INTEGER NOT NULL /* D_INTEGER = INTEGER */,
                                    ID_LOCAL    D_INTEGER NOT NULL /* D_INTEGER = INTEGER */,
                                    ID_VIA_ADM  D_INTEGER NOT NULL /* D_INTEGER = INTEGER */
                                );

                                ALTER TABLE PNI_LOCAL_VIA_ADM ADD CONSTRAINT PK_PNI_LOCAL_VIA_ADM PRIMARY KEY (ID);

                                ALTER TABLE PNI_LOCAL_VIA_ADM ADD CONSTRAINT FK_PNI_LOCAL_VIA_ADM_LOCAL FOREIGN KEY (ID_LOCAL) REFERENCES PNI_LOCAL_APLICACAO (ID) ON DELETE CASCADE ON UPDATE CASCADE;
                                ALTER TABLE PNI_LOCAL_VIA_ADM ADD CONSTRAINT FK_PNI_LOCAL_VIA_ADM_VIA FOREIGN KEY (ID_VIA_ADM) REFERENCES PNI_VIA_ADM (ID) ON DELETE CASCADE ON UPDATE CASCADE;

                                SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_LOCAL_VIA_ADM_BI FOR PNI_LOCAL_VIA_ADM
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_LOCAL_VIA_ADM_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 8
                });

                //CONFIGURACAO_USUARIO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE CONFIGURACAO_USUARIO (
                                  ID INTEGER NOT NULL,
                                  ID_USUARIO INTEGER NOT NULL,
                                  ID_ULTIMA_UNIDADE INTEGER,
                                  QTDE_REGISTRO_TABELA SMALLINT,
                                  TIPO_MENU SMALLINT);

                                ALTER TABLE CONFIGURACAO_USUARIO
                                ADD CONSTRAINT PK_CONFIGURACAO_USUARIO
                                PRIMARY KEY (ID);

                                COMMENT ON COLUMN CONFIGURACAO_USUARIO.TIPO_MENU IS
                                '0 = Fechado | 1 = Aberto';

                                ALTER TABLE CONFIGURACAO_USUARIO
                                ADD CONSTRAINT FK_CONFIGURACAO_USUARIO
                                FOREIGN KEY (ID_USUARIO)
                                REFERENCES SEG_USUARIO(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE CONFIGURACAO_USUARIO
                                ADD CONSTRAINT FK_CONFIGURACAO_USUARIO_UNID
                                FOREIGN KEY (ID_ULTIMA_UNIDADE)
                                REFERENCES TSI_UNIDADE(CSI_CODUNI)
                                ON UPDATE CASCADE;

                                CREATE SEQUENCE GEN_CONFIGURACAO_USUARIO_ID;",
                    codigo = 9
                });

                //PNI_VACINADOS
                itens.Add(new ScriptViewModel()
                {
                    script = $@"DROP TRIGGER PNI_VACINADOS_AI0;

                                DROP TRIGGER PNI_VACINADOS_AU0;

                                DROP TRIGGER PNI_VACINADOS_AD0;

                                ALTER TABLE PNI_VACINADOS
                                ADD FLG_EXCLUIDO SMALLINT;

                                COMMENT ON COLUMN PNI_VACINADOS.FLG_EXCLUIDO IS
                                '0 = Nao | 1 = Sim';

                                ALTER TABLE PNI_VACINADOS
                                ADD ID_USUARIO_EXCLUSAO INTEGER;

                                ALTER TABLE PNI_VACINADOS
                                ADD CONSTRAINT FK_PNI_VACINADOS_ID_USUARIO
                                FOREIGN KEY (ID_USUARIO_EXCLUSAO)
                                REFERENCES SEG_USUARIO(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_VACINADOS
                                ADD OBSERVACAO VARCHAR(500);

                                ALTER TABLE PNI_VACINADOS
                                ADD ID_VIA_ADM INTEGER;

                                ALTER TABLE PNI_VACINADOS
                                ADD ID_LOCAL_APLICACAO INTEGER;

                                ALTER TABLE PNI_VACINADOS
                                ADD CONSTRAINT FK_PNI_VACINADOS_VIA_ADM
                                FOREIGN KEY (ID_VIA_ADM)
                                REFERENCES PNI_VIA_ADM(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_VACINADOS
                                ADD CONSTRAINT FK_PNI_VACINADOS_LOCAL_APLI
                                FOREIGN KEY (ID_LOCAL_APLICACAO)
                                REFERENCES PNI_LOCAL_APLICACAO(ID)
                                ON UPDATE CASCADE;",
                    codigo = 10
                });

                //PNI_ENTRADA_PRODUTO_ITEM
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM DROP CONSTRAINT FK_PNI_ENTRADA_PRODUTO_ITEM_PDT;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM DROP CONSTRAINT FK_PNI_ENTRADA_PRODUTO_ITEM_PDR;

                                DROP TRIGGER PNI_ENTRADA_PRODUTO_ITEM_AD0;

                                DROP TRIGGER PNI_ENTRADA_PRODUTO_ITEM_AI0;

                                DROP TRIGGER PNI_ENTRADA_PRODUTO_ITEM_AU0;

                                DROP TRIGGER PNI_ENTRADA_PRODUTO_ITEM_BI0;

                                DROP TRIGGER PNI_ENTRADA_PRODUTO_ITEM_BU0;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM DROP LOTE;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM DROP ID_PRODUTO;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM DROP ID_PRODUTOR;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM
                                ADD ID_LOTE INTEGER;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM
                                ADD CONSTRAINT FK_PNI_ENTRADA_PROD_ITEM_LOTE
                                FOREIGN KEY (ID_LOTE)
                                REFERENCES PNI_LOTE_PRODUTO(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM DROP CONSTRAINT FK_PNI_ENTRADA_PROD_ITEM_LOTE;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM
                                ADD CONSTRAINT FK_PNI_ENTRADA_PROD_ITEM_LOTE
                                FOREIGN KEY (ID_LOTE)
                                REFERENCES PNI_LOTE_PRODUTO(ID)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE
                                USING INDEX FK_PNI_ENTRADA_PROD_ITEM_LOTE;

                                ALTER TABLE PNI_ENTRADA_PRODUTO_ITEM
                                ADD QTDE_DOSES INTEGER;",
                    codigo = 11
                });

                //PNI_MOVIMENTO_PRODUTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"DROP TRIGGER PNI_MOVIMENTO_PRODUTO_AI0;

                                DROP TRIGGER PNI_MOVIMENTO_PRODUTO_AU0;

                                DROP TRIGGER PNI_MOVIMENTO_PRODUTO_AD0;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO DROP TIPO_LANCAMENTO;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO DROP CONSTRAINT FK_PNI_MOVIMENTO_PRODUTO_APRES;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO DROP ID_APRESENTACAO;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO
                                ADD TABELA_ORIGEM D_TEXT_035;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO
                                ADD ESTOQUE_ANTERIOR D_INTEGER;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO
                                ADD ID_ORIGEM D_INTEGER;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO
                                ADD TIPO_MOVIMENTO SMALLINT;

                                ALTER TABLE PNI_MOVIMENTO_PRODUTO
                                ADD OPERACAO D_INTEGER;

                                COMMENT ON COLUMN PNI_MOVIMENTO_PRODUTO.TIPO_MOVIMENTO IS 
                                '0 = Entrada | 1 = Vacinado | 2 = Perca | 3 = Doacao | 4 = Envio';

                                COMMENT ON COLUMN PNI_MOVIMENTO_PRODUTO.OPERACAO IS 
                                '0=ADD ESTOQUE|1=REMOVE ESTOQUE';

                                CREATE SEQUENCE GEN_PNI_MOVIMENTO_PRODUTO_ID;",
                    codigo = 12
                });

                //PNI_APRESENTACAO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE PNI_APRESENTACAO
                                ADD QUANTIDADE INTEGER;

                                CREATE SEQUENCE GEN_PNI_APRESENTACAO_ID;

                                ALTER SEQUENCE GEN_PNI_APRESENTACAO_ID RESTART WITH 50 INCREMENT BY 1;",
                    codigo = 13
                });

                //PNI_PRODUTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE PNI_PRODUTO
                                ADD ID_VIA_ADM INTEGER;

                                ALTER TABLE PNI_PRODUTO
                                ADD CONSTRAINT FK_PNI_PRODUTO_VIA_ADM
                                FOREIGN KEY (ID_VIA_ADM)
                                REFERENCES PNI_VIA_ADM(ID)
                                ON UPDATE CASCADE;",
                    codigo = 14
                });

                //PNI_LOTE_PRODUTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE PNI_LOTE_PRODUTO
                                ADD FLG_BLOQUEADO SMALLINT
                                DEFAULT 0 
                                NOT NULL;",
                    codigo = 15
                });

                //TSI_CADPAC
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE TSI_CADPAC
                                ADD CSI_COD_EQUIPE INTEGER;

                                ALTER TABLE TSI_CADPAC
                                ADD CONSTRAINT FK_TSI_CADPAC_COD_EQUIPE
                                FOREIGN KEY (CSI_COD_EQUIPE)
                                REFERENCES ESUS_EQUIPES(ID)
                                ON DELETE SET NULL
                                ON UPDATE CASCADE;",
                    codigo = 15
                });

                //CRIA PROCEDURE -> PNI_INSERE_APRAZAMENTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                create or alter procedure PNI_INSERE_APRAZAMENTO (
                                    IN_ID_INDIVIDUO integer,
                                    IN_PUBLICO_ALVO integer,
                                    IN_DATA_NASCIMENTO date,
                                    IN_ID_CALENDARIO_BASICO integer,
                                    IN_CAMPANHA integer)
                                as
                                declare variable VAR_DATA_NASCIMENTO date;
                                declare variable VAR_IDADE numeric(15,2);
                                declare variable VAR_ID_CALENDARIO_BASICO integer;
                                declare variable VAR_IDADE_MINIMA numeric(15,2);
                                declare variable VAR_ID_PRODUTO integer;
                                declare variable VAR_ID_DOSE integer;
                                declare variable VAR_DATA_REF date;
                                BEGIN
                                   SELECT B.ID, B.IDADE_MINIMA, B.ID_PRODUTO, B.ID_DOSE
                                   FROM PNI_CALENDARIO_BASICO B
                                   WHERE B.FLG_INATIVO = 0
                                   AND B.PUBLICO_ALVO = :IN_PUBLICO_ALVO
                                   AND B.ID = :IN_ID_CALENDARIO_BASICO
                                   INTO :VAR_ID_CALENDARIO_BASICO, :VAR_IDADE_MINIMA, :VAR_ID_PRODUTO, :VAR_ID_DOSE;

                                   VAR_DATA_REF = DATEADD(MONTH, SUBSTRING(SUBSTRING(CAST(:VAR_IDADE_MINIMA AS VARCHAR(6)) FROM (POSITION('.' IN CAST(:VAR_IDADE_MINIMA AS VARCHAR(6)))+1)) FROM 1 FOR 2),
                                           DATEADD(YEAR, CAST(:VAR_IDADE_MINIMA AS INTEGER),:IN_DATA_NASCIMENTO));

                                   IF (IN_CAMPANHA = 2) THEN
                                     VAR_DATA_REF = :IN_DATA_NASCIMENTO;


                                   UPDATE OR INSERT INTO PNI_APRAZAMENTO(ID_INDIVIDUO, ID_CALENDARIO_BASICO, ID_PRODUTO, ID_DOSE, DATA_LIMITE)
                                     VALUES(:IN_ID_INDIVIDUO, :VAR_ID_CALENDARIO_BASICO, :VAR_ID_PRODUTO, :VAR_ID_DOSE, :VAR_DATA_REF)
                                    MATCHING(ID_INDIVIDUO, ID_CALENDARIO_BASICO);
                                END^

                                SET TERM ; ^",
                    codigo = 17
                });

                //PROCEDURE: PNI_GERA_APRAZAMENTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                create or alter procedure PNI_GERA_APRAZAMENTO (
                                    P_ID_CIDADAO integer,
                                    P_ID_CALENDARIO integer,
                                    P_ID_PUBLICO_ALVO integer)
                                as
                                declare variable VAR_PUBLICO_ALVO integer;
                                declare variable VAR_ID_CIDADAO integer;
                                declare variable VAR_ID_CALENDARIO integer;
                                declare variable VAR_DATA_NASCIMENTO date;
                                declare variable VAR_DATA_NASCIMENTO_CRIANCA date;
                                declare variable VAR_IDADE_MINIMA numeric(15,2);
                                declare variable VAR_IDADE_MAXIMA numeric(15,2);
                                declare variable VAR_VIGENCIA_INICIO date;
                                declare variable VAR_VIGENCIA_FIM date;
                                declare variable VAR_DATA_REFERENCIA date;
                                declare variable VAR_SQL_CIDADAO varchar(5000);
                                declare variable VAR_SQL_CALENDARIO varchar(5000);
                                declare variable VAR_CAMPANHA integer; /* 1: NAO, 2: SIM */
                                BEGIN
                                  VAR_SQL_CALENDARIO = '';
                                  VAR_SQL_CIDADAO    = '';

                                  IF (:P_ID_PUBLICO_ALVO IS NOT NULL) THEN
                                  BEGIN
                                    IF (:P_ID_CALENDARIO IS NOT NULL) THEN -- Monta o SQL para listar apenas o calendario em questao
                                    BEGIN
                                      VAR_SQL_CALENDARIO ='SELECT CB.ID, CB.PUBLICO_ALVO, CB.IDADE_MINIMA, CB.IDADE_MAXIMA, CB.VIGENCIA_INICIO, CB.VIGENCIA_FIM '||
                                                          'FROM PNI_CALENDARIO_BASICO CB '||
                                                          'WHERE CB.FLG_INATIVO = 0 AND CB.ID = '||:P_ID_CALENDARIO;

                                    END
                                    ELSE BEGIN -- Monta o SQL para listar todos os calendarios de um determinado publico alvo

                                      VAR_SQL_CALENDARIO ='SELECT CB.ID, CB.PUBLICO_ALVO, CB.IDADE_MINIMA, CB.IDADE_MAXIMA, CB.VIGENCIA_INICIO, CB.VIGENCIA_FIM '||
                                                          'FROM PNI_CALENDARIO_BASICO CB '||
                                                           'WHERE CB.FLG_INATIVO = 0 AND CB.PUBLICO_ALVO = '||:P_ID_PUBLICO_ALVO;
                                    END


                                    IF (:P_ID_CIDADAO IS NOT NULL) THEN -- Monta o SQL para listar apenas o cidadao em questao
                                    BEGIN
                                      VAR_SQL_CIDADAO = 'SELECT CP.CSI_CODPAC, CP.CSI_DTNASC, NULL FROM TSI_CADPAC CP '||
                                                        'WHERE CP.CSI_DTNASC IS NOT NULL AND CP.CSI_CODPAC = '||:P_ID_CIDADAO;
                                    END
                                    ELSE BEGIN
                                      IF (:P_ID_PUBLICO_ALVO = 1) THEN -- Geral
                                      BEGIN
                                        VAR_SQL_CIDADAO = 'SELECT CP.CSI_CODPAC, CP.CSI_DTNASC, NULL '||
                                                          'FROM TSI_CADPAC CP '||
                                                          'WHERE CP.ID_ESUS_CADDOMICILIAR IS NOT NULL AND CP.CSI_DTNASC IS NOT NULL ';
                                      END
                                      ELSE IF (:P_ID_PUBLICO_ALVO = 2) THEN -- Gestante
                                      BEGIN
                                        VAR_SQL_CIDADAO = 'SELECT CP.CSI_CODPAC, GI.DUM, NULL '||
                                                          'FROM GESTACAO_ITEM GI '||
                                                          'JOIN GESTACAO G ON G.ID = GI.ID_GESTACAO '||
                                                          'JOIN TSI_CADPAC CP ON CP.CSI_CODPAC = G.ID_CIDADAO '||
                                                          'WHERE GI.FLG_DESFECHO = 0 AND CP.CSI_DTNASC IS NOT NULL ';
                                      END
                                      ELSE IF (:P_ID_PUBLICO_ALVO = 3) THEN -- Mulher
                                      BEGIN
                                        VAR_SQL_CIDADAO = 'SELECT CP.CSI_CODPAC, CP.CSI_DTNASC, NULL '||
                                                          'FROM TSI_CADPAC CP '||
                                                          'WHERE CP.ID_ESUS_CADDOMICILIAR IS NOT NULL AND CP.CSI_DTNASC IS NOT NULL '||
                                                          'AND CP.CSI_SEXPAC = ''Feminino'' ';
                                      END
                                      ELSE IF (:P_ID_PUBLICO_ALVO = 4) THEN -- Homem
                                      BEGIN
                                        VAR_SQL_CIDADAO = 'SELECT CP.CSI_CODPAC, CP.CSI_DTNASC, NULL '||
                                                          'FROM TSI_CADPAC CP '||
                                                          'WHERE CP.ID_ESUS_CADDOMICILIAR IS NOT NULL AND CP.CSI_DTNASC IS NOT NULL '||
                                                          'AND CP.CSI_SEXPAC = ''Masculino'' ';
                                      END
                                      ELSE IF (:P_ID_PUBLICO_ALVO = 5) THEN -- Puerpera
                                      BEGIN
                                        VAR_SQL_CIDADAO = 'SELECT CP.CSI_CODPAC, CP.CSI_DTNASC, GI.DATA_NASCIMENTO '||
                                                          'FROM GESTACAO_ITEM GI '||
                                                          'JOIN GESTACAO G ON G.ID = GI.ID_GESTACAO '||
                                                          'JOIN TSI_CADPAC CP ON CP.CSI_CODPAC = G.ID_CIDADAO '||
                                                          'WHERE GI.FLG_DESFECHO > 0 AND CP.CSI_DTNASC IS NOT NULL AND GI.DATA_NASCIMENTO IS NOT NULL ';
                                      END
                                      ELSE IF (:P_ID_PUBLICO_ALVO = 6) THEN -- Deficiente
                                      BEGIN
                                        VAR_SQL_CIDADAO = 'SELECT CP.CSI_CODPAC, CP.CSI_DTNASC, NULL '||
                                                          'FROM TSI_CADPAC CP '||
                                                          'WHERE CP.ID_ESUS_CADDOMICILIAR IS NOT NULL AND CP.CSI_DTNASC IS NOT NULL AND '||
                                                          'CP.VERIFICA_DEFICIENCIA = 1 ';
                                      END
                                    END
                                  END

                                  IF ((:VAR_SQL_CALENDARIO <> '') AND (:VAR_SQL_CIDADAO <> '')) THEN
                                  BEGIN
                                    FOR EXECUTE STATEMENT :VAR_SQL_CIDADAO INTO :VAR_ID_CIDADAO, :VAR_DATA_NASCIMENTO, :VAR_DATA_NASCIMENTO_CRIANCA
                                    DO
                                    BEGIN
                                      -- APAGANDO AS DOSES APRAZADAS PARA O INDIVIDUO DO PUBLICO ALVO A SER GERADO
                                      DELETE FROM PNI_APRAZAMENTO
                                      WHERE ID_INDIVIDUO = :VAR_ID_CIDADAO
                                      AND ID_VACINADOS IS NULL
                                      AND ID_CALENDARIO_BASICO IN(SELECT CB.ID FROM PNI_CALENDARIO_BASICO CB WHERE CB.PUBLICO_ALVO = :P_ID_PUBLICO_ALVO);

                                      FOR EXECUTE STATEMENT :VAR_SQL_CALENDARIO INTO :VAR_ID_CALENDARIO,:VAR_PUBLICO_ALVO, :VAR_IDADE_MINIMA, :VAR_IDADE_MAXIMA, :VAR_VIGENCIA_INICIO, :VAR_VIGENCIA_FIM
                                      DO
                                      BEGIN
                                        VAR_DATA_REFERENCIA = :VAR_DATA_NASCIMENTO;
                                        VAR_CAMPANHA = 1;

                                        IF (:VAR_VIGENCIA_INICIO IS NOT NULL) THEN
                                        BEGIN
                                          VAR_CAMPANHA = 2;

                                          IF (:P_ID_PUBLICO_ALVO = 5) THEN
                                          BEGIN
                                            IF (((:VAR_DATA_NASCIMENTO_CRIANCA >= :VAR_VIGENCIA_INICIO) AND (:VAR_DATA_NASCIMENTO_CRIANCA <= :VAR_VIGENCIA_FIM)) OR
                                               (((DATEADD (41 DAY TO :VAR_DATA_NASCIMENTO_CRIANCA)) >= :VAR_VIGENCIA_INICIO) AND (:VAR_DATA_NASCIMENTO_CRIANCA <= :VAR_VIGENCIA_FIM))) THEN
                                            BEGIN
                                              VAR_DATA_REFERENCIA = :VAR_VIGENCIA_INICIO;
                                            END
                                            ELSE
                                              VAR_DATA_REFERENCIA = NULL;
                                          END
                                          ELSE IF (((SELECT IDADE FROM PRO_CALCULA_IDADE(:VAR_DATA_NASCIMENTO, :VAR_VIGENCIA_INICIO)) BETWEEN :VAR_IDADE_MINIMA AND :VAR_IDADE_MAXIMA) OR
                                                  ((SELECT IDADE FROM PRO_CALCULA_IDADE(:VAR_DATA_NASCIMENTO, :VAR_VIGENCIA_FIM)) BETWEEN :VAR_IDADE_MINIMA AND :VAR_IDADE_MAXIMA)) THEN
                                          BEGIN
                                            VAR_DATA_REFERENCIA = :VAR_VIGENCIA_INICIO;
                                          END
                                          ELSE
                                            VAR_DATA_REFERENCIA = NULL;
                                        END

                                        IF (:VAR_DATA_REFERENCIA IS NOT NULL) THEN
                                          EXECUTE PROCEDURE PNI_INSERE_APRAZAMENTO(:VAR_ID_CIDADAO, :VAR_PUBLICO_ALVO, :VAR_DATA_REFERENCIA, :VAR_ID_CALENDARIO, :VAR_CAMPANHA);
                                      END
                                    END
                                   END
                                END^

                                SET TERM ; ^",
                    codigo = 18
                });

                //PROCEDURE: PNI_MOVIMENTA_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                create or alter procedure PNI_MOVIMENTA_ESTOQUE (
                                     IN_ID_UNIDADE integer,
                                     IN_ID_PRODUTO integer,
                                     IN_LOTE varchar(30),
                                     IN_ID_PRODUTOR integer,
                                     IN_QTDE_DOSES integer,
                                     IN_OPERACAO integer)
                                 as
                                 declare variable EXISTE_REGISTRO integer;
                                 BEGIN

                                   --VERIFICA SE EXISTE O REGISTRO DE ESTOQUE DO LOTE
                                   SELECT COUNT(EP.ID) FROM PNI_ESTOQUE_PRODUTO EP
                                   WHERE EP.ID_UNIDADE = :IN_ID_UNIDADE
                                     AND EP.LOTE = :IN_LOTE
                                     AND EP.ID_PRODUTO = :IN_ID_PRODUTO
                                     AND EP.ID_PRODUTOR = :IN_ID_PRODUTOR INTO :EXISTE_REGISTRO;

                                   --CASO N?O EXISTA O LOTE, INSERE O REGISTRO DE LOTE NA TABELA DE ESTOQUE
                                   IF (EXISTE_REGISTRO = 0) THEN
                                     BEGIN
                                       INSERT INTO PNI_ESTOQUE_PRODUTO (ID_UNIDADE, LOTE, ID_PRODUTO, ID_PRODUTOR, QTDE)
                                       VALUES(:IN_ID_UNIDADE, :IN_LOTE, :IN_ID_PRODUTO, :IN_ID_PRODUTOR, 0);
                                     END

                                   IF (IN_OPERACAO = 0) THEN
                                     BEGIN
                                     --0 = ADD NO ESTOQUE
                                     UPDATE PNI_ESTOQUE_PRODUTO E SET E.QTDE = E.QTDE + :IN_QTDE_DOSES
                                     WHERE E.ID_UNIDADE = :IN_ID_UNIDADE
                                     AND E.LOTE = :IN_LOTE
                                     AND E.ID_PRODUTO = :IN_ID_PRODUTO
                                     AND E.ID_PRODUTOR = :IN_ID_PRODUTOR;
                                     END
                                   ELSE
                                     BEGIN
                                     --1 = REMOVE DO ESTOQUE
                                     UPDATE PNI_ESTOQUE_PRODUTO E SET E.QTDE = E.QTDE - :IN_QTDE_DOSES
                                     WHERE E.ID_UNIDADE = :IN_ID_UNIDADE
                                     AND E.LOTE = :IN_LOTE
                                     AND E.ID_PRODUTO = :IN_ID_PRODUTO
                                     AND E.ID_PRODUTOR = :IN_ID_PRODUTOR;
                                     END
                                 END^

                                SET TERM ; ^",
                    codigo = 19
                });

                //PROCEDURE: PNI_INSERT_MOVIMENTO_PRODUTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                create or alter procedure PNI_INSERT_MOVIMENTO_PRODUTO (
                                    ID_UNIDADE integer,
                                    LOTE varchar(30),
                                    ID_PRODUTO integer,
                                    ID_PRODUTOR integer,
                                    QUANTIDADE integer,
                                    DATA timestamp,
                                    TIPO_MOVIMENTO integer,
                                    OPERACAO integer,
                                    ID_ORIGEM integer,
                                    TABELA_ORIGEM varchar(35),
                                    USUARIO varchar(50))
                                as
                                declare variable EXISTE_REGISTRO integer;
                                declare variable ESTOQUE_ATUAL integer;
                                begin
                                --VERIFICA SE EXISTE O REGISTRO DE ESTOQUE DO LOTE
                                  SELECT EP.ID, EP.QTDE FROM PNI_ESTOQUE_PRODUTO EP
                                  WHERE EP.ID_UNIDADE = :ID_UNIDADE
                                    AND EP.LOTE = :LOTE
                                    AND EP.ID_PRODUTO = :ID_PRODUTO
                                    AND EP.ID_PRODUTOR = :ID_PRODUTOR INTO :EXISTE_REGISTRO, :ESTOQUE_ATUAL;

                                  --CASO NAO EXISTA O LOTE, INSERE O REGISTRO DE LOTE NA TABELA DE ESTOQUE
                                  IF (EXISTE_REGISTRO IS NULL) THEN
                                    BEGIN
                                      INSERT INTO PNI_ESTOQUE_PRODUTO (ID_UNIDADE, LOTE, ID_PRODUTO, ID_PRODUTOR, QTDE)
                                      VALUES(:ID_UNIDADE, :LOTE, :ID_PRODUTO, :ID_PRODUTOR, 0);
                                    END

                                    INSERT INTO PNI_MOVIMENTO_PRODUTO (ID_UNIDADE, LOTE, ID_PRODUTO, ID_PRODUTOR,
                                                                     QTDE, DATA, TIPO_MOVIMENTO, OPERACAO,
                                                                     USUARIO, ID_ORIGEM, TABELA_ORIGEM,
                                                                     ANO_APURACAO, MES_APURACAO, ESTOQUE_ANTERIOR)
                                    VALUES(:ID_UNIDADE, :LOTE, :ID_PRODUTO, :ID_PRODUTOR, :QUANTIDADE,
                                         CURRENT_TIMESTAMP, :TIPO_MOVIMENTO, :OPERACAO, :USUARIO, :ID_ORIGEM, :TABELA_ORIGEM,
                                         EXTRACT(YEAR FROM :DATA), EXTRACT(MONTH FROM :DATA), :ESTOQUE_ATUAL);
                                END^

                                SET TERM ; ^",
                    codigo = 20
                });

                //TRIGGER: TSI_CADPAC_GERA_VACINA
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER TSI_CADPAC_GERA_VACINA FOR TSI_CADPAC
                                ACTIVE AFTER INSERT OR UPDATE POSITION 0
                                AS
                                BEGIN
                                    IF (INSERTING) THEN
                                    BEGIN
                                      EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NEW.CSI_CODPAC, NULL, 1);

                                      IF (NEW.CSI_SEXPAC = 'Feminino') THEN
                                        EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NEW.CSI_CODPAC, NULL, 3);
                                      ELSE IF (NEW.CSI_SEXPAC = 'Masculino') THEN
                                        EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NEW.CSI_CODPAC, NULL, 4);
    
                                      IF (NEW.VERIFICA_DEFICIENCIA = 1) THEN
                                        EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NEW.CSI_CODPAC, NULL, 6);
                                    END

                                    ELSE
                                    BEGIN
                                      IF (OLD.VERIFICA_DEFICIENCIA = 0 AND NEW.VERIFICA_DEFICIENCIA = 1) THEN
                                        EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(OLD.CSI_CODPAC, NULL, 6);

                                      IF (OLD.CSI_SEXPAC <> NEW.CSI_SEXPAC) THEN
                                      BEGIN
                                        IF (NEW.CSI_SEXPAC = 'Feminino') THEN
                                          EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NEW.CSI_CODPAC, NULL, 3);
                                        ELSE IF (NEW.CSI_SEXPAC = 'Masculino') THEN
                                          EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NEW.CSI_CODPAC, NULL, 4);
                                      END

                                    END
                                END^

                            SET TERM ; ^",
                    codigo = 21
                });

                //TRIGGER: GESTACAO_ITEM_I_VACINA
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER GESTACAO_ITEM_I_VACINA FOR GESTACAO_ITEM
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                DECLARE VARIABLE VAR_ID_INDIVIDUO INTEGER;

                                BEGIN
                                  -- GERANDO APRAZAMENTO PARA GESTANTES EM ANDAMENTO. PUBLICO_ALVO = 2
                                  IF (NEW.FLG_DESFECHO = 0) THEN
                                  BEGIN
                                    SELECT G.ID_CIDADAO FROM GESTACAO G WHERE G.ID = NEW.ID_GESTACAO INTO :VAR_ID_INDIVIDUO;

                                    EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(:VAR_ID_INDIVIDUO, NULL, 2);
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 22
                });

                //TRIGGER: GESTACAO_ITEM_U_VACINA
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER GESTACAO_ITEM_U_VACINA FOR GESTACAO_ITEM
                                ACTIVE AFTER UPDATE POSITION 0
                                AS
                                DECLARE VARIABLE VAR_ID_CALENDARIO_BASICO INTEGER;
                                DECLARE VARIABLE VAR_ID_INDIVIDUO INTEGER;

                                BEGIN
                                  -- GERANDO APRAZAMENTO PARA PUERPERA. PUBLICO_ALVO = 5
                                  IF (NEW.FLG_DESFECHO = 2 AND OLD.FLG_DESFECHO = 0) THEN
                                  BEGIN
                                    SELECT G.ID_CIDADAO FROM GESTACAO G WHERE G.ID = NEW.ID_GESTACAO INTO :VAR_ID_INDIVIDUO;

                                    EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(:VAR_ID_INDIVIDUO, NULL, 5);

                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 23
                });

                //TRIGGER: PNI_VACINADOS_I_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_VACINADOS_I_ESTOQUE FOR PNI_VACINADOS
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.REGISTRO_ANTERIOR <> 'T') THEN
                                  BEGIN
                                   IF ((NEW.ID_PRODUTOR IS NOT NULL) AND (NEW.ID_PRODUTO IS NOT NULL)) THEN
                                   BEGIN
                                     EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                    NEW.ID_PRODUTOR, 1, NEW.DATA_APLICACAO, 1, 1,
                                                                                    NEW.ID, 'PNI_VACINADOS', NEW.USUARIO);
                                   END
                                  END
                                END^

                               SET TERM ; ^",
                    codigo = 24
                });

                //TRIGGER: PNI_VACINADOS_U_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_VACINADOS_U_ESTOQUE FOR PNI_VACINADOS
                                ACTIVE AFTER UPDATE POSITION 0
                                AS
                                BEGIN
                                  IF ((NEW.ID_UNIDADE <> OLD.ID_UNIDADE) OR (NEW.LOTE <> OLD.LOTE) OR
                                      (NEW.ID_PRODUTO <> OLD.ID_PRODUTO) OR (NEW.ID_PRODUTOR <> OLD.ID_PRODUTOR) OR
                                      (NEW.DATA_APLICACAO <> OLD.DATA_APLICACAO)) THEN
                                  BEGIN
                                    IF (OLD.REGISTRO_ANTERIOR <> 'T') THEN
                                    BEGIN
                                      IF ((OLD.ID_PRODUTOR IS NOT NULL) AND (OLD.ID_PRODUTO IS NOT NULL)) THEN
                                      BEGIN
                                        EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                              OLD.ID_PRODUTOR, 1, OLD.DATA_APLICACAO, 1, 0,
                                                                              OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                      END

                                      IF ((NEW.ID_PRODUTOR IS NOT NULL) AND (NEW.ID_PRODUTO IS NOT NULL)) THEN
                                      BEGIN
                                        EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                  NEW.ID_PRODUTOR, 1, NEW.DATA_APLICACAO, 1, 1,
                                                                                  NEW.ID, 'PNI_VACINADOS', NEW.USUARIO);
                                      END
                                    END
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 25
                });

                //TRIGGER: PNI_VACINADOS_D_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_VACINADOS_D_ESTOQUE FOR PNI_VACINADOS
                                ACTIVE AFTER DELETE POSITION 0
                                AS
                                BEGIN
                                  IF ((OLD.ID_PRODUTOR IS NOT NULL) AND (OLD.ID_PRODUTO IS NOT NULL)) THEN
                                  BEGIN
                                    EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                    OLD.ID_PRODUTOR, 1, OLD.DATA_APLICACAO, 1, 0,
                                                                                    OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 26
                });

                //TRIGGER: PNI_ACERTO_ESTOQUE_I_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER trigger pni_acerto_estoque_i_estoque for pni_acerto_estoque
                                active after insert position 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                begin
                                  SELECT LOGIN FROM SEG_USUARIO WHERE ID = NEW.ID_USUARIO INTO :USUARIO;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                  NEW.ID_PRODUTOR, NEW.QTDE, NEW.DATA, NEW.TIPO_LANCAMENTO, 1,
                                                                                  NEW.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);
                                END^

                                SET TERM ; ^",
                    codigo = 27
                });

                //TRIGGER: PNI_ACERTO_ESTOQUE_U_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER trigger pni_acerto_estoque_u_estoque for pni_acerto_estoque
                                active after update position 0
                                AS
                                DECLARE VARIABLE USUARIO VARCHAR(50);
                                begin
                                  SELECT LOGIN FROM SEG_USUARIO WHERE ID = NEW.ID_USUARIO INTO :USUARIO;

                                  IF ((NEW.ID_UNIDADE <> OLD.ID_UNIDADE) OR (NEW.LOTE <> OLD.LOTE) OR
                                      (NEW.ID_PRODUTO <> OLD.ID_PRODUTO) OR (NEW.ID_PRODUTOR <> OLD.ID_PRODUTOR) OR
                                      (NEW.DATA <> OLD.DATA) OR (NEW.QTDE <> OLD.QTDE)) THEN
                                  BEGIN

                                    EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                  OLD.ID_PRODUTOR, OLD.QTDE, OLD.DATA, OLD.TIPO_LANCAMENTO, 0,
                                                                                  OLD.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);

                                    EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                  NEW.ID_PRODUTOR, NEW.QTDE, NEW.DATA, NEW.TIPO_LANCAMENTO, 1,
                                                                                  NEW.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 28
                });

                // TRIGGER: PNI_ACERTO_ESTOQUE_D_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER trigger pni_acerto_estoque_d_estoque for pni_acerto_estoque
                                active after delete position 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                BEGIN
                                  SELECT LOGIN FROM SEG_USUARIO WHERE ID = OLD.ID_USUARIO INTO :USUARIO;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                  OLD.ID_PRODUTOR, OLD.QTDE, OLD.DATA, OLD.TIPO_LANCAMENTO, 0,
                                                                                  OLD.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);
                                END^

                                SET TERM ; ^",
                    codigo = 29
                });

                //TRIGGER: PNI_MOVIMENTO_PRODUTO_AI0
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_MOVIMENTO_PRODUTO_AI0 FOR PNI_MOVIMENTO_PRODUTO
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                begin
                                  EXECUTE PROCEDURE PNI_MOVIMENTA_ESTOQUE(NEW.ID_UNIDADE, NEW.ID_PRODUTO,
                                     NEW.LOTE, NEW.ID_PRODUTOR, NEW.QTDE, NEW.OPERACAO);
                                END^

                                SET TERM ; ^",
                    codigo = 30
                });

                //TRIGGER:PNI_MOVIMENTO_PRODUTO_BI
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_MOVIMENTO_PRODUTO_BI FOR PNI_MOVIMENTO_PRODUTO
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_MOVIMENTO_PRODUTO_ID,1);
                                 END^

                                SET TERM ; ^",
                    codigo = 31
                });

                //TRIGGER: PNI_ENTRADA_PROD_ITEM_I_MOV
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER trigger pni_entrada_prod_item_i_mov for pni_entrada_produto_item
                                active after insert position 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                 DECLARE VARIABLE LOTE VARCHAR(30);
                                 DECLARE VARIABLE ID_PRODUTO INT;
                                 DECLARE VARIABLE ID_PRODUTOR INT;
                                 DECLARE VARIABLE DATA TIMESTAMP;
                                BEGIN
                                  SELECT EP.USUARIO, EP.DATA
                                  FROM PNI_ENTRADA_PRODUTO EP WHERE EP.ID = NEW.ID_ENTRADA_PRODUTO
                                  INTO :USUARIO, :DATA;

                                  SELECT LP.LOTE, LP.ID_PRODUTO, LP.ID_PRODUTOR
                                  FROM PNI_LOTE_PRODUTO LP
                                  WHERE LP.ID = NEW.ID_LOTE
                                  INTO :LOTE, :ID_PRODUTO,  :ID_PRODUTOR;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, :LOTE, :ID_PRODUTO,
                                                                                 :ID_PRODUTOR, NEW.QTDE_DOSES,
                                                                                 :DATA, 0, 0, NEW.ID, 'PNI_ENTRADA_PRODUTO_ITEM', :USUARIO);
                                END^

                                SET TERM ; ^",
                    codigo = 32
                });

                //TRIGGER: PNI_ENTRADA_PROD_ITEM_U_MOV
                itens.Add(new ScriptViewModel()
                {
                    script = $@" SET TERM ^ ;
                                CREATE OR ALTER trigger pni_entrada_prod_item_u_mov for pni_entrada_produto_item
                                active after update position 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                 DECLARE VARIABLE LOTE VARCHAR(30);
                                 DECLARE VARIABLE ID_PRODUTO INT;
                                 DECLARE VARIABLE ID_PRODUTOR INT;
                                 DECLARE VARIABLE DATA TIMESTAMP;
                                BEGIN
                                  SELECT EP.USUARIO, EP.DATA
                                  FROM PNI_ENTRADA_PRODUTO EP WHERE EP.ID = NEW.ID_ENTRADA_PRODUTO
                                  INTO :USUARIO, :DATA;

                                  IF ((NEW.ID_LOTE <> OLD.ID_LOTE) OR (NEW.QTDE_DOSES <> OLD.QTDE_DOSES)) THEN
                                  BEGIN

                                  SELECT LP.LOTE, LP.ID_PRODUTO, LP.ID_PRODUTOR
                                  FROM PNI_LOTE_PRODUTO LP
                                  WHERE LP.ID = OLD.ID_LOTE
                                  INTO :LOTE, :ID_PRODUTO,  :ID_PRODUTOR;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, :LOTE, :ID_PRODUTO,
                                                                                 :ID_PRODUTOR, OLD.QTDE_DOSES,
                                                                                 :DATA, 0, 1, OLD.ID, 'PNI_ENTRADA_PRODUTO_ITEM', :USUARIO);


                                  SELECT LP.LOTE, LP.ID_PRODUTO, LP.ID_PRODUTOR
                                  FROM PNI_LOTE_PRODUTO LP
                                  WHERE LP.ID = NEW.ID_LOTE
                                  INTO :LOTE, :ID_PRODUTO,  :ID_PRODUTOR;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, :LOTE, :ID_PRODUTO,
                                                                                  :ID_PRODUTOR, NEW.QTDE_DOSES,
                                                                                  :DATA, 0, 0, NEW.ID, 'PNI_ENTRADA_PRODUTO_ITEM', :USUARIO);
                                  END
                                 END^

                                SET TERM ; ^",
                    codigo = 33
                });

                //TRIGGER: PNI_ENTRADA_PROD_ITEM_D_MOV
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER trigger pni_entrada_prod_item_d_mov for pni_entrada_produto_item
                                active after delete position 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                 DECLARE VARIABLE LOTE VARCHAR(30);
                                 DECLARE VARIABLE ID_PRODUTO INT;
                                 DECLARE VARIABLE ID_PRODUTOR INT;
                                 DECLARE VARIABLE DATA TIMESTAMP;
                                BEGIN
                                  SELECT EP.USUARIO, EP.DATA
                                  FROM PNI_ENTRADA_PRODUTO EP WHERE EP.ID = OLD.ID_ENTRADA_PRODUTO
                                  INTO :USUARIO, :DATA;

                                  SELECT LP.LOTE, LP.ID_PRODUTO, LP.ID_PRODUTOR
                                  FROM PNI_LOTE_PRODUTO LP
                                  WHERE LP.ID = OLD.ID_LOTE
                                  INTO :LOTE, :ID_PRODUTO,  :ID_PRODUTOR;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, :LOTE, :ID_PRODUTO,
                                                                                 :ID_PRODUTOR, OLD.QTDE_DOSES,
                                                                                 :DATA, 0, 1, OLD.ID, 'PNI_ENTRADA_PRODUTO_ITEM', :USUARIO);
                                END^

                                SET TERM ; ^",
                    codigo = 34
                });

                //TRIGGER: PNI_VACINADOS_AU0
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_VACINADOS_AU0 FOR PNI_VACINADOS
                                ACTIVE AFTER UPDATE POSITION 0
                                AS
                                BEGIN
                                 -- RGSYSTEM TEMP
                                END^

                                SET TERM ; ^",
                    codigo = 35
                });

                //PNI_VIA_ADM
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO PNI_VIA_ADM (ID, NOME)
                                                 VALUES (1, 'ENDOVENOSA');
                                INSERT INTO PNI_VIA_ADM (ID, NOME)
                                                 VALUES (2, 'INTRADÉRMICA');
                                INSERT INTO PNI_VIA_ADM (ID, NOME)
                                                 VALUES (3, 'SUBCUTÂNEA');
                                INSERT INTO PNI_VIA_ADM (ID, NOME)
                                                 VALUES (4, 'INTRAMUSCULAR');
                                INSERT INTO PNI_VIA_ADM (ID, NOME)
                                                 VALUES (5, 'INTRAMUSCULAR PROFUNDA');
                                INSERT INTO PNI_VIA_ADM (ID, NOME)
                                                 VALUES (6, 'ORAL');",
                    codigo = 36
                });

                //PNI_LOCAL_APLICACAO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (1, 'BOCA');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (2, 'REDE VENOSA');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (3, 'FACE ANTEROLATERAL EXTERNA DA COXA DIREITA');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (4, 'FACE ANTEROLATERAL EXTERNA DA COXA ESQUERDA');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (5, 'FACE ANTEROLATERAL EXTERNA DO ANTEBRAÇO DIREITO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (6, 'FACE ANTEROLATERAL EXTERNA DO ANTEBRAÇO ESQUERDO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (7, 'FACE ANTEROLATERAL EXTERNA DO BRAÇO DIREITO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (8, 'FACE ANTEROLATERAL EXTERNA DO BRAÇO ESQUERDO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (9, 'FACE EXTERNA INFERIOR DO BRAÇO DIREITO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (10, 'FACE EXTERNA INFERIOR DO BRAÇO ESQUERDO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (11, 'FACE EXTERNA SUPERIOR DO BRAÇO DIREITO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (12, 'FACE EXTERNA SUPERIOR DO BRAÇO ESQUERDO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (13, 'OUTRO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (14, 'DELTÓIDE DIREITO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (15, 'DELTÓIDE ESQUERDO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (16, 'DORSO GLÚTEO DIREITO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (17, 'DORSO GLÚTEO ESQUERDO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (18, 'VASTO LATERAL DA COXA DIREITA');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (19, 'VASTO LATERAL DA COXA ESQUERDA');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (20, 'VENTRO GLÚTEO DIREITO');
                                INSERT INTO PNI_LOCAL_APLICACAO (ID, NOME)
                                                         VALUES (21, 'VENTRO GLÚTEO ESQUERDO');",
                    codigo = 37
                });

                //PNI_LOCAL_VIA_ADM
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (1, 1, 6);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (2, 2, 1);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (4, 3, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (5, 4, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (6, 5, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (7, 6, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (8, 7, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (9, 8, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (10, 9, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (11, 10, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (12, 11, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (13, 12, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (14, 13, 2);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (15, 3, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (16, 4, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (17, 5, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (18, 6, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (19, 7, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (20, 8, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (21, 9, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (22, 10, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (23, 11, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (24, 12, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (25, 13, 3);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (26, 14, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (27, 15, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (28, 16, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (29, 17, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (30, 18, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (31, 19, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (32, 20, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (33, 21, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (34, 13, 4);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (35, 14, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (36, 15, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (37, 16, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (38, 17, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (39, 18, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (40, 19, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (41, 20, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (42, 21, 5);
                                INSERT INTO PNI_LOCAL_VIA_ADM (ID, ID_LOCAL, ID_VIA_ADM)
                                                       VALUES (43, 13, 5);",
                    codigo = 38
                });

                //PNI_PRODUTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"UPDATE PNI_PRODUTO SET ID_VIA_ADM = 2  WHERE ID = 15;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 9;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 42;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 22;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 6  WHERE ID = 45;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 41;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 3  WHERE ID = 51;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 3  WHERE ID = 24;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 47;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 35;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 6  WHERE ID = 28;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 3  WHERE ID = 34;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 60;
                                UPDATE PNI_PRODUTO SET ID_VIA_ADM = 4  WHERE ID = 60;",
                    codigo = 39
                });

                //PNI_APRESENTACAO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"UPDATE PNI_APRESENTACAO SET QUANTIDADE = 1 WHERE ID = 1;
                                UPDATE PNI_APRESENTACAO SET QUANTIDADE = 5 WHERE ID = 5;
                                UPDATE PNI_APRESENTACAO SET QUANTIDADE = 10 WHERE ID = 10;
                                UPDATE PNI_APRESENTACAO SET QUANTIDADE = 20 WHERE ID = 20;
                                UPDATE PNI_APRESENTACAO SET QUANTIDADE = 25 WHERE ID = 25;
                                UPDATE PNI_APRESENTACAO SET QUANTIDADE = 50 WHERE ID = 50;",
                    codigo = 40
                });

                //PNI_CALENDARIO_BASICO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (1, 15, 9, 1, 1, 1, 0, NULL, NULL, 0, 0, 0);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (2, 9, 8, 1, 1, 1, 0, NULL, NULL, 0, 0, 0);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (3, 42, 1, 1, 1, 30, 0, NULL, NULL, 0, 0.02, 0.02);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (4, 22, 1, 1, 1, 30, 0, NULL, NULL, 0, 0.02, 0.02);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (5, 45, 1, 1, 1, 30, 0, NULL, NULL, 0, 0.02, 0.02);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (6, 26, 1, 1, 1, 30, 0, NULL, NULL, 0, 0.02, 0.02);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (7, 42, 2, 1, 1, 30, 0, NULL, NULL, 0, 0.04, 0.04);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (8, 22, 2, 1, 1, 30, 0, NULL, NULL, 0, 0.04, 0.04);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (9, 45, 2, 1, 1, 30, 0, NULL, NULL, 0, 0.04, 0.04);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (10, 26, 2, 1, 1, 30, 0, NULL, NULL, 0, 0.04, 0.04);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (11, 41, 2, 1, 1, 30, 0, NULL, NULL, 0, 0.05, 0.05);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (12, 42, 3, 1, 1, 30, 0, NULL, NULL, 0, 0.06, 0.06);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (13, 22, 3, 1, 1, 30, 0, NULL, NULL, 0, 0.06, 0.06);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (14, 14, 1, 1, 1, 60, 0, NULL, NULL, 0, 0.09, 0.09);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (15, 26, 38, 1, 1, 60, 0, NULL, NULL, 0, 1, 1);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (16, 24, 1, 1, 1, 60, 0, NULL, NULL, 0, 1, 1);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (17, 41, 6, 1, 1, 60, 0, NULL, NULL, 0, 1, 1);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (18, 28, 6, 1, 1, 60, 0, NULL, NULL, 0, 1.03, 1.03);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (19, 55, 1, 1, 1, 90, 0, NULL, NULL, 0, 1.03, 1.03);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (20, 47, 1, 1, 1, 90, 0, NULL, NULL, 0, 1.03, 1.03);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (21, 56, 9, 1, 1, 90, 0, NULL, NULL, 0, 1.03, 1.03);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (22, 47, 7, 1, 1, 90, 0, NULL, NULL, 0, 4, 4);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (23, 28, 7, 1, 1, 90, 0, NULL, NULL, 0, 4, 4);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (24, 34, 2, 1, 1, 90, 0, NULL, NULL, 0, 4, 4);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (25, 14, 38, 1, 1, 90, 0, NULL, NULL, 0, 4, 4);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (26, 60, 1, 3, 1, 365, 0, NULL, NULL, 0, 9, 14);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (27, 60, 2, 3, 1, 365, 0, NULL, NULL, 0, 9, 14);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (28, 60, 1, 4, 1, 365, 0, NULL, NULL, 0, 11, 13);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (29, 60, 2, 4, 1, 365, 0, NULL, NULL, 0, 11, 13);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (30, 9, 8, 1, 1, 365, 0, NULL, NULL, 1, 11, 19);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (31, 41, 7, 1, 1, 365, 0, NULL, NULL, 0, 11, 14);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (32, 9, 1, 1, 1, 365, 0, NULL, NULL, 0, 20, 59);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (33, 9, 2, 1, 1, 365, 0, NULL, NULL, 0, 20, 59);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (34, 9, 3, 1, 1, 365, 0, NULL, NULL, 0, 20, 59);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (35, 24, 1, 1, 1, 365, 0, NULL, NULL, 0, 20, 29);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (36, 24, 2, 1, 1, 365, 0, NULL, NULL, 0, 20, 29);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (37, 24, 1, 1, 1, 365, 0, NULL, NULL, 0, 30, 49);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (38, 9, 1, 2, 1, 1, 0, NULL, NULL, 1, 0, 0);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (39, 9, 2, 2, 1, 1, 0, NULL, NULL, 1, 0.01, 0.02);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (40, 9, 3, 2, 1, 190, 0, NULL, NULL, 1, 0.06, 0.09);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (41, 25, 38, 2, 1, 1, 0, NULL, NULL, 1, 0, 9);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (42, 46, 6, 2, 1, 1, 0, NULL, NULL, 1, 0, 0.09);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (43, 9, 1, 1, 1, 365, 0, NULL, NULL, 0, 60, 120);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (44, 9, 2, 1, 1, 365, 0, NULL, NULL, 1, 60.01, 120);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (45, 9, 3, 1, 1, 365, 0, NULL, NULL, 1, 60.06, 120);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (46, 25, 38, 1, 1, 365, 0, NULL, NULL, 1, 60, 120);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (47, 41, 1, 1, 1, 30, 0, NULL, NULL, 0, 0.03, 0.03);
                                INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, ID_ESTRATEGIA, DIAS_ANTES_APRAZAMENTO, FLG_INATIVO, VIGENCIA_INICIO, VIGENCIA_FIM, FLG_EXCLUIR_APRAZAMENTO, IDADE_MINIMA, IDADE_MAXIMA)
                                                           VALUES (48, 33, 9, 2, 2, 1, 0, NULL, NULL, 1, 0, 0.09);",
                    codigo = 41
                });

                //Atuais

                //PNI_ENVIO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE PNI_ENVIO (
                                    ID D_INTEGER NOT NULL,
                                    ID_UNIDADE_ORIGEM D_INTEGER NOT NULL,
                                    ID_UNIDADE_DESTINO D_INTEGER NOT NULL,
                                    DATA_ENVIO D_DATE NOT NULL,
                                    ID_USUARIO D_INTEGER NOT NULL,
                                    STATUS D_INTEGER NOT NULL,
                                    OBSERVACAO D_TEXT_LONGO1);
                                
                                ALTER TABLE PNI_ENVIO
                                ADD CONSTRAINT PK_PNI_ENVIO
                                PRIMARY KEY (ID);
                                
                                COMMENT ON COLUMN PNI_ENVIO.ID_UNIDADE_ORIGEM IS
                                'UNIDADE DE ORIGEM DO ENVIO';
                                
                                COMMENT ON COLUMN PNI_ENVIO.ID_UNIDADE_DESTINO IS
                                'UNIDADE DE DESTINO DO ENVIO';
                                
                                COMMENT ON COLUMN PNI_ENVIO.STATUS IS
                                '0=CADASTRADO|1=ENVIADO|2=RECEBIDO';
                                
                                CREATE SEQUENCE GEN_PNI_ENVIO_ID;
                                
                                SET TERM ^ ;
                                CREATE TRIGGER PNI_ENVIO_BI FOR PNI_ENVIO
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_ENVIO_ID,1);
                                END^

                                SET TERM ; ^
                                
                                ALTER TABLE PNI_ENVIO
                                ADD CONSTRAINT FK_PNI_ENVIO_UNIDADE_ORIGEM
                                FOREIGN KEY (ID_UNIDADE_ORIGEM)
                                REFERENCES TSI_UNIDADE(CSI_CODUNI)
                                ON UPDATE CASCADE;
                                
                                ALTER TABLE PNI_ENVIO
                                ADD CONSTRAINT FK_PNI_ENVIO_UNIDADE_DESTINO
                                FOREIGN KEY (ID_UNIDADE_DESTINO)
                                REFERENCES TSI_UNIDADE(CSI_CODUNI)
                                ON UPDATE CASCADE;
                                
                                ALTER TABLE PNI_ENVIO
                                ADD CONSTRAINT FK_PNI_ENVIO_USUARIO
                                FOREIGN KEY (ID_USUARIO)
                                REFERENCES SEG_USUARIO(ID)
                                ON UPDATE CASCADE;",
                    codigo = 42
                });

                //PNI_ENVIO_ITEM 
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE PNI_ENVIO_ITEM (
                                    ID INTEGER NOT NULL,
                                    ID_ENVIO INTEGER NOT NULL,
                                    ID_LOTE INTEGER NOT NULL,
                                    QTDE_FRASCOS INTEGER NOT NULL,
                                    VALOR D_NUMERICO);

                                ALTER TABLE PNI_ENVIO_ITEM
                                ADD CONSTRAINT PK_PNI_ENVIO_ITEM
                                PRIMARY KEY (ID);

                                COMMENT ON COLUMN PNI_ENVIO_ITEM.VALOR IS
                                'VALOR DA ULTIMA ENTRADA DO LOTE';

                                ALTER TABLE PNI_ENVIO_ITEM
                                ADD CONSTRAINT FK_PNI_ENVIO_ITEM_ENVIO
                                FOREIGN KEY (ID_ENVIO)
                                REFERENCES PNI_ENVIO(ID)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_ENVIO_ITEM
                                ADD CONSTRAINT FK_PNI_ENVIO_ITEM_LOTE
                                FOREIGN KEY (ID_LOTE)
                                REFERENCES PNI_LOTE_PRODUTO(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE PNI_ENVIO_ITEM
                                ADD CONSTRAINT UNQ_PNI_ENVIO_ITEM_ENVIO_LOTE
                                UNIQUE (ID_ENVIO,ID_LOTE);

                                CREATE SEQUENCE GEN_PNI_ENVIO_ITEM_ID;

                                SET TERM ^ ;
                                CREATE TRIGGER PNI_ENVIO_ITEM_BI FOR PNI_ENVIO_ITEM
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_PNI_ENVIO_ITEM_ID,1);
                                END^

                                SET TERM ; ^
                                ",
                    codigo = 43
                });

                //PNI_ACERTO_ESTOQUE
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE PNI_ACERTO_ESTOQUE
                                ADD ID_ENVIO_ITEM D_INTEGER;

                                ALTER TABLE PNI_ACERTO_ESTOQUE
                                ADD CONSTRAINT FK_PNI_ACERTO_ENVIO_ITEM
                                FOREIGN KEY (ID_ENVIO_ITEM)
                                REFERENCES PNI_ENVIO_ITEM(ID)
                                ON UPDATE CASCADE;",
                    codigo = 44
                });

                //PNI_ENTRADA_PRODUTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE PNI_ENTRADA_PRODUTO
                                ADD ID_ENVIO INTEGER;

                                ALTER TABLE PNI_ENTRADA_PRODUTO
                                ADD CONSTRAINT FK_PNI_ENTRADA_PRODUTO_1
                                FOREIGN KEY (ID_ENVIO)
                                REFERENCES PNI_ENVIO(ID)
                                ON DELETE SET NULL
                                ON UPDATE CASCADE;",
                    codigo = 45
                });

                //TRIGGER - PNI_ENVIO
                itens.Add(new ScriptViewModel()
                {
                    script = $@" SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_ENVIO_MOV FOR PNI_ENVIO
                                ACTIVE AFTER UPDATE POSITION 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                 DECLARE VARIABLE LOTE VARCHAR(30);
                                 DECLARE VARIABLE ID_ENVIO_ITEM INT;
                                 DECLARE VARIABLE ID_PRODUTO INT;
                                 DECLARE VARIABLE ID_PRODUTOR INT;
                                 DECLARE VARIABLE QTDE_FRASCOS INT;
                                 DECLARE VARIABLE QTDE_DOSES_POR_FRASCO INT;
                                 DECLARE VARIABLE QTDE_DOSES INT;
                                BEGIN
                                  IF (OLD.STATUS = 0 AND NEW.STATUS = 1) THEN
                                  BEGIN
                                    SELECT U.LOGIN FROM SEG_USUARIO U WHERE U.ID = NEW.ID_USUARIO
                                    INTO :USUARIO;

                                    FOR
                                        SELECT EI.ID, EI.QTDE_FRASCOS, LP.ID_PRODUTO, LP.ID_PRODUTOR, LP.LOTE, A.QUANTIDADE
                                        FROM PNI_ENVIO_ITEM EI
                                        JOIN PNI_LOTE_PRODUTO LP ON EI.ID_LOTE = LP.ID
                                        JOIN PNI_APRESENTACAO A ON LP.ID_APRESENTACAO = A.ID
                                        WHERE EI.ID_ENVIO = NEW.ID
                                        INTO :ID_ENVIO_ITEM, :QTDE_FRASCOS, :ID_PRODUTO, :ID_PRODUTOR, :LOTE, :QTDE_DOSES_POR_FRASCO
                                    DO
                                    BEGIN
                                      -- CALCULO DE DOSES PARA MOVIMENTAR ESTOQUE
                                      QTDE_DOSES = (:QTDE_FRASCOS * :QTDE_DOSES_POR_FRASCO);

                                      EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE_ORIGEM, :LOTE, :ID_PRODUTO,
                                                                                    :ID_PRODUTOR, :QTDE_DOSES, NEW.DATA_ENVIO, 4, 1,
                                                                                    :ID_ENVIO_ITEM, 'PNI_ENVIO_ITEM', :USUARIO);
                                    END
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 46
                });

                //TRIGGER - PNI_ENTRADA_PRODUTO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER TRIGGER PNI_ENTR_PROD_ALT_STATUS_ENVIO FOR PNI_ENTRADA_PRODUTO
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                BEGIN
                                  /* SE FOR UMA ENTRADA RECEBIDA DE OUTRA UNIDADE (PEGANDO REGISTRO DE PNI_ENVIO),
                                     ALTERA O STATUS NA TABELA PNI_ENVIO PARA 2 (RECEBIDO) */

                                  IF (NEW.ID_ENVIO IS NOT NULL) THEN
                                  BEGIN
                                    UPDATE PNI_ENVIO SET STATUS = 2 WHERE ID = NEW.ID_ENVIO;
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 47
                });

                //SEG_USUARIO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE SEG_USUARIO
                                ADD TIPO_USUARIO SMALLINT;

                                COMMENT ON COLUMN SEG_USUARIO.TIPO_USUARIO IS
                                '1=SUPER_USER|2=ADMIN|3=USER';",
                    codigo = 48
                });

                //SEG_MODULOS
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE SEG_MODULOS (
                                  ID INTEGER NOT NULL,
                                  DESCRICAO VARCHAR(100) CHARACTER SET ISO8859_1 NOT NULL COLLATE ISO8859_1);

                                ALTER TABLE SEG_MODULOS
                                ADD CONSTRAINT PK_SEG_MODULOS
                                PRIMARY KEY (ID);

                                ALTER TABLE SEG_MODULOS
                                ADD NOME VARCHAR(100)
                                NOT NULL;

                                ALTER TABLE SEG_MODULOS
                                ADD CONSTRAINT UNQ_SEG_MODULOS_NOME
                                UNIQUE (NOME);
                                ",
                    codigo = 49
                });

                //SEG_TELAS
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE SEG_TELAS (
                                  ID INTEGER NOT NULL,
                                  DESCRICAO VARCHAR(100) CHARACTER SET ISO8859_1 NOT NULL COLLATE ISO8859_1,
                                  NOME VARCHAR(100) CHARACTER SET ISO8859_1 NOT NULL COLLATE ISO8859_1,
                                  ID_MODULO INTEGER NOT NULL);

                                ALTER TABLE SEG_TELAS
                                ADD CONSTRAINT PK_SEG_TELAS
                                PRIMARY KEY (ID);

                                ALTER TABLE SEG_TELAS
                                ADD CONSTRAINT FK_SEG_TELAS_MODULO
                                FOREIGN KEY (ID_MODULO)
                                REFERENCES SEG_MODULOS(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE SEG_TELAS
                                ADD CONSTRAINT UNQ_SEG_TELAS_MOD_TELA
                                UNIQUE (NOME,ID_MODULO);",
                    codigo = 50
                });

                //SEG_ACOES
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE SEG_ACOES (
                                  ID INTEGER NOT NULL,
                                  DESCRICAO VARCHAR(100) CHARACTER SET ISO8859_1 NOT NULL COLLATE ISO8859_1);

                                ALTER TABLE SEG_ACOES
                                ADD CONSTRAINT PK_SEG_ACOES
                                PRIMARY KEY (ID);

                                ALTER TABLE SEG_ACOES
                                ADD NOME VARCHAR(100) CHARACTER SET ISO8859_1 
                                NOT NULL 
                                COLLATE ISO8859_1;

                                ALTER TABLE SEG_ACOES
                                ADD CONSTRAINT UNQ_SEG_ACOES_NOME
                                UNIQUE (NOME);",
                    codigo = 51
                });

                //SEG_TELAS_ACOES
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE SEG_TELAS_ACOES (
                                  ID INTEGER NOT NULL,
                                  ID_TELA INTEGER NOT NULL,
                                  ID_ACAO INTEGER NOT NULL);

                                ALTER TABLE SEG_TELAS_ACOES
                                ADD CONSTRAINT PK_SEG_TELAS_ACOES
                                PRIMARY KEY (ID);

                                ALTER TABLE SEG_TELAS_ACOES
                                ADD CONSTRAINT FK_SEG_TELAS_ACOES_TELA
                                FOREIGN KEY (ID_TELA)
                                REFERENCES SEG_TELAS(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE SEG_TELAS_ACOES
                                ADD CONSTRAINT FK_SEG_TELAS_ACOES_ACAO
                                FOREIGN KEY (ID_ACAO)
                                REFERENCES SEG_ACOES(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE SEG_TELAS_ACOES
                                ADD CONSTRAINT UNQ_SEG_TELAS_ACOES_TELA_ACAO
                                UNIQUE (ID_TELA,ID_ACAO);",
                    codigo = 52
                });

                //SEG_PERFIL_ACESSO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE SEG_PERFIL_ACESSO (
                                  ID INTEGER NOT NULL,
                                  DESCRICAO VARCHAR(100) CHARACTER SET ISO8859_1 NOT NULL COLLATE ISO8859_1);

                                ALTER TABLE SEG_PERFIL_ACESSO
                                ADD CONSTRAINT PK_SEG_PERFIL_ACESSO
                                PRIMARY KEY (ID);",
                    codigo = 53
                });

                //SEG_PERMISSOES_PERFIL
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE SEG_PERMISSOES_PERFIL (
                                  ID INTEGER NOT NULL,
                                  ID_TELA_ACAO INTEGER NOT NULL,
                                  ID_PERFIL INTEGER NOT NULL,
                                  PERMISSAO SMALLINT NOT NULL);

                                ALTER TABLE SEG_PERMISSOES_PERFIL
                                ADD CONSTRAINT PK_SEG_PERMISSOES_PERFIL
                                PRIMARY KEY (ID);

                                ALTER TABLE SEG_PERMISSOES_PERFIL
                                ADD CONSTRAINT FK_SEG_PERM_PERFIL_TELA_ACAO
                                FOREIGN KEY (ID_TELA_ACAO)
                                REFERENCES SEG_TELAS_ACOES(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE SEG_PERMISSOES_PERFIL
                                ADD CONSTRAINT FK_SEG_PERMISSOES_PERFIL_PERFIL
                                FOREIGN KEY (ID_PERFIL)
                                REFERENCES SEG_PERFIL_ACESSO(ID)
                                ON UPDATE CASCADE;

                                ALTER TABLE SEG_PERMISSOES_PERFIL
                                ADD CONSTRAINT UNQ_SEG_PERM_PERF_TACAO_PERFIL
                                UNIQUE (ID_PERFIL,ID_TELA_ACAO);",
                    codigo = 54
                });

                //SEG_PERFIL_USUARIO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE SEG_PERFIL_USUARIO (
                                   ID INTEGER NOT NULL,
                                   ID_USUARIO INTEGER NOT NULL,
                                   ID_UNIDADE INTEGER NOT NULL,
                                   ID_PERFIL INTEGER NOT NULL);
                                 
                                 ALTER TABLE SEG_PERFIL_USUARIO
                                 ADD CONSTRAINT PK_SEG_PERFIL_USUARIO
                                 PRIMARY KEY (ID);
                                 
                                 ALTER TABLE SEG_PERFIL_USUARIO
                                 ADD CONSTRAINT FK_SEG_PERFIL_USUARIO_USUARIO
                                 FOREIGN KEY (ID_USUARIO)
                                 REFERENCES SEG_USUARIO(ID)
                                 ON UPDATE CASCADE;
                                 
                                 ALTER TABLE SEG_PERFIL_USUARIO
                                 ADD CONSTRAINT FK_SEG_PERFIL_USUARIO_UNIDADE
                                 FOREIGN KEY (ID_UNIDADE)
                                 REFERENCES TSI_UNIDADE(CSI_CODUNI)
                                 ON UPDATE CASCADE;
                                 
                                 ALTER TABLE SEG_PERFIL_USUARIO
                                 ADD CONSTRAINT FK_SEG_PERFIL_USUARIO_PERFIL
                                 FOREIGN KEY (ID_PERFIL)
                                 REFERENCES SEG_PERFIL_ACESSO(ID)
                                 ON UPDATE CASCADE;
                                 
                                 ALTER TABLE SEG_PERFIL_USUARIO
                                 ADD CONSTRAINT UNQ_USU_UNI_PERFIL
                                 UNIQUE (ID_USUARIO,ID_UNIDADE,ID_PERFIL);",
                    codigo = 55
                });

                //GENERATORS
                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE SEQUENCE GEN_SEG_TELAS_ACOES_ID;

                                CREATE SEQUENCE GEN_SEG_MODULOS_ID;

                                CREATE SEQUENCE GEN_SEG_TELAS_ID;

                                CREATE SEQUENCE GEN_SEG_ACOES_ID;

                                CREATE SEQUENCE GEN_SEG_PERMISSOES_PERFIL_ID;

                                CREATE SEQUENCE GEN_SEG_PERFIL_ACESSO_ID;

                                CREATE SEQUENCE GEN_SEG_PERFIL_USUARIO_ID;",
                    codigo = 56
                });

                //PROCEDURES 
                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER PROCEDURE SEG_INSERT_PERMISSOES_PERFIL (
                                  IN_ID_PERFIL INTEGER,
                                  IN_ID_TELA_ACAO INTEGER)
                                AS
                                DECLARE VARIABLE VAR_ID_TELA_ACAO INTEGER;
                                DECLARE VARIABLE VAR_ID_PERFIL INTEGER;
                                BEGIN
                                  -- AO INSERRIR NOVO PERFIL
                                  IF (IN_ID_PERFIL IS NOT NULL) THEN
                                  BEGIN
                                    FOR SELECT ID FROM SEG_TELAS_ACOES INTO :VAR_ID_TELA_ACAO DO
                                    BEGIN
                                      INSERT INTO SEG_PERMISSOES_PERFIL (ID_TELA_ACAO, ID_PERFIL, PERMISSAO)
                                      VALUES (:VAR_ID_TELA_ACAO, :IN_ID_PERFIL, 0);
                                    END
                                  END

                                  -- AO INSERIR NOVA TELA_ACAO
                                  ELSE IF (IN_ID_TELA_ACAO IS NOT NULL) THEN
                                  BEGIN
                                    FOR SELECT ID FROM SEG_PERFIL_ACESSO INTO :VAR_ID_PERFIL DO
                                    BEGIN
                                      INSERT INTO SEG_PERMISSOES_PERFIL (ID_TELA_ACAO, ID_PERFIL, PERMISSAO)
                                      VALUES (:IN_ID_TELA_ACAO, :VAR_ID_PERFIL, 0);
                                    END
                                  END
                                  SUSPEND;
                                END^

                                SET TERM ; ^",
                    codigo = 57
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE TRIGGER SEG_TELAS_ACOES_BI FOR SEG_TELAS_ACOES
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_SEG_TELAS_ACOES_ID,1);

                                END^

                                SET TERM ; ^",
                    codigo = 58
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE TRIGGER SEG_MODULOS_BI FOR SEG_MODULOS
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_SEG_MODULOS_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 59
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE TRIGGER SEG_TELAS_BI FOR SEG_TELAS
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_SEG_TELAS_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 60
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE TRIGGER SEG_ACOES_BI FOR SEG_ACOES
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_SEG_ACOES_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 61
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                               CREATE TRIGGER SEG_PERMISSOES_PERFIL_BI FOR SEG_PERMISSOES_PERFIL
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_SEG_PERMISSOES_PERFIL_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 62
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                               CREATE TRIGGER SEG_PERFIL_ACESSO_BI FOR SEG_PERFIL_ACESSO
                               ACTIVE BEFORE INSERT POSITION 0
                               AS
                               BEGIN
                                   IF (NEW.ID IS NULL) THEN
                                   NEW.ID = GEN_ID(GEN_SEG_PERFIL_ACESSO_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 63
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE TRIGGER SEG_PERFIL_USUARIO_BI FOR SEG_PERFIL_USUARIO
                                ACTIVE BEFORE INSERT POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.ID IS NULL) THEN
                                    NEW.ID = GEN_ID(GEN_SEG_PERFIL_USUARIO_ID,1);
                                END^

                                SET TERM ; ^",
                    codigo = 64
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE OR ALTER TRIGGER SEG_TELAS_ACOES_I_PERM_PERFIL FOR SEG_TELAS_ACOES
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                BEGIN
                                  EXECUTE PROCEDURE SEG_INSERT_PERMISSOES_PERFIL(NULL, NEW.ID);
                                END^

                                SET TERM ; ^",
                    codigo = 65
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE TRIGGER SEG_PERFIL_ACESSO_I_PERM_PERFIL FOR SEG_PERFIL_ACESSO
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                BEGIN
                                  EXECUTE PROCEDURE SEG_INSERT_PERMISSOES_PERFIL(NEW.ID, NULL);
                                END^

                                SET TERM ; ^",
                    codigo = 66
                });

                //MODULOS
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_MODULOS (ID, DESCRICAO, NOME) VALUES (1, 'Imunização', 'imunizacao');
                                INSERT INTO SEG_MODULOS (ID, DESCRICAO, NOME) VALUES (2, 'Relatórios', 'relatorios');
                                INSERT INTO SEG_MODULOS (ID, DESCRICAO, NOME) VALUES (3, 'Cadastro', 'cadastro');",
                    codigo = 67
                });

                //TELAS
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (1, 'Cartão Vacina', 'cartao_vacina', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (2, 'Produto', 'produto', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (3, 'Fabricante', 'fabricante', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (4, 'Entrada de Imunobiológico', 'entrada_imunobiologico', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (5, 'Movimentação', 'movimentacao', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (6, 'Auditoria de Estoque', 'auditoria_estoque', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (7, 'Calendário Básico', 'calendario_basico', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (8, 'Envio de Imunobiológico', 'envio_imunobiologico', 1);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (11, 'Indivíduo', 'individuo', 3);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (10, 'Imunização', 'imunizacao', 2);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (12, 'Fornecedor', 'fornecedor', 3);",
                    codigo = 68
                });

                //ACOES
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (1, 'Visualizar', 'visualizar');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (2, 'Inserir', 'inserir');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (3, 'Editar', 'editar');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (4, 'Excluir', 'excluir');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (5, 'Gerar Aprazamento', 'gerar_aprazamento');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (6, 'Excluir Aprazamento', 'excluir_aprazamento');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (7, 'Imprimir Cartão de Vacina', 'imprimir_cartao_vacina');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (8, 'Registar Dose', 'registrar_dose');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (20, 'Auditar Estoque', 'auditar_estoque');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (10, 'Cancelar Dose', 'cancelar_dose');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (11, 'Editar Dose', 'editar_dose');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (12, 'Ver Estoque', 'ver_estoque');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (13, 'Ver Lotes', 'ver_lotes');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (14, 'Editar Lote', 'editar_lote');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (15, 'Bloquear/Desbloquear Lote', 'bloquear_desbloquear_lote');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (16, 'Receber Envio', 'receber_envio');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (17, 'Incluir Indivíduo', 'incluir_individuo');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (18, 'Editar Indivíduo', 'editar_individuo');
                                INSERT INTO SEG_ACOES (ID, DESCRICAO, NOME) VALUES (19, 'Registar Dose Avulso', 'registrar_dose_avulso');",
                    codigo = 69
                });

                //TELAS_ACOES
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (1, 1, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (2, 1, 5);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (3, 1, 6);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (4, 1, 7);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (5, 1, 8);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (44, 2, 20);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (7, 1, 10);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (8, 1, 11);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (9, 7, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (10, 7, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (11, 7, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (12, 7, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (13, 7, 5);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (14, 3, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (15, 3, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (16, 3, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (17, 3, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (18, 2, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (19, 2, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (20, 2, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (21, 2, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (22, 2, 12);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (23, 2, 13);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (24, 2, 14);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (25, 2, 15);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (26, 11, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (27, 11, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (28, 11, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (29, 4, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (30, 4, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (31, 4, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (32, 8, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (33, 8, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (34, 8, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (35, 8, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (36, 5, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (37, 5, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (38, 5, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (39, 4, 16);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (40, 10, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (41, 1, 17);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (42, 1, 18);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (43, 1, 19);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (45, 12, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (46, 12, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (47, 12, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (48, 12, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (49, 6, 1);
                                ",
                    codigo = 70
                });
                //PERFIL_ACESSO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_PERFIL_ACESSO (ID, DESCRICAO) VALUES (1, 'Imunização');",
                    codigo = 71
                });
                //MIGRAÇÃO 
                //MIGRANDO USUÁRIOS QUE TINHAM ACESSO A IMUNIZAÇÃO PARA NOVA ESTRUTURA DE PERMISSOES
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_PERFIL_USUARIO (ID_USUARIO, ID_UNIDADE, ID_PERFIL)
                                SELECT USU.ID, USERUND.CSI_CODUNI, 1
                                FROM SEG_USUARIO USU
                                JOIN TSI_USERUNIDADE USERUND ON (USU.ID = USERUND.CSI_IDUSER)
                                WHERE (SELECT COUNT(*)
                                  FROM SEG_USUARIO U
                                  JOIN SEG_GRUPO G ON (G.ID = U.ID_GRUPO)
                                  JOIN SEG_GRUPO_PERFIL GP ON (GP.ID_GRUPO = G.ID)
                                  JOIN SEG_PERFIL P ON (P.ID = GP.ID_PERFIL)
                                  JOIN SEG_PERFIL_FUNCAO PF ON (PF.ID_PERFIL = P.ID)
                                  JOIN SEG_FUNCAO F ON (F.ID = PF.ID_FUNCAO)
                                  WHERE U.ID = USU.ID
                                  AND U.STATUS = 'A'
                                  AND F.FORMULARIO = 'F1ESUSVacinacao'
                                  AND F.FUNCAO_OBJETO = 'PodeIncluir'
                                ) > 0;",
                    codigo = 72
                });

                // CONCEDENDO PERMISSÕES AO PERFIL IMUNIZACAO
                itens.Add(new ScriptViewModel()
                {
                    script = $@"UPDATE SEG_PERMISSOES_PERFIL PP SET PP.PERMISSAO = 1
                                WHERE ( 
                                    SELECT M.ID FROM SEG_TELAS_ACOES TA
                                    JOIN SEG_TELAS T ON (TA.ID_TELA = T.ID)
                                    JOIN SEG_MODULOS M ON (T.ID_MODULO = M.ID)
                                    WHERE TA.ID = PP.ID_TELA_ACAO
                                ) = 1
                                OR (PP.ID_TELA_ACAO = 40);",
                    codigo = 73
                });

                //DEFININDO ADMINISTRADORES
                itens.Add(new ScriptViewModel()
                {
                    script = $@"UPDATE SEG_USUARIO USU SET USU.TIPO_USUARIO = 2
                                WHERE UPPER(USU.ADMINISTRADOR) = 'T';",
                    codigo = 74
                });

                //DEFININDO USUÁRIOS
                itens.Add(new ScriptViewModel()
                {
                    script = $@"UPDATE SEG_USUARIO USU SET USU.TIPO_USUARIO = 3
                                WHERE UPPER(USU.ADMINISTRADOR) = 'F'
                                OR (USU.ADMINISTRADOR IS NULL);",
                    codigo = 75
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE SEG_USUARIO
                                ADD EMAIL_3 D_TEXT_100;",
                    codigo = 76
                });

                #region Endemias
                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE VS_ESTABELECIMENTOS
                                ADD QUARTEIRAO_LOGRADOURO D_TEXT_010;

                                ALTER TABLE VS_ESTABELECIMENTOS
                                ADD SEQUENCIA_QUARTEIRAO D_INTEGER;
                            
                                ALTER TABLE VS_ESTABELECIMENTOS
                                ADD SEQUENCIA_NUMERO D_INTEGER;",
                    codigo = 77
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE ENDEMIAS_CICLOS (
                                    ID             INTEGER NOT NULL,
                                    DATA_INICIAL   DATE NOT NULL,
                                    DATA_FINAL     DATE,
                                    NUM_CICLO      VARCHAR(10),
                                    SITUACAO       D_INTEGER /* D_INTEGER = INTEGER */,
                                    DATA_SITUACAO  TIMESTAMP,
                                    ID_USUARIO     D_INTEGER /* D_INTEGER = INTEGER */
                                );

                                ALTER TABLE ENDEMIAS_CICLOS ADD CONSTRAINT PK_ENDEMIAS_CICLOS PRIMARY KEY (ID);
                                ALTER TABLE ENDEMIAS_CICLOS ADD CONSTRAINT FK_ENDEMIAS_CICLOS_USER FOREIGN KEY (ID_USUARIO) REFERENCES SEG_USUARIO (ID) ON UPDATE CASCADE;

                                COMMENT ON COLUMN ENDEMIAS_CICLOS.SITUACAO IS 
                                '0=ABERTO|1=ENCERRADO|2=REABERTO|3=DESATIVADO';

                                CREATE SEQUENCE GEN_ENDEMIAS_CICLOS_ID;",
                    codigo = 78
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE VISITA_IMOVEL
                                ADD ID_ESTABELECIMENTO INTEGER;

                                ALTER TABLE VISITA_IMOVEL
                                ADD CONSTRAINT FK_VISITA_IMOVEL_1
                                FOREIGN KEY (ID_ESTABELECIMENTO)
                                REFERENCES VS_ESTABELECIMENTOS(ID)
                                ON DELETE CASCADE;",
                    codigo = 79
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE ENDEMIAS_CICLOS_LOG (
                                    ID INTEGER NOT NULL,
                                    ID_CICLO INTEGER NOT NULL,
                                    SITUACAO INTEGER NOT NULL,
                                    DATA_SITUACAO TIMESTAMP NOT NULL);

                                ALTER TABLE ENDEMIAS_CICLOS_LOG
                                ADD CONSTRAINT PK_ENDEMIAS_CICLOS_LOG
                                PRIMARY KEY (ID);

                                ALTER TABLE ENDEMIAS_CICLOS_LOG
                                ADD CONSTRAINT FK_ENDEMIAS_CICLOS_LOG_1
                                FOREIGN KEY (ID_CICLO)
                                REFERENCES ENDEMIAS_CICLOS(ID);

                                COMMENT ON COLUMN ENDEMIAS_CICLOS_LOG.SITUACAO IS
                                '0=ABERTO|1=ENCERRADO|2=REABERTO|3=DESATIVADO';

                                CREATE SEQUENCE GEN_ENDEMIAS_CICLOS_LOG_ID;

                                ALTER TABLE ENDEMIAS_CICLOS_LOG
                                ADD ID_USUARIO SMALLINT;

                                ALTER TABLE ENDEMIAS_CICLOS_LOG
                                ADD CONSTRAINT FK_ENDEMIAS_CICLOS_LOG_2
                                FOREIGN KEY (ID_USUARIO)
                                REFERENCES SEG_USUARIO(ID);",
                    codigo = 80
                });

                #endregion

                #region scripts permissões
                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_MODULOS (ID, DESCRICAO, NOME) VALUES (4, 'Vigilância Ambiental', 'vigilancia_ambiental');",
                    codigo = 81
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (13, 'Ciclo', 'ciclo', 4);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (14, 'Espécime', 'especime', 4);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (15, 'Imóvel', 'imovel', 4);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (16, 'Visita', 'visita', 4);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (17, 'Resultado de Amostras', 'resultado_amostras', 4);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (18, 'Vigilância Ambiental', 'vigilancia_ambiental', 2);
                                INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES (19, 'Gestão de Imóveis', 'gestao_imovel', 4);",
                    codigo = 82
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (50, 13, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (51, 13, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (52, 13, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (53, 13, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (54, 14, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (55, 14, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (56, 14, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (57, 14, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (58, 15, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (59, 15, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (60, 15, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (61, 15, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (62, 16, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (63, 16, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (64, 16, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (65, 16, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (66, 17, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (68, 17, 2);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (69, 17, 3);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (70, 17, 4);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (71, 18, 1);
                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO) VALUES (72, 19, 1);",
                    codigo = 83
                });
                #endregion

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                 CREATE OR ALTER TRIGGER PNI_VACINADOS_U_ESTOQUE FOR PNI_VACINADOS
                                 ACTIVE AFTER UPDATE POSITION 0
                                 AS
                                 BEGIN
                                   IF ((NEW.ID_UNIDADE <> OLD.ID_UNIDADE) OR (NEW.LOTE <> OLD.LOTE) OR
                                       (NEW.ID_PRODUTO <> OLD.ID_PRODUTO) OR (NEW.ID_PRODUTOR <> OLD.ID_PRODUTOR) OR
                                       (NEW.DATA_APLICACAO <> OLD.DATA_APLICACAO)) THEN
                                   BEGIN
                                     IF (COALESCE(OLD.REGISTRO_ANTERIOR,'F') <> 'T') THEN
                                     BEGIN
                                       IF ((OLD.ID_PRODUTOR IS NOT NULL) AND (OLD.ID_PRODUTO IS NOT NULL)) THEN
                                       BEGIN
                                         EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                               OLD.ID_PRODUTOR, 1, OLD.DATA_APLICACAO, 1, 0,
                                                                               OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                       END
                                       IF ((NEW.ID_PRODUTOR IS NOT NULL) AND (NEW.ID_PRODUTO IS NOT NULL)) THEN
                                       BEGIN
                                         EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                   NEW.ID_PRODUTOR, 1, NEW.DATA_APLICACAO, 1, 1,
                                                                                   NEW.ID, 'PNI_VACINADOS', NEW.USUARIO);
                                       END
                                     END
                                   END
                                   IF (COALESCE(NEW.FLG_EXCLUIDO,0) <> COALESCE(OLD.FLG_EXCLUIDO,0)) THEN
                                   BEGIN
                                     IF (COALESCE(OLD.REGISTRO_ANTERIOR,'F') <> 'T') THEN
                                     BEGIN
                                       IF (COALESCE(NEW.FLG_EXCLUIDO,0) = 1) THEN
                                       BEGIN
                                         DELETE FROM PNI_APRAZAMENTO WHERE ID_CALENDARIO_BASICO IS NULL AND ID_VACINADOS = OLD.ID;
                                         UPDATE PNI_APRAZAMENTO APZ SET APZ.ID_VACINADOS = NULL WHERE APZ.ID_VACINADOS = OLD.ID;
                                         EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                        OLD.ID_PRODUTOR, 1, OLD.DATA_APLICACAO, 1, 0,
                                                                                        OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                       END
                                       ELSE BEGIN
                                       -- Validar melhor para tratar esse tipo de situação
                                       END
                                     END
                                   END
                                END^
                                SET TERM ; ^",
                    codigo = 84
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE ENDEMIAS_CICLOS
                                ADD DATA_ALTERACAO_SERV D_DATE;",
                    codigo = 85
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE OR ALTER TRIGGER ENDEMIAS_CICLOS_BIU_SINC_MOBILE FOR ENDEMIAS_CICLOS
                                ACTIVE BEFORE INSERT OR UPDATE OR DELETE POSITION 0
                                AS
                                BEGIN
                                  IF (UPDATING) THEN
                                    BEGIN
                                      NEW.DATA_ALTERACAO_SERV = (SELECT DATA_ALTERACAO_SERV
                                      FROM PRO_MOBILE_ATUALIZA_DATAS('UPDATING', OLD.DATA_ALTERACAO_SERV, NEW.DATA_ALTERACAO_SERV));
                                    END
                                    IF (INSERTING) THEN
                                    BEGIN
                                      NEW.DATA_ALTERACAO_SERV = (SELECT DATA_ALTERACAO_SERV
                                      FROM PRO_MOBILE_ATUALIZA_DATAS('INSERTING', OLD.DATA_ALTERACAO_SERV, NEW.DATA_ALTERACAO_SERV));
                                    END
                                    IF (DELETING) THEN
                                    BEGIN
                                      EXECUTE PROCEDURE PRO_MOBILE_REG_EXCLUIDOS(CURRENT_TIMESTAMP, 'ENDEMIAS_CICLOS', OLD.ID, NULL, 'ID', NULL,
                                          NULL);
                                    END
                                END^
                                SET TERM ; ^",
                    codigo = 86
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Tabela: VISITA_IMOVEL
                                ALTER TABLE VISITA_IMOVEL
                                ADD ID_CICLO INTEGER;

                                ALTER TABLE VISITA_IMOVEL
                                ADD CONSTRAINT FK_VISITA_IMOVEL_CICLO
                                FOREIGN KEY (ID_CICLO)
                                REFERENCES ENDEMIAS_CICLOS(ID)
                                ON UPDATE CASCADE;",
                    codigo = 87
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE VISITA_IMOVEL
                                ADD OBSERVACOES VARCHAR(250) CHARACTER SET ISO8859_1 
                                COLLATE ISO8859_1;",
                    codigo = 88
                });


                itens.Add(new ScriptViewModel()
                {
                    script = $@"CREATE TABLE PNI_LOTE_UNIDADE_BLOQUEADO (
                                     ID_UNIDADE INTEGER NOT NULL,
                                     ID_LOTE INTEGER NOT NULL);

                                 ALTER TABLE PNI_LOTE_UNIDADE_BLOQUEADO
                                 ADD CONSTRAINT FK_PNI_LOTE_UNIDADE_BLOQ_UNI
                                 FOREIGN KEY (ID_UNIDADE)
                                 REFERENCES TSI_UNIDADE(CSI_CODUNI)
                                 ON UPDATE CASCADE;

                                 ALTER TABLE PNI_LOTE_UNIDADE_BLOQUEADO
                                 ADD CONSTRAINT FK_PNI_LOTE_UNIDADE_BLOQ_LOTE
                                 FOREIGN KEY (ID_LOTE)
                                 REFERENCES PNI_LOTE_PRODUTO(ID)
                                 ON UPDATE CASCADE;

                                 ALTER TABLE PNI_LOTE_UNIDADE_BLOQUEADO
                                 ADD CONSTRAINT UNQ_PNI_LOTE_UNIDADE_BLOQ
                                 UNIQUE (ID_UNIDADE,ID_LOTE);

                                 -- Migrando Lotes bloqueados para nova Tabela
                                 INSERT INTO PNI_LOTE_UNIDADE_BLOQUEADO (ID_UNIDADE, ID_LOTE)
                                 SELECT EP.ID_UNIDADE, LP.ID ID_LOTE
                                 FROM PNI_ESTOQUE_PRODUTO EP
                                 JOIN PNI_LOTE_PRODUTO LP ON (EP.LOTE = LP.LOTE AND EP.ID_PRODUTO = LP.ID_PRODUTO
                                                             AND EP.ID_PRODUTOR = LP.ID_PRODUTOR)
                                 WHERE LP.FLG_BLOQUEADO = 1;
                                 ALTER TABLE PNI_LOTE_PRODUTO DROP FLG_BLOQUEADO;",
                    codigo = 89
                });


                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE PNI_APRAZAMENTO
                                ADD DATA_ALTERACAO_SERV D_DATE;

                                ALTER TABLE PNI_VACINADOS
                                ADD UUID_REGISTRO_MOBILE D_TEXT_200;",
                    codigo = 90
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^;
                                CREATE OR ALTER TRIGGER PNI_APRAZAMENTO_BIU_SINC_MOBILE FOR PNI_APRAZAMENTO
                                ACTIVE BEFORE INSERT OR UPDATE OR DELETE POSITION 0
                                AS
                                BEGIN
                                    IF (UPDATING) THEN
                                    BEGIN
                                        NEW.DATA_ALTERACAO_SERV = (SELECT DATA_ALTERACAO_SERV
                                        FROM PRO_MOBILE_ATUALIZA_DATAS('UPDATING', OLD.DATA_ALTERACAO_SERV, NEW.DATA_ALTERACAO_SERV));
                                    END
                                    IF (INSERTING) THEN
                                    BEGIN
                                        NEW.DATA_ALTERACAO_SERV = (SELECT DATA_ALTERACAO_SERV
                                        FROM PRO_MOBILE_ATUALIZA_DATAS('INSERTING', OLD.DATA_ALTERACAO_SERV, NEW.DATA_ALTERACAO_SERV));
                                    END
                                    IF (DELETING) THEN
                                    BEGIN
                                        EXECUTE PROCEDURE PRO_MOBILE_REG_EXCLUIDOS(CURRENT_TIMESTAMP, 'PNI_APRAZAMENTO', OLD.ID, NULL, 'ID', NULL,
                                            NULL);
                                    END
                                END^
                                SET TERM ; ^",
                    codigo = 91
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"COMMENT ON COLUMN VS_ESTABELECIMENTOS.TIPO_IMOVEL IS
                                '0 = Domicilio 
                                1 = Comercio 
                                2 = Terreno Baldio 
                                3 = Ponto Estrategico 
                                4 = Escola 
                                5 = Creche 
                                6 = Abrigo 
                                7 = Instituicao de Longa Permanencia para Idosos 
                                8 = Unidade Prisional 
                                9 = Unidade de Medida Socioeducativa 
                                10 = Delegacia 
                                11 = Estabelecimento Religioso 
                                12 = Outros';
                                
                                COMMENT ON COLUMN VS_ESTABELECIMENTOS.ZONA IS
                                '0 = Urbana
                                1 = Rural';
                                
                                COMMENT ON COLUMN VS_ESTABELECIMENTOS.TIPO_ACESSO_DOMIC IS
                                '0 = Pavimento
                                1 = Chao Batido
                                2 = Fluvial
                                3 = Outro';
                                
                                COMMENT ON COLUMN VS_ESTABELECIMENTOS.MAT_PREDOMINANTE IS
                                '0 = Alvenaria/tijolo com Revestimento
                                1 = Alvenaria/tijolo sem Revestimento
                                2 = Taipa com Revestimento
                                3 = Taipa sem Revestimento
                                4 = Outros | Madeira aparelhada
                                5 = Outros | Material aproveitado
                                6 = Outros | Palha
                                7 = Outros | Outro material';
                                
                                COMMENT ON COLUMN VS_ESTABELECIMENTOS.ABASTECIMENTO_AGUA IS
                                '0 = Rede encanada ate o Domicilio
                                1 = Carro pipa
                                2 = Poco | Nascente no Domicilio
                                3 = Cisterna
                                4 = Outro';
                                
                                COMMENT ON COLUMN VS_ESTABELECIMENTOS.ESCOAMENTO_SANITA IS
                                '0 = Rede coletora de Esgoto ou Pluvial
                                1 = Fossa Septica
                                2 = Fossa Rudimentar
                                3 = Direto para um rio, lago ou mar
                                4 = Ceu aberto
                                5 = Outra forma';
                                
                                COMMENT ON COLUMN ESUS_FAMILIA.RENDA_FAMILIAR_SAL_MIN IS
                                '0 = Ate 1/4
                                1 = Ate 1/2
                                2 = Ate 1
                                3 = Ate 2
                                4 = Ate 3
                                5 = Ate 4
                                6 = Maior que 4';
                                
                                COMMENT ON COLUMN ESUS_FAMILIA.TRAT_AGUA IS
                                '0 = Filtrada
                                1 = Fervida
                                2 = Clorada
                                3 = Mineral
                                4 = Sem tratamento';
                                
                                COMMENT ON COLUMN ESUS_FAMILIA.DESTINO_LIXO IS
                                '0 = Coletado
                                1 = Queimado / Enterrado
                                2 = Ceu aberto
                                3 = Outro';
                                
                                COMMENT ON COLUMN ESUS_FAMILIA.AREA_PROD_RURAL IS
                                '0 = Proprietario
                                1 = Parceiro(a) / Meeiro(a
                                2 = Assentado(a
                                3 = Posseiro
                                4 = Arrematario
                                5 = Comodatario
                                6 = Beneficiario(a) do banco da terra
                                7 = Nao se aplica';",
                    codigo = 92
                });

                // Imunização
                itens.Add(new ScriptViewModel()
                {
                    script = $@"COMMENT ON COLUMN PNI_ACERTO_ESTOQUE.TIPO_LANCAMENTO IS
                                '0=ENTRADA|2=PERCA|3=DOACAO';",
                    codigo = 93
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@" -- Trigger: PNI_ACERTO_ESTOQUE_I_ESTOQUE
                                SET TERM ^ ;

                                CREATE OR ALTER TRIGGER PNI_ACERTO_ESTOQUE_I_ESTOQUE FOR PNI_ACERTO_ESTOQUE
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                 DECLARE VARIABLE OPERACAO INTEGER;
                                BEGIN
                                  -- OPERACAO: 0=ADD ESTOQUE | 1=REMOVE ESTOQUE
                                  :OPERACAO = 1;

                                  SELECT LOGIN FROM SEG_USUARIO WHERE ID = NEW.ID_USUARIO INTO :USUARIO;

                                  IF (CAST(NEW.TIPO_LANCAMENTO AS INTEGER) = 0) THEN
                                    :OPERACAO = 0;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                  NEW.ID_PRODUTOR, NEW.QTDE, NEW.DATA, NEW.TIPO_LANCAMENTO, :OPERACAO,
                                                                                  NEW.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);
                                END^

                                SET TERM ; ^",
                    codigo = 94
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: PNI_ACERTO_ESTOQUE_U_ESTOQUE
                                SET TERM ^ ;

                                CREATE OR ALTER trigger pni_acerto_estoque_u_estoque for pni_acerto_estoque
                                active after update position 0
                                AS
                                DECLARE VARIABLE USUARIO VARCHAR(50);
                                DECLARE VARIABLE OLD_OPERACAO INTEGER;
                                DECLARE VARIABLE NEW_OPERACAO INTEGER;
                                begin
                                  SELECT LOGIN FROM SEG_USUARIO WHERE ID = NEW.ID_USUARIO INTO :USUARIO;

                                  IF ((NEW.ID_UNIDADE <> OLD.ID_UNIDADE) OR (NEW.LOTE <> OLD.LOTE) OR
                                      (NEW.ID_PRODUTO <> OLD.ID_PRODUTO) OR (NEW.ID_PRODUTOR <> OLD.ID_PRODUTOR) OR
                                      (NEW.DATA <> OLD.DATA) OR (NEW.QTDE <> OLD.QTDE)) THEN
                                  BEGIN
                                    -- Operacao: 0=ADD ESTOQUE | 1=REMOVE ESTOQUE

                                    :OLD_OPERACAO = 1;
                                    :NEW_OPERACAO = 1;

                                    IF (CAST(OLD.TIPO_LANCAMENTO AS INTEGER) <> 0) THEN
                                      :OLD_OPERACAO = 0;

                                    IF (CAST(NEW.TIPO_LANCAMENTO AS INTEGER) = 0) THEN
                                      :NEW_OPERACAO = 0;


                                    EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                  OLD.ID_PRODUTOR, OLD.QTDE, OLD.DATA, OLD.TIPO_LANCAMENTO, :OLD_OPERACAO,
                                                                                  OLD.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);

                                    EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                  NEW.ID_PRODUTOR, NEW.QTDE, NEW.DATA, NEW.TIPO_LANCAMENTO, :NEW_OPERACAO,
                                                                                  NEW.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 95
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"ALTER TABLE CONFIGURACAO_USUARIO
                                ADD BUSCA_AUTOMATICA D_BOOLEAN
                                DEFAULT 'T';",
                    codigo = 96
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"INSERT INTO SEG_MODULOS (ID, DESCRICAO, NOME) VALUES (5, 'Atenção Básica', 'atencao_basica');
                                
                                INSERT INTO SEG_TELAS (ID, NOME, DESCRICAO, ID_MODULO)
                                VALUES((SELECT MAX(ID)+1 FROM SEG_TELAS), 'gestao_indicadores', 'Gestão de Indicadores',
                                (SELECT ID FROM SEG_MODULOS WHERE NOME = 'atencao_basica'));

                                INSERT INTO SEG_TELAS_ACOES (ID, ID_TELA, ID_ACAO)
                                VALUES((SELECT MAX(ID)+1 FROM SEG_TELAS_ACOES), (SELECT ID FROM SEG_TELAS WHERE NOME = 'gestao_indicadores'), 1);
                            ",
                    codigo = 97
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: PNI_VACINADOS_I_ESTOQUE
                                SET TERM ^ ;

                                CREATE OR ALTER TRIGGER PNI_VACINADOS_I_ESTOQUE FOR PNI_VACINADOS
                                ACTIVE AFTER INSERT POSITION 0
                                AS
                                DECLARE VARIABLE QTDE_DOSES INTEGER;
                                BEGIN
                                  IF (NEW.REGISTRO_ANTERIOR <> 'T') THEN
                                  BEGIN
                                   IF ((NEW.ID_PRODUTOR IS NOT NULL) AND (NEW.ID_PRODUTO IS NOT NULL)) THEN
                                   BEGIN
                                     SELECT D.QTDE FROM PNI_DOSE D WHERE D.ID = NEW.ID_DOSE INTO :QTDE_DOSES;

                                     EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                    NEW.ID_PRODUTOR, :QTDE_DOSES, NEW.DATA_APLICACAO, 1, 1,
                                                                                    NEW.ID, 'PNI_VACINADOS', NEW.USUARIO);
                                   END
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 98
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: PNI_VACINADOS_U_ESTOQUE
                                SET TERM ^ ;

                                CREATE OR ALTER trigger pni_vacinados_u_estoque for pni_vacinados
                                active after update position 0
                                AS
                                DECLARE VARIABLE OLD_QTDE_DOSES INTEGER;
                                DECLARE VARIABLE NEW_QTDE_DOSES INTEGER;
                                 BEGIN
                                   IF ((NEW.ID_UNIDADE <> OLD.ID_UNIDADE) OR (NEW.LOTE <> OLD.LOTE) OR
                                       (NEW.ID_PRODUTO <> OLD.ID_PRODUTO) OR (NEW.ID_PRODUTOR <> OLD.ID_PRODUTOR) OR
                                       (NEW.DATA_APLICACAO <> OLD.DATA_APLICACAO) OR (NEW.ID_DOSE <> OLD.ID_DOSE)) THEN
                                   BEGIN
                                     IF (COALESCE(OLD.REGISTRO_ANTERIOR,'F') <> 'T') THEN
                                     BEGIN
                                       SELECT D.QTDE FROM PNI_DOSE D WHERE D.ID = OLD.ID_DOSE INTO :OLD_QTDE_DOSES;
                                       SELECT D.QTDE FROM PNI_DOSE D WHERE D.ID = NEW.ID_DOSE INTO :NEW_QTDE_DOSES;

                                       UPDATE PNI_APRAZAMENTO APZ SET
                                         APZ.ID_PRODUTO = NEW.ID_PRODUTO,
                                         APZ.ID_DOSE = NEW.ID_DOSE
                                       WHERE APZ.ID_VACINADOS = NEW.ID;

                                       IF ((OLD.ID_PRODUTOR IS NOT NULL) AND (OLD.ID_PRODUTO IS NOT NULL)) THEN
                                       BEGIN
                                         EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                               OLD.ID_PRODUTOR, :OLD_QTDE_DOSES, OLD.DATA_APLICACAO, 1, 0,
                                                                               OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                       END
                                       IF ((NEW.ID_PRODUTOR IS NOT NULL) AND (NEW.ID_PRODUTO IS NOT NULL)) THEN
                                       BEGIN
                                         EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(NEW.ID_UNIDADE, NEW.LOTE, NEW.ID_PRODUTO,
                                                                                   NEW.ID_PRODUTOR, :NEW_QTDE_DOSES, NEW.DATA_APLICACAO, 1, 1,
                                                                                   NEW.ID, 'PNI_VACINADOS', NEW.USUARIO);
                                       END
                                     END
                                   END

                                   IF (COALESCE(NEW.FLG_EXCLUIDO,0) <> COALESCE(OLD.FLG_EXCLUIDO,0)) THEN
                                   BEGIN
                                     IF (COALESCE(OLD.REGISTRO_ANTERIOR,'F') <> 'T') THEN
                                     BEGIN
                                       IF (COALESCE(NEW.FLG_EXCLUIDO,0) = 1) THEN
                                       BEGIN
                                         SELECT D.QTDE FROM PNI_DOSE D WHERE D.ID = OLD.ID_DOSE INTO :OLD_QTDE_DOSES;

                                         DELETE FROM PNI_APRAZAMENTO WHERE ID_CALENDARIO_BASICO IS NULL AND ID_VACINADOS = OLD.ID;
                                         UPDATE PNI_APRAZAMENTO APZ SET APZ.ID_VACINADOS = NULL WHERE APZ.ID_VACINADOS = OLD.ID;
                                         EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                        OLD.ID_PRODUTOR, :OLD_QTDE_DOSES, OLD.DATA_APLICACAO, 1, 0,
                                                                                        OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                       END
                                       ELSE BEGIN
                                       -- VALIDAR MELHOR PARA TRATAR ESSE TIPO DE SITUAÇÃO
                                       END
                                     END
                                   END
                                END^

                                SET TERM ; ^",
                    codigo = 99
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: PNI_VACINADOS_D_ESTOQUE
                                SET TERM ^ ;

                                CREATE OR ALTER TRIGGER PNI_VACINADOS_D_ESTOQUE FOR PNI_VACINADOS
                                ACTIVE AFTER DELETE POSITION 0
                                AS
                                DECLARE VARIABLE OLD_QTDE_DOSES INTEGER;
                                BEGIN
                                  IF ((OLD.ID_PRODUTOR IS NOT NULL) AND (OLD.ID_PRODUTO IS NOT NULL)) THEN
                                  BEGIN
                                    SELECT D.QTDE FROM PNI_DOSE D WHERE D.ID = OLD.ID_DOSE INTO :OLD_QTDE_DOSES;

                                    EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                    OLD.ID_PRODUTOR, :OLD_QTDE_DOSES, OLD.DATA_APLICACAO, 1, 0,
                                                                                    OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 100
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Procedure: PNI_INSERT_MOVIMENTO_PRODUTO
                                SET TERM ^ ;

                                CREATE OR ALTER PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO (
                                    ID_UNIDADE INTEGER,
                                    LOTE VARCHAR(30),
                                    ID_PRODUTO INTEGER,
                                    ID_PRODUTOR INTEGER,
                                    QUANTIDADE INTEGER,
                                    DATA TIMESTAMP,
                                    TIPO_MOVIMENTO INTEGER,
                                    OPERACAO INTEGER,
                                    ID_ORIGEM INTEGER,
                                    TABELA_ORIGEM VARCHAR(35),
                                    USUARIO VARCHAR(50))
                                AS
                                DECLARE VARIABLE EXISTE_REGISTRO INTEGER;
                                DECLARE VARIABLE ESTOQUE_ATUAL INTEGER;
                                BEGIN
                                  -- VERIFICA SE EXISTE O REGISTRO DE ESTOQUE DO LOTE
                                  SELECT EP.ID, EP.QTDE FROM PNI_ESTOQUE_PRODUTO EP
                                    WHERE EP.ID_UNIDADE = :ID_UNIDADE
                                    AND EP.LOTE = :LOTE
                                    AND EP.ID_PRODUTO = :ID_PRODUTO
                                    AND EP.ID_PRODUTOR = :ID_PRODUTOR
                                  INTO :EXISTE_REGISTRO, :ESTOQUE_ATUAL;

                                  -- CASO NAO EXISTA O LOTE, INSERE O REGISTRO DE LOTE NA TABELA DE ESTOQUE
                                  IF (EXISTE_REGISTRO IS NULL) THEN
                                  BEGIN
                                    INSERT INTO PNI_ESTOQUE_PRODUTO (ID_UNIDADE, LOTE, ID_PRODUTO, ID_PRODUTOR, QTDE)
                                    VALUES(:ID_UNIDADE, :LOTE, :ID_PRODUTO, :ID_PRODUTOR, 0);
                                  END

                                  -- SE A DATA ESTIVER NULL, USA A DATA ATUAL
                                  IF (:DATA IS NULL) THEN
                                    :DATA = CURRENT_TIMESTAMP;

                                  INSERT INTO PNI_MOVIMENTO_PRODUTO (ID_UNIDADE, LOTE, ID_PRODUTO, ID_PRODUTOR,
                                    QTDE, DATA, TIPO_MOVIMENTO, OPERACAO,
                                    USUARIO, ID_ORIGEM, TABELA_ORIGEM,
                                    ANO_APURACAO, MES_APURACAO, ESTOQUE_ANTERIOR)
                                  VALUES(:ID_UNIDADE, :LOTE, :ID_PRODUTO, :ID_PRODUTOR, :QUANTIDADE,
                                    :DATA, :TIPO_MOVIMENTO, :OPERACAO, :USUARIO, :ID_ORIGEM, :TABELA_ORIGEM,
                                    EXTRACT(YEAR FROM :DATA), EXTRACT(MONTH FROM :DATA), :ESTOQUE_ATUAL);
                                END^

                                SET TERM ; ^",
                    codigo = 101
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: ENTRADA_PROD_UPD_DATA_MOV_PROD
                                SET TERM ^ ;

                                CREATE OR ALTER TRIGGER ENTRADA_PROD_UPD_DATA_MOV_PROD FOR PNI_ENTRADA_PRODUTO
                                ACTIVE AFTER UPDATE POSITION 0
                                AS
                                BEGIN
                                  IF (NEW.DATA <> OLD.DATA) THEN
                                  BEGIN
                                    UPDATE PNI_MOVIMENTO_PRODUTO MP
                                    SET DATA = NEW.DATA
                                    WHERE MP.TABELA_ORIGEM = 'PNI_ENTRADA_PRODUTO_ITEM'
                                    AND MP.ID_ORIGEM IN (
                                      SELECT EPI.ID FROM PNI_ENTRADA_PRODUTO_ITEM EPI WHERE EPI.ID_ENTRADA_PRODUTO = OLD.ID
                                    );
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 102
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: PNI_ENTRADA_PROD_ITEM_D_MOV
                                SET TERM ^ ;

                                CREATE OR ALTER TRIGGER PNI_ENTRADA_PROD_ITEM_D_MOV FOR PNI_ENTRADA_PRODUTO_ITEM
                                ACTIVE AFTER DELETE POSITION 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                 DECLARE VARIABLE LOTE VARCHAR(30);
                                 DECLARE VARIABLE ID_PRODUTO INT;
                                 DECLARE VARIABLE ID_PRODUTOR INT;
                                 DECLARE VARIABLE DATA TIMESTAMP;
                                BEGIN
                                  SELECT EP.USUARIO, EP.DATA
                                  FROM PNI_ENTRADA_PRODUTO EP WHERE EP.ID = OLD.ID_ENTRADA_PRODUTO
                                  INTO :USUARIO, :DATA;

                                  SELECT LP.LOTE, LP.ID_PRODUTO, LP.ID_PRODUTOR
                                  FROM PNI_LOTE_PRODUTO LP
                                  WHERE LP.ID = OLD.ID_LOTE
                                  INTO :LOTE, :ID_PRODUTO,  :ID_PRODUTOR;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, :LOTE, :ID_PRODUTO,
                                                                                 :ID_PRODUTOR, OLD.QTDE_DOSES,
                                                                                 NULL, 0, 1, OLD.ID, 'PNI_ENTRADA_PRODUTO_ITEM', :USUARIO);
                                END^

                                SET TERM ; ^",
                    codigo = 103
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: PNI_ACERTO_ESTOQUE_D_ESTOQUE
                                SET TERM ^ ;

                                CREATE OR ALTER TRIGGER PNI_ACERTO_ESTOQUE_D_ESTOQUE FOR PNI_ACERTO_ESTOQUE
                                ACTIVE AFTER DELETE POSITION 0
                                AS
                                 DECLARE VARIABLE USUARIO VARCHAR(50);
                                BEGIN
                                  SELECT LOGIN FROM SEG_USUARIO WHERE ID = OLD.ID_USUARIO INTO :USUARIO;

                                  EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                  OLD.ID_PRODUTOR, OLD.QTDE, NULL, OLD.TIPO_LANCAMENTO, 0,
                                                                                  OLD.ID, 'PNI_ACERTO_ESTOQUE', :USUARIO);
                                END^

                                SET TERM ; ^",
                    codigo = 104
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Trigger: PNI_VACINADOS_D_ESTOQUE
                                SET TERM ^ ;

                                CREATE OR ALTER TRIGGER PNI_VACINADOS_D_ESTOQUE FOR PNI_VACINADOS
                                ACTIVE AFTER DELETE POSITION 0
                                AS
                                DECLARE VARIABLE OLD_QTDE_DOSES INTEGER;
                                BEGIN
                                  IF ((OLD.ID_PRODUTOR IS NOT NULL) AND (OLD.ID_PRODUTO IS NOT NULL)) THEN
                                  BEGIN
                                    SELECT D.QTDE FROM PNI_DOSE D WHERE D.ID = OLD.ID_DOSE INTO :OLD_QTDE_DOSES;

                                    EXECUTE PROCEDURE PNI_INSERT_MOVIMENTO_PRODUTO(OLD.ID_UNIDADE, OLD.LOTE, OLD.ID_PRODUTO,
                                                                                    OLD.ID_PRODUTOR, :OLD_QTDE_DOSES, NULL, 1, 0,
                                                                                    OLD.ID, 'PNI_VACINADOS', OLD.USUARIO);
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 105
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"-- Alterando data dos movimentos de entrada produto item, para
                                -- a data escolhida na entrada produto
                                SET TERM ^ ;
                                EXECUTE BLOCK
                                AS
                                 DECLARE VARIABLE SQL VARCHAR(1000);
                                BEGIN
                                  FOR SELECT
                                  'UPDATE PNI_MOVIMENTO_PRODUTO MP SET MP.DATA = '''||CAST(EP.DATA AS DATE)||''' WHERE MP.ID = '||MP.ID||';'
                                  FROM PNI_MOVIMENTO_PRODUTO MP
                                  JOIN PNI_ENTRADA_PRODUTO_ITEM EPI ON (MP.ID_ORIGEM = EPI.ID)
                                  JOIN PNI_ENTRADA_PRODUTO EP ON (EPI.ID_ENTRADA_PRODUTO = EP.ID)
                                  WHERE MP.TABELA_ORIGEM = 'PNI_ENTRADA_PRODUTO_ITEM'
                                  AND CAST(MP.DATA AS DATE) <> CAST(EP.DATA AS DATE) INTO :SQL DO
                                  BEGIN
                                    EXECUTE STATEMENT :SQL;
                                  END
                                END^

                                SET TERM ; ^",
                    codigo = 106
                });

                //ATUALIZANDO PROCEDURE "TIRA_ACENTOS"
                itens.Add(new ScriptViewModel() {
                    script = $@"SET TERM ^ ; 
                                CREATE OR ALTER procedure TIRA_ACENTOS (
                                    DADO varchar(512) = '')
                                returns (
                                    RETORNO varchar(512))
                                as
                                declare variable LETRA varchar(1) = '';
                                BEGIN
                                   RETORNO = '';
                                   WHILE (DADO <> '') DO
                                   BEGIN
                                      SELECT CASE SUBSTRING(:DADO FROM 1 FOR 1)
                                            when 'ÃƒÂ ' then
                                                 'a'
                                            when 'ÃƒÂ¢' then
                                                 'a'
                                            when 'ÃƒÂ£' then
                                                 'a'
                                            when 'ÃƒÂ¡' then
                                                 'a'
                                            when 'ÃƒÂ€' then
                                                 'A'
                                            when 'ÃƒÂƒ' then
                                                 'A'
                                            when 'ÃƒÂ' then
                                                 'A'
                                            when 'ÃƒÂª' then
                                                 'e'
                                            when 'ÃƒÂ©' then
                                                 'e'
                                            when 'ÃƒÂŠ' then
                                                 'e'
                                            when 'ÃƒÂ‰' then
                                                 'E'
                                            when 'ÃƒÂ´' then
                                                 'o'
                                            when 'ÃƒÂµ' then
                                                 'o'
                                            when 'ÃƒÂ³' then
                                                 'o'
                                            when 'ÃƒÂ”' then
                                                 'O'
                                            when 'ÃƒÂ“' then
                                                 'O'
                                            when 'ÃƒÂ•' then
                                                 'O'
                                            when 'ÃƒÂ»' then
                                                 'u'
                                            when 'ÃƒÂº' then
                                                 'u'
                                            when 'ÃƒÂ¼' then
                                                 'u'
                                            when 'ÃƒÂ›' then
                                                 'U'
                                            when 'ÃƒÂš' then
                                                 'U'
                                            when 'ÃƒÂœ' then
                                                 'U'
                                            when 'ÃƒÂ­' then
                                                 'i'
                                            when 'ÃƒÂ' then
                                                 'I'
                                            when 'ÃƒÂ§' then
                                                 'c'
                                            when 'ÃƒÂ‡' then
                                                 'C'
                                            when 'ÃƒÂ±' then
                                                 'n'
                                            when 'ÃƒÂ‘' then
                                                 'N'
                                            ELSE
                                               SUBSTRING(:DADO FROM 1 FOR 1)
                                            END
                                      FROM RDB$DATABASE INTO :LETRA;
                                
                                      RETORNO = RETORNO || LETRA;
                                
                                      DADO  = SUBSTRING(DADO FROM 2 FOR 512);
                                   END
                                   SUSPEND;
                                END^

                                SET TERM ; ^",
                    codigo = 107
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ;
                                CREATE OR ALTER procedure INICIO_QUADRIMESTRE (
                                    QUADRIMESTRE integer,
                                    ANO integer)
                                returns (
                                    DATA date)
                                as
                                BEGIN
                                  -- SE INFORMAR QUADRIMESTRE
                                  IF (:QUADRIMESTRE IS NOT NULL) THEN
                                  BEGIN
                                    IF (:QUADRIMESTRE = 1) THEN
                                      DATA = CAST('01.01.'||:ANO AS DATE);

                                    ELSE IF (:QUADRIMESTRE = 2) THEN
                                      DATA = CAST('01.05.'||:ANO AS DATE);

                                    ELSE IF (:QUADRIMESTRE = 3) THEN
                                      DATA = CAST('01.09.'||:ANO AS DATE);
                                  END

                                  -- SE NAO INFORMAR QUADRIMESTRE
                                  ELSE
                                  BEGIN
                                    IF (CURRENT_DATE <= CAST('30.04.'||:ANO AS DATE)) THEN
                                      DATA = CAST('01.01.'||:ANO AS DATE);

                                    ELSE IF ((CURRENT_DATE > CAST('30.04.'||:ANO AS DATE)) AND
                                      (CURRENT_DATE <= CAST('31.08.'||:ANO AS DATE))) THEN
                                      DATA = CAST('01.05.'||:ANO AS DATE);

                                    ELSE IF (CURRENT_DATE > CAST('31.08.'||:ANO AS DATE)) THEN
                                      DATA = CAST('01.09.'||:ANO AS DATE);
                                  END

                                  SUSPEND;
                                END^

                                SET TERM ; ^",
                    codigo = 108
                });

                itens.Add(new ScriptViewModel()
                {
                    script = $@"SET TERM ^ ; 
                                CREATE OR ALTER procedure FECHAMENTO_QUADRIMESTRE (
                                    QUADRIMESTRE integer,
                                    ANO integer)
                                returns (
                                    DATA date)
                                as
                                BEGIN
                                  -- SE INFORMAR QUADRIMESTRE
                                  IF (:QUADRIMESTRE IS NOT NULL) THEN
                                  BEGIN
                                    IF (:QUADRIMESTRE = 1) THEN
                                      DATA = CAST('30.04.'||:ANO AS DATE);

                                    ELSE IF (:QUADRIMESTRE = 2) THEN
                                      DATA = CAST('31.08.'||:ANO AS DATE);

                                    ELSE IF (:QUADRIMESTRE = 3) THEN
                                      DATA = CAST('31.12.'||:ANO AS DATE);
                                  END

                                  -- SE NAO INFORMAR QUADRIMESTRE
                                  ELSE
                                  BEGIN
                                    IF (CURRENT_DATE <= CAST('30.04.'||:ANO AS DATE)) THEN
                                      DATA = CAST('30.04.'||:ANO AS DATE);

                                    ELSE IF ((CURRENT_DATE > CAST('30.04.'||:ANO AS DATE)) AND
                                      (CURRENT_DATE <= CAST('31.08.'||:ANO AS DATE))) THEN
                                      DATA = CAST('31.08.'||:ANO AS DATE);

                                    ELSE IF (CURRENT_DATE > CAST('31.08.'||:ANO AS DATE)) THEN
                                      DATA = CAST('31.12.'||:ANO AS DATE);
                                  END 

                                  SUSPEND;
                                END^

                                SET TERM ; ^",
                    codigo = 108
                });

                #region Script Atenção Básica

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"
                //        -- Telas
                //        INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES ((SELECT MAX(T.ID)+1 FROM SEG_TELAS T), 'Procedimento Avulso', 'procedimento_avulso', (SELECT M.ID FROM SEG_MODULOS M WHERE M.NOME = 'atencao_basica'));
                //        INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES ((SELECT MAX(T.ID)+1 FROM SEG_TELAS T), 'Atividades Coletivas', 'atividade_coletiva', (SELECT M.ID FROM SEG_MODULOS M WHERE M.NOME = 'atencao_basica'));
                //        INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES ((SELECT MAX(T.ID)+1 FROM SEG_TELAS T), 'Gestão de Famílias', 'gestao_familia', (SELECT M.ID FROM SEG_MODULOS M WHERE M.NOME = 'atencao_basica'));
                //        INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES ((SELECT MAX(T.ID)+1 FROM SEG_TELAS T), 'Ficha De Consumo Alimentar', 'consumo_alimentar', (SELECT M.ID FROM SEG_MODULOS M WHERE M.NOME = 'atencao_basica'));",
                //    codigo = 108
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(21, 'Incluir Imóvel', 'incluir_imovel');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(22, 'Editar Imóvel', 'editar_imovel');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(23, 'Vincular Família', 'vincular_familia');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(24, 'Vincular Indivíduo', 'vincular_individuo');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(25, 'Visitar Família', 'visitar_familia');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(26, 'Histórico de Visitas', 'historico_visitas');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(27, 'Família mudou dentro da área', 'familia_mudou_dentro_area');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(28, 'Família mudou para outra área', 'familia_mudou_outra_area');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(29, 'Editar Família', 'editar_familia');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(30, 'Realizar Visita Indivíduo', 'realizar_visita_individuo');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(31, 'Cartão de Vacina Indivíduo', 'cartao_vacina_individuo');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(32, 'Consumo Alimentar Indivíduo', 'consumo_alimentar_individuo');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(33, 'Definir como Responsável', 'definir_responsavel');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(34, 'Desvincular Indivíduo', 'desvincular_individuo');
                //                INSERT INTO SEG_ACOES(ID, DESCRICAO, NOME) VALUES(35, 'Ignora Parâmetro (USA_TABLET)', 'ignora_usa_tablet');",
                //    codigo = 109
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"-- Telas Ações
                //                EXECUTE PROCEDURE ATUALIZA_GENERATOR('GEN_SEG_TELAS_ACOES_ID', 'SEG_TELAS_ACOES', 'ID');

                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'procedimento_avulso'), 1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'procedimento_avulso'), 2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'atividade_coletiva'), 1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'atividade_coletiva'), 2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'atividade_coletiva'), 3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'atividade_coletiva'), 4);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'consumo_alimentar'), 3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'consumo_alimentar'), 1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'consumo_alimentar'), 2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'consumo_alimentar'), 4);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'procedimento_avulso'), 3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'procedimento_avulso'), 4);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 18);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 21);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 22);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 23);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 24);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 25);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 26);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 27);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 28);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 29);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 30);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 31);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 32);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 33);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 34);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'gestao_familia'), 35);",
                //    codigo = 110
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@" -- Ajuste em pni_acerto_estoque
                //                COMMENT ON COLUMN PNI_ACERTO_ESTOQUE.TIPO_LANCAMENTO IS
                //                    '2=PERCA|3=DOACAO|4=ENVIO|5=ENTRADA'",
                //    codigo = 111
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"ALTER TABLE ESUS_ATIVIDADE_COLETIVA
                //                ADD LOCAL_ATENDIMENTO D_INTEGER;

                //                COMMENT ON COLUMN ESUS_ATIVIDADE_COLETIVA.LOCAL_ATENDIMENTO IS
                //                '1 = Estabelecimento de Saúde|2= Escola/Creche|3 = Auditório do Município|4 = Outros Locais';

                //                CREATE GENERATOR GEN_PSE_ESCOLA_ID;

                //                CREATE TABLE PSE_ESCOLA (
                //                    ID INTEGER NOT NULL,
                //                    NOME D_NOME NOT NULL,
                //                    INEP D_TEXT_050 NOT NULL,
                //                    ID_LOGRADOURO D_INTEGER,
                //                    TELEFONE D_TEXT_015);

                //                ALTER TABLE PSE_ESCOLA
                //                ADD CONSTRAINT PK_PSE_ESCOLA
                //                PRIMARY KEY (ID);

                //                ALTER TABLE PSE_ESCOLA
                //                ADD CONSTRAINT FK_ESCOLA_LOGRADOURO
                //                FOREIGN KEY (ID_LOGRADOURO)
                //                REFERENCES TSI_LOGRADOURO (CSI_CODEND)
                //                ON UPDATE CASCADE;",
                //    codigo = 112
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"SET TERM ^;
                //                CREATE TRIGGER PSE_ESCOLA_BI FOR PSE_ESCOLA
                //                ACTIVE BEFORE INSERT POSITION 0
                //                AS
                //                BEGIN
                //                  IF (NEW.ID IS NULL) THEN
                //                    NEW.ID = GEN_ID(GEN_PSE_ESCOLA_ID,1);
                //                END^
                //                SET TERM ; ^",
                //    codigo = 113
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"ALTER TABLE ESUS_ATIVIDADE_COLETIVA
                //                ADD ID_ESCOLA D_INTEGER;

                //                ALTER TABLE ESUS_ATIVIDADE_COLETIVA
                //                ADD ID_ESTABELECIMENTO D_INTEGER;

                //                ALTER TABLE ESUS_ATIVIDADE_COLETIVA
                //                ADD CONSTRAINT FK_ESUS_ATIVIDADE_COL_EST
                //                FOREIGN KEY (ID_ESTABELECIMENTO)
                //                REFERENCES TSI_UNIDADE(CSI_CODUNI)
                //                ON UPDATE CASCADE;

                //                ALTER TABLE ESUS_ATIVIDADE_COLETIVA 
                //                ADD CONSTRAINT FK_ESUS_ATIV_COL_ESCOLA
                //                FOREIGN KEY (ID_ESCOLA) 
                //                REFERENCES PSE_ESCOLA (ID) 
                //                ON UPDATE CASCADE; ",
                //    codigo = 114
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"ALTER TABLE PSE_ESCOLA ADD CONSTRAINT UNQ1_PSE_ESCOLA_INEP UNIQUE (INEP);",
                //    codigo = 115
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"CREATE GENERATOR GEN_PSE_CICLOS_ID;

                //                CREATE TABLE PSE_CICLOS (
                //                    ID      D_INTEGER NOT NULL,
                //                    CICLO   D_TEXT_035,
                //                    INICIO  D_DATA_DATE,
                //                    FIM     D_DATA_DATE 
                //                );",
                //    codigo = 116
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"ALTER TABLE PSE_CICLOS ADD CONSTRAINT PK_PSE_CICLOS PRIMARY KEY (ID);",
                //    codigo = 117
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"SET TERM ^;
                //                CREATE OR ALTER TRIGGER PSE_CICLOS_BI FOR PSE_CICLOS
                //                ACTIVE BEFORE INSERT POSITION 0
                //                AS
                //                BEGIN
                //                  IF (NEW.ID IS NULL) THEN
                //                    NEW.ID = GEN_ID(GEN_PSE_CICLOS_ID,1);
                //                END^
                //                SET TERM ; ^",
                //    codigo = 118
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"CREATE TABLE PSE_ESCOLA_CICLOS (
                //                    ID         D_INTEGER NOT NULL,
                //                    ID_ESCOLA  D_INTEGER NOT NULL,
                //                    ID_CICLO   D_INTEGER NOT NULL
                //                );

                //                ALTER TABLE PSE_ESCOLA_CICLOS ADD CONSTRAINT UNQ1_PSE_ESCOLA_CICLOS UNIQUE (ID_ESCOLA, ID_CICLO);
                //                ALTER TABLE PSE_ESCOLA_CICLOS ADD CONSTRAINT PK_PSE_ESCOLA_CICLOS PRIMARY KEY (ID);
                //                ALTER TABLE PSE_ESCOLA_CICLOS ADD CONSTRAINT FK_PSE_ESCOLA_CICLOS_CICLO FOREIGN KEY (ID_CICLO) REFERENCES PSE_CICLOS (ID) ON UPDATE CASCADE;
                //                ALTER TABLE PSE_ESCOLA_CICLOS ADD CONSTRAINT FK_PSE_ESCOLA_CICLOS_ESCOLA FOREIGN KEY (ID_ESCOLA) REFERENCES PSE_ESCOLA (ID) ON UPDATE CASCADE;

                //                CREATE GENERATOR GEN_PSE_ESCOLA_ACOES_ID;

                //                CREATE TABLE PSE_ESCOLA_ACOES (
                //                    ID               D_INTEGER NOT NULL,
                //                    ACAO             D_INTEGER NOT NULL,
                //                    ID_ESCOLA_CICLO  D_INTEGER NOT NULL,
                //                    GRUPO            D_INTEGER NOT NULL
                //                );

                //                ALTER TABLE PSE_ESCOLA_ACOES ADD CONSTRAINT UNQ1_PSE_ESCOLA_ACOES UNIQUE (ID_ESCOLA_CICLO, ACAO);
                //                ALTER TABLE PSE_ESCOLA_ACOES ADD CONSTRAINT PK_PSE_ESCOLA_ACOES PRIMARY KEY (ID);
                //                ALTER TABLE PSE_ESCOLA_ACOES ADD CONSTRAINT FK_PSE_ESCOLA_ACOES_ESCOLA FOREIGN KEY (ID_ESCOLA_CICLO) REFERENCES PSE_ESCOLA_ACOES (ID) ON DELETE CASCADE ON UPDATE CASCADE; ",
                //    codigo = 119
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"SET TERM ^;
                //                CREATE OR ALTER TRIGGER PSE_ESCOLA_ACOES_BI FOR PSE_ESCOLA_ACOES
                //                ACTIVE BEFORE INSERT POSITION 0
                //                AS
                //                BEGIN
                //                  IF (NEW.ACAO IS NULL) THEN
                //                    NEW.ACAO = GEN_ID(GEN_PSE_ESCOLA_ACOES_ID,1);
                //                END^
                //                SET TERM ; ^",
                //    codigo = 120
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"COMMENT ON COLUMN PSE_ESCOLA_ACOES.ACAO IS 
                //                '1. A??es de combate ao mosquito Aedes aegypti;
                //                2. Promo??o das pr?ticas Corporais, da Atividade F?sica e do lazer nas escolas;
                //                3. Preven??o ao uso de ?lcool, tabaco, crack e outras drogas;
                //                4. Promo??o da Cultura de Paz, Cidadania e Direitos Humanos;
                //                5. Preven??o das viol?ncias e dos acidentes;
                //                6. Identifica??o de educandos com poss?veis sinais de agravos de doen?as em
                //                elimina??o;
                //                7. Promo??o e Avalia??o de Sa?de bucal e aplica??o t?pica de fl?or;
                //                8. Verifica??o da situa??o vacinal;
                //                9. Promo??o da seguran?a alimentar e nutricional e da alimenta??o saud?vel e
                //                preven??o da obesidade infantil;
                //                10. Promo??o da sa?de auditiva e identifica??o de educandos com
                //                poss?veis sinais de altera??o.
                //                11. Direito sexual e reprodutivo e preven??o de DST/AIDS;
                //                12. Promo??o da sa?de ocular e identifica??o de educandos com poss?veis
                //                sinais de altera??o.';

                //                COMMENT ON COLUMN PSE_ESCOLA_ACOES.GRUPO IS 
                //                '1=PRIORITARIA|2=NAO PRIORITARIA';",
                //    codigo = 121
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"CREATE TABLE FICHA_ESUS(
                //                 ID D_KEY NOT NULL,
                //                 NOME_FICHA_ESUS D_TEXT_200 NOT NULL);

                //                 ALTER TABLE FICHA_ESUS ADD CONSTRAINT PK_ficha_esus PRIMARY KEY (ID);

                //                CREATE TABLE FICHA_ESUS_CBO(
                //                  ID D_KEY NOT NULL,
                //                  ID_FICHA_ESUS D_INTEGER NOT NULL,
                //                  ID_CBO VARCHAR(6) NOT NULL);

                //                                ALTER TABLE FICHA_ESUS_CBO ADD CONSTRAINT PK_FICHA_ESUS_CBO PRIMARY KEY(ID);
                //                                ALTER TABLE FICHA_ESUS_CBO ADD CONSTRAINT FK_FEC_FE FOREIGN KEY(ID_FICHA_ESUS) REFERENCES
                //                FICHA_ESUS(ID) ON UPDATE CASCADE ON DELETE CASCADE;
                //                                ALTER TABLE FICHA_ESUS_CBO ADD CONSTRAINT FK_FEC_CBO FOREIGN KEY(ID_CBO) REFERENCES
                //                TSI_CBO(CODIGO) ON UPDATE CASCADE ON DELETE CASCADE;

                //                CREATE SEQUENCE GEN_FICHA_ESUS_CBO_ID; ",
                //    codigo = 122
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"SET TERM ^;
                //                CREATE TRIGGER FICHA_ESUS_CBO_BI FOR FICHA_ESUS_CBO
                //                ACTIVE BEFORE INSERT POSITION 0
                //                AS
                //                BEGIN
                //                  IF (NEW.ID IS NULL) THEN
                //                    NEW.ID = GEN_ID(GEN_FICHA_ESUS_CBO_ID,1);
                //                END^
                //                SET TERM ; ^",
                //    codigo = 123
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (1,'DADOS_CADASTRO_CIDADAO');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (2,'FICHA_DE_CADASTRO_INDIVIDUAL');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (3,'FICHA_DE_CADASTRO_DOMICILIAR');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (4,'FICHA_DE_ATENDIMENTO_INDIVIDUAL');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (5,'FICHA_DE_ATENDIMENTO_ODONTOLOGICO');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (6,'FICHA_DE_ATIVIDADE_COLETIVA');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (7,'FICHA_DE_PROCEDIMENTOS');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (8,'FICHA_DE_VISITA_DOMICILIAR');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (10,'FICHA_DE_ATENDIMENTO_DOMICILIAR');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (11,'FICHA_DE_AVALIACAO_ELEGIBILIDADE');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (12,'FICHA_MARCADOR_CONSUMO_ALIMENTAR');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (13,'FICHA_COMPLEMENTAR_SINDROME_NEUROLOGICA_ZICA');
                //                INSERT INTO FICHA_ESUS(ID,NOME_FICHA_ESUS) VALUES (14,'FICHA_DE_VACINACAO');",
                //    codigo = 124
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'515105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'515310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'515140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'352210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'515130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'515124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'226310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'411010');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223204');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223208');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223280');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223284');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223212');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223216');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223224');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223228');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223276');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223288');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223232');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223236');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223240');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223244');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223248');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223252');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223256');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223264');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223268');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223272');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223293');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'412110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'131205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'515305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'226315');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'131210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'422215');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'226305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'226320');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'422220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'226110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'142340');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'239415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'234410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'22410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'239425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'226105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'422110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'422105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'1312C1');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'131225');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322245');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'322425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'422205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'422210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (2,'515120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'515105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'515310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'515140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'352210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'515130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'515125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'226310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223204');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223208');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223280');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223284');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223212');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223216');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223224');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223228');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223276');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223288');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223232');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223236');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223240');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223244');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223248');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223252');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223256');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223264');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223268');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223272');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223293');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'131205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'515305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'226315');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'131210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'226305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'226320');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'239415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'234410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'22410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'239425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'422110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'422105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'1312C1');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'131225');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322245');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'322425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (3,'515120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'226305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'226110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'22410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'234410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'226105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (4,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'322415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'322430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223204');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223208');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223280');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223284');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223212');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223216');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223224');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223228');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223276');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223288');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223232');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223236');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223240');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223244');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223248');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223252');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223256');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223264');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223268');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223272');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'223293');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'322405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (5,'322425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'515105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'515310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'515140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'352210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'515130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'515125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'226310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223204');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223208');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223280');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223284');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223212');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223216');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223224');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223228');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223276');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223288');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223232');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223236');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223240');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223244');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223248');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223252');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223256');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223264');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223268');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223272');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223293');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'131205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'515305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'226315');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'131210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'226305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'226320');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'226110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'239415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'234410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'22410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'239425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'226105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'1312C1');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'131225');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322245');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'322125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (6,'515120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'515105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'515310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'515140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'515130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'515125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'226310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'322230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'322250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'515305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'226315');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'226305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'226320');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'226110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'239415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'22410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'239425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'226105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'322205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'322245');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'322210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'324205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'322125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (7,'515120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (8,'515105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (8,'515310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (8,'515140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (8,'515130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (8,'515125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (8,'515120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'515105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'515310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'515140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'515130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'515125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223204');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223208');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223280');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223284');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223212');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223216');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223224');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223228');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223276');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223288');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223232');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223236');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223240');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223244');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223248');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223256');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223252');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223264');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223268');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223272');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223293');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'515305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'226305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'226110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'239415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'234410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'22410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251504');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'239425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'226105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322245');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'322425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (12,'515120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223660');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223655');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223630');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'223635');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225115');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225225');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225203');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225150');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'2231F8');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225320');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'2231A2');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (11,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'322230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'322250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223204');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223208');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223293');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223272');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223280');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223212');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223276');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223288');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223232');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223236');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223240');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223248');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223256');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223264');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223268');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223660');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223655');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223630');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223635');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223815');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223830');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223840');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225115');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225225');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225203');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225150');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'2231F8');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225320');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'2231A2');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'226305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251520');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251525');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'322205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'322245');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'322210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'322405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'322425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (10,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223650');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223605');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223810');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223710');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'226110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'22410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251510');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'251530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'226105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (13,'223905');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'515110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322235');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223505');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223565');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223520');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223525');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223530');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223535');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223540');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223545');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223550');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223555');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223560');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223405');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223415');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223430');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223445');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223425');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223410');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225105');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225110');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225148');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225151');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225115');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225154');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225290');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225122');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225120');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225295');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225215');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225220');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225225');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225230');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225235');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225240');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225125');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225280');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225142');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225130');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225135');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225140');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225203');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225310');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225145');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225150');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225315');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225320');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225155');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225160');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225165');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225170');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225175');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225180');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225250');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225185');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225340');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225345');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225195');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225103');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225106');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225255');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225109');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225260');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225350');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225112');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225118');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225265');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225121');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225270');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225275');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225325');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225335');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225124');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225127');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225133');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225330');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'2231F9');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225136');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225139');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'225285');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'223305');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322205');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322245');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322210');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322215');
                //                INSERT INTO FICHA_ESUS_CBO (ID_FICHA_ESUS,ID_CBO) VALUES (14,'322220');",
                //    codigo = 125
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO SEG_TELAS (ID, DESCRICAO, NOME, ID_MODULO) VALUES ((SELECT MAX(T.ID)+1 FROM SEG_TELAS T), 'Escola', 'escola', 3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'escola'), 1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'escola'), 2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'escola'), 3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'escola'), 4);",
                //    codigo = 126
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"EXECUTE PROCEDURE ATUALIZA_GENERATOR('GEN_SEG_MODULOS_ID', 'SEG_MODULOS', 'ID');
                //                EXECUTE PROCEDURE ATUALIZA_GENERATOR('GEN_SEG_TELAS_ID', 'SEG_TELAS', 'ID');
                //                EXECUTE PROCEDURE ATUALIZA_GENERATOR('GEN_SEG_ACOES_ID', 'SEG_ACOES', 'ID');
                //                EXECUTE PROCEDURE ATUALIZA_GENERATOR('GEN_SEG_TELAS_ACOES_ID', 'SEG_TELAS_ACOES', 'ID');",
                //    codigo = 127
                //});


                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO SEG_TELAS (DESCRICAO, NOME, ID_MODULO) VALUES ('Ficha De Atendimento Odontologico Individual', 'atendimento_odontologico', 5);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT ID FROM SEG_TELAS WHERE NOME = 'atendimento_odontologico' AND ID_MODULO = 5),1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT ID FROM SEG_TELAS WHERE NOME = 'atendimento_odontologico' AND ID_MODULO = 5),2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT ID FROM SEG_TELAS WHERE NOME = 'atendimento_odontologico' AND ID_MODULO = 5),3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT ID FROM SEG_TELAS WHERE NOME = 'atendimento_odontologico' AND ID_MODULO = 5),4);",
                //    codigo = 128
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"ALTER TABLE PEP_EXAME_FISICO ADD VALOR_IMC D_NUMERICO;",
                //    codigo = 129
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO SEG_TELAS (DESCRICAO, NOME, ID_MODULO) VALUES ('Exame Físico', 'exame_fisico', 5);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'exame_fisico' AND T.ID_MODULO = 5), 1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'exame_fisico' AND T.ID_MODULO = 5), 2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'exame_fisico' AND T.ID_MODULO = 5), 3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'exame_fisico' AND T.ID_MODULO = 5), 4);",
                //    codigo = 130
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO SEG_TELAS (DESCRICAO, NOME, ID_MODULO) VALUES ('Visita Domiciliar', 'visita_domiciliar', 5);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'visita_domiciliar' AND T.ID_MODULO = 5), 1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'visita_domiciliar' AND T.ID_MODULO = 5), 2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'visita_domiciliar' AND T.ID_MODULO = 5), 3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES ((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'visita_domiciliar' AND T.ID_MODULO = 5), 4);",
                //    codigo = 131
                //});

                //itens.Add(new ScriptViewModel()
                //{
                //    script = $@"INSERT INTO SEG_TELAS (DESCRICAO, NOME, ID_MODULO) VALUES('Calendario de Atendimentos', 'calendario_atendimentos', 5);

                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'calendario_atendimentos' AND T.ID_MODULO = 5),1);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'calendario_atendimentos' AND T.ID_MODULO = 5),2);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'calendario_atendimentos' AND T.ID_MODULO = 5),3);
                //                INSERT INTO SEG_TELAS_ACOES (ID_TELA, ID_ACAO) VALUES((SELECT T.ID FROM SEG_TELAS T WHERE T.NOME = 'calendario_atendimentos' AND T.ID_MODULO = 5),4);",
                //    codigo = 132
                //});
                #endregion

                return itens.Where(x => x.codigo > ultimoscript).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ScriptViewModel> GetScriptBancoFotos(int ultimoscript)
        {
            try
            {
                var itens = new List<ScriptViewModel>();



                return itens.Where(x => x.codigo > ultimoscript).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
