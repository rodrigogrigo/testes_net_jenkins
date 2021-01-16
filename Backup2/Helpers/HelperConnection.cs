using FirebirdSql.Data.FirebirdClient;
using Imunizacao.Domain.Infra.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.Infra.Helpers
{
    public static class HelperConnection
    {
        public static void ExecuteCommand(string ibge, Action<FbConnection> task)
        {
            var connStr = GetConnectionMunicipio(ibge);
            using (var conn = new FbConnection(connStr))
            {
                conn.Open();
                task(conn);
            }
        }

        public static T ExecuteCommand<T>(string ibge, Func<FbConnection, T> task)
        {
            //recupera conexão do município a partir do ibge informado
            var connStr = GetConnectionMunicipio(ibge);
            using (var conn = new FbConnection(connStr))
            {
                conn.Open();

                return task(conn);
            }
        }

        public static void ExecuteCommandFoto(string ibge, Action<FbConnection> task)
        {
            var connStr = GetConnectionMunicipioFoto(ibge);
            using (var conn = new FbConnection(connStr))
            {
                conn.Open();
                task(conn);
            }
        }

        public static T ExecuteCommandFoto<T>(string ibge, Func<FbConnection, T> task)
        {
            //recupera conexão do município a partir do ibge informado
            var connStr = GetConnectionMunicipioFoto(ibge);
            using (var conn = new FbConnection(connStr))
            {
                conn.Open();

                return task(conn);
            }
        }

        public static string GetConnectionMunicipio(string ibge)
        {
            string stringconnection = string.Empty;
//#if DEBUG
            switch (ibge)
            {
                case "3203056":
                    stringconnection = Settings.DefaultConnection;
                    break;
                case "3204658":
                    stringconnection = Settings.DefaultSDNConnection;
                    break;
                case "3203908":
                    stringconnection = Settings.DefaultConnectionteste;
                    break;
                case "3201605":
                    stringconnection = Settings.DefaultConceicaoConnection;
                    break;

            }
//#else
//            switch (ibge)
//            {
//                case "3203056":
//                    stringconnection = Settings.JaguareConnection;
//                    break;
//                case "3203908":
//                    stringconnection = Settings.NovaVeneciaConnection;
//                    break;
//                case "3200904":
//                    stringconnection = Settings.BSFConnection;
//                    break;
//                case "3201001":
//                    stringconnection = Settings.BoaEsperancaConnection;
//                    break;
//                case "3201605":
//                    stringconnection = Settings.ConceicaoBarraConnection;
//                    break;
//                case "3202108":
//                    stringconnection = Settings.EcoporangaConnection;
//                    break;
//                case "3203502":
//                    stringconnection = Settings.MontanhaConnection;
//                    break;
//                case "3203601":
//                    stringconnection = Settings.MucuriciConnection;
//                    break;
//                case "3204054":
//                    stringconnection = Settings.PedroCanarioConnection;
//                    break;
//                case "3204104":
//                    stringconnection = Settings.PinheirosConnection;
//                    break;
//                case "3204252":
//                    stringconnection = Settings.PontoBeloConnection;
//                    break;
//                case "3204906":
//                    stringconnection = Settings.SaoMateusConnection;
//                    break;
//                case "3205150":
//                    stringconnection = Settings.VilaPavaoConnection;
//                    break;
//                case "3200169":
//                    stringconnection = Settings.AguaDoceNorteConnection;
//                    break;
//                case "3200359":
//                    stringconnection = Settings.AltoRioNovoConnection;
//                    break;
//                case "3200136":
//                    stringconnection = Settings.AguiaBrancaConnection;
//                    break;
//                case "3202256":
//                    stringconnection = Settings.GovernadorLindembergConnection;
//                    break;
//                case "3203130":
//                    stringconnection = Settings.JoaoNeivaConnection;
//                    break;
//                case "3203205":
//                    stringconnection = Settings.LinharesConnection;
//                    break;
//                case "3203353":
//                    stringconnection = Settings.MarilandiaConnection;
//                    break;
//                case "3204351":
//                    stringconnection = Settings.RioBananalConnection;
//                    break;
//                case "3205010":
//                    stringconnection = Settings.SooretamaConnection;
//                    break;
//                case "3205176":
//                    stringconnection = Settings.VilaValerioConnection;
//                    break;
//                case "3200201":
//                    stringconnection = Settings.AlegreConnection;
//                    break;
//                case "3200508":
//                    stringconnection = Settings.ApiacaConnection;
//                    break;
//                case "3201100":
//                    stringconnection = Settings.BomJesusdoNorteConnection;
//                    break;
//                case "3201803":
//                    stringconnection = Settings.DivinoSaoLourencoConnection;
//                    break;
//                case "3202009":
//                    stringconnection = Settings.DoresdoRioPretoConnection;
//                    break;
//                case "3202553":
//                    stringconnection = Settings.IbitiramaConnection;
//                    break;
//                case "3202652":
//                    stringconnection = Settings.IrupiConnection;
//                    break;
//                case "3203007":
//                    stringconnection = Settings.IunaConnection;
//                    break;
//                case "3203106":
//                    stringconnection = Settings.JeronimoMonteiroConnection;
//                    break;
//                case "3203403":
//                    stringconnection = Settings.MimosodoSulConnection;
//                    break;
//                case "3203700":
//                    stringconnection = Settings.MunizFreireConnection;
//                    break;
//                case "3203809":
//                    stringconnection = Settings.MuquiConnection;
//                    break;
//                case "3204807":
//                    stringconnection = Settings.SaoJosedosCalcadosConnection;
//                    break;
//                case "3200607":
//                    stringconnection = Settings.AracruzConnection;
//                    break;
//                case "3202207":
//                    stringconnection = Settings.FundaoConnection;
//                    break;
//                case "3202504":
//                    stringconnection = Settings.IbiracuConnection;
//                    break;
//                case "3204609":
//                    stringconnection = Settings.SantaTeresaConnection;
//                    break;
//                case "3204500":
//                    stringconnection = Settings.SantaLeopoldinaConnection;
//                    break;
//                case "3204955":
//                    stringconnection = Settings.SaoRoqueDoCanaaConnection;
//                    break;
//                case "3200805":
//                    stringconnection = Settings.BaixoGuanduConnection;
//                    break;
//                case "3203304":
//                    stringconnection = Settings.MantenopolesSulConnection;
//                    break;
//                case "3204658":
//                    stringconnection = Settings.SaoDomingosConnection;
//                    break;
//                case "3204708":
//                    stringconnection = Settings.SgpConnection;
//                    break;
//                case "3200102":
//                    stringconnection = Settings.AfonsoClaudioConnection;
//                    break;
//                case "3201159":
//                    stringconnection = Settings.BrejetubaConnection;
//                    break;
//                case "3201704":
//                    stringconnection = Settings.ConceicaoCasteloConnection;
//                    break;
//                case "3201902":
//                    stringconnection = Settings.DomingosMartinsConnection;
//                    break;
//                case "3202454":
//                    stringconnection = Settings.IbatibaConnection;
//                    break;
//                case "3202702":
//                    stringconnection = Settings.ItaguacuGuanduConnection;
//                    break;
//                case "3202900":
//                    stringconnection = Settings.ItaranaConnection;
//                    break;
//                case "3203163":
//                    stringconnection = Settings.LaranjaTerraConnection;
//                    break;
//                case "3203346":
//                    stringconnection = Settings.MarechalFlorianoConnection;
//                    break;
//                case "3205069":
//                    stringconnection = Settings.VendaNovaConnection;
//                    break;
//                case "3205101":
//                    stringconnection = Settings.VianaConnection;
//                    break;
//                case "3200706":
//                    stringconnection = Settings.AtilioVivacquaConnection;
//                    break;
//                case "3201209":
//                    stringconnection = Settings.CachoeiroConnection;
//                    break;
//                case "3201407":
//                    stringconnection = Settings.CasteloConnection;
//                    break;
//                case "3202306":
//                    stringconnection = Settings.GuacuiConnection;
//                    break;
//                case "3204302":
//                    stringconnection = Settings.PresidenteKennedyConnection;
//                    break;
//                case "3205036":
//                    stringconnection = Settings.VargemAltaConnection;
//                    break;
//                case "3200409":
//                    stringconnection = Settings.AnchietaConnection;
//                    break;
//                case "3202405":
//                    stringconnection = Settings.GuarapariConnection;
//                    break;
//                case "3202603":
//                    stringconnection = Settings.IconhaConnection;
//                    break;
//                case "3202801":
//                    stringconnection = Settings.ItapemirimConnection;
//                    break;
//                case "3203320":
//                    stringconnection = Settings.MarataizesConnection;
//                    break;
//                case "3204203":
//                    stringconnection = Settings.PiumaConnection;
//                    break;
//                case "3204401":
//                    stringconnection = Settings.RioNovoSulConnection;
//                    break;
//                case "3200300":
//                    stringconnection = Settings.AlfredoChavesConnection;
//                    break;
//                case "3204559":
//                    stringconnection = Settings.SantaMariaConnection;
//                    break;
//                case "3204005":
//                    stringconnection = Settings.PancasConnection;
//                    break;
//            }
//#endif
            return stringconnection;
        }

        public static string GetConnectionMunicipioFoto(string ibge)
        {
            string stringconnection = string.Empty;

            switch (ibge)
            {
                case "3203056":
                    stringconnection = Settings.DefaultFotosConnection;
                    break;
                case "3204658":
                    stringconnection = Settings.DefaultFotosConnection;
                    break;
                case "3201605":
                    stringconnection = Settings.DefaultConceicaoFotosConnection;
                    break;
            }
            return stringconnection;
        }
    }
}
