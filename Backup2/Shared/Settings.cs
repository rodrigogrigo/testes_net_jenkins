using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.Infra.Shared
{
    public static class Settings
    {
        //MUNICÍPIO - DEBUGGER
        public static string DefaultConnection = @"initial catalog=localhost:D:\Desenvolvimento\Bancos\Cidadao\JAGUARE\JAGUARE.FDB;User Id=sysdba;Password=masterkey;Charset=ISO8859_1";
        public static string DefaultConnectionteste = @"initial catalog=localhost:D:\iisProjetos\bancos\bancos\JAGUARE - Copia\JAGUARE - Copia.FDB;User Id=sysdba;Password=masterkey;Charset=ISO8859_1";
        public static string DefaultSDNConnection = @"initial catalog=localhost:D:\Desenvolvimento\Bancos\Cidadao\SAODOMINGOS.FDB;User Id=sysdba;Password=masterkey;Charset=ISO8859_1";
        public static string DefaultConceicaoConnection = @"initial catalog=localhost:D:\Desenvolvimento\Bancos\CONCEICAO_BARRA\CONCEICAO_BARRA\C_BARRA.FDB;User Id=sysdba;Password=masterkey;Charset=ISO8859_1";

        //MUNICÍPIO - RELEASE
        public static string AfonsoClaudioConnection = $@"Server=www.afonsoclaudio.es.gov.br;Database=C:\RG System\BD_Cidadao\AFONSOCLAUDIO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AguaDoceNorteConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\AGUADOCEDONORTE\AGUADOCENORTE.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AguiaBrancaConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\AguiaBranca\AGUIABRANCA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AlegreConnection = $@"Server=189.113.6.42;Database=D:\BD\ALEGRE\ALEGRE.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AlfredoChavesConnection = $@"Server=189.113.15.234;Database=D:\RG System\BD\ALFREDOCHAVES\ALFREDOCHAVES.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AltoRioNovoConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\AltoRioNovo\ALTORIONOVO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AnchietaConnection = $@"Server=189.113.15.234;Database=D:\RG System\BD\ANCHIETA\ANCHIETA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string ApiacaConnection = $@"Server=189.113.6.42;Database=D:\BD\APIACA\APIACA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AracruzConnection = $@"Server=177.70.4.134;Database=D:\RG SYSTEM\BD\ARACRUZ\ARACRUZ.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string AtilioVivacquaConnection = $@"Server=189.113.6.42;Database=D:\BD\ATILIOVIVACQUA\ATILIOVIVACQUA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string BaixoGuanduConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\BaixoGuandu\BAIXOGUANDU.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string BSFConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\BarraSaoFrancisco\BARRASAOFRANCISCO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string BoaEsperancaConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\BOAESPERANCA\BOAESPERANCA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string BomJesusdoNorteConnection = $@"Server=189.113.6.42;Database=D:\BD\BOMJESUSDONORTE\BOMJESUSDONORTE.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string BrejetubaConnection = $@"Server=189.113.6.43;Database=D:\BD\Brejetuba\BREJETUBA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string CachoeiroConnection = $@"Server=189.113.6.42;Database=D:\BD\CACHOEIRODEITAPEMIRIM\CACHOEIRODEITAPEMIRIM.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string CasteloConnection = $@"Server=189.113.6.42;Database=D:\BD\CASTELO\CASTELO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string ConceicaoBarraConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\CONCEICAODABARRA\CONCEICAO_BARRA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string ConceicaoCasteloConnection = $@"Server=189.113.6.43;Database=D:\BD\Conceicao_do_Castelo\CONCEICAOCASTELO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string DivinoSaoLourencoConnection = $@"Server=189.113.6.42;Database=D:\BD\DIVINOSAOLOURENCO\DIVINOSAOLOURENCO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string DomingosMartinsConnection = $@"Server=189.113.6.43;Database=D:\BD\Domingos Martins\DOMINGOSMARTINS.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string DoresdoRioPretoConnection = $@"Server=189.113.6.42;Database=D:\BD\DORESDORIOPRETO\DORESDORIOPRETO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string EcoporangaConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\ECOPORANGA\ECOPORANGA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string FundaoConnection = $@"Server=177.70.4.134;Database=D:\RG SYSTEM\BD\FUNDAO\FUNDAO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string GovernadorLindembergConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\GovernadorLindenberg\GOVERNADORLINDENBERG.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string GuacuiConnection = $@"Server=189.113.6.42;Database=D:\BD\GUACUI\GUACUI.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string GuarapariConnection = $@"Server=192.168.0.117;Database=C:\RG System\GUARAPARI\GUARAPARI.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string IbatibaConnection = $@"Server=189.113.6.43;Database=D:\BD\Ibatiba\IBATIBA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string IbiracuConnection = $@"Server=177.70.4.134;Database=D:\RG SYSTEM\BD\IBIRACU\IBIRACU.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string IbitiramaConnection = $@"Server=189.113.6.43;Database=D:\BD\Ibitirama\IBITIRAMA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string IconhaConnection = $@"Server=189.113.15.234;Database=D:\RG System\BD\ICONHA\ICONHA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string IrupiConnection = $@"Server=189.113.6.43;Database=D:\BD\Irupi\IRUPI.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string ItaguacuGuanduConnection = $@"Server=189.113.6.43;Database=D:\BD\Itaguacu\ITAGUACU.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string ItapemirimConnection = $@"Server=189.113.15.234;Database=D:\RG System\BD\ITAPEMIRIM\ITAPEMIRIM.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string ItaranaConnection = $@"Server=189.113.6.43;Database=D:\BD\Itarana\ITARANA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string IunaConnection = $@"Server=189.113.6.43;Database=D:\BD\Iuna\IUNA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string JaguareConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\JAGUARE\JAGUARE.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string JeronimoMonteiroConnection = $@"Server=189.113.6.42;Database=D:\BD\JERONIMOMONTEIRO\JERONIMOMONTEIRO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string JoaoNeivaConnection = $@"Server=177.70.4.134;Database=D:\RG System\BD\JoaoNeiva\JOAONEIVA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string LaranjaTerraConnection = $@"Server=168.197.176.42;Database=C:\BDs\BD\LARANJADATERRA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string LinharesConnection = $@"Server=177.70.4.134;Database=D:\RG System\BD\Linhares\LINHARES.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MantenaConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\Mantena\MANTENA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MantenopolesSulConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\Mantenopolis\MANTENOPOLIS.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MarataizesConnection = $@"Server=189.113.15.234;Database=D:\RG System\BD\MARATAIZES\MARATAIZES.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MarechalFlorianoConnection = $@"Server=189.113.6.43;Database=D:\BD\Marechal Floriano\MARECHALFLORIANO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MarilandiaConnection = $@"Server=177.92.128.176;Database=C:\RG System\BD\MARILANDIA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MimosodoSulConnection = $@"Server=189.113.6.42;Database=D:\BD\MIMOSODOSUL\MIMOSODOSUL.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MontanhaConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\MONTANHA\MONTANHA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MucuriciConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\MUCURICI\MUCURICI.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MunizFreireConnection = $@"Server=189.113.6.43;Database=D:\BD\MunizFreire\MUNIZFREIRE.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string MuquiConnection = $@"Server=189.113.6.42;Database=D:\BD\MUQUI\MUQUI.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string NovaVeneciaConnection = $@"Server=201.59.100.252;Database=C:\RGSystem\BD\NOVAVENECIA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string PancasConnection = $@"Server=200.195.33.130;Database=C:\RG System\BD\PANCAS.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string PedroCanarioConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\PEDROCANARIO\PEDRO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string PinheirosConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\PINHEIROS\PINHEIROS.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string PiumaConnection = $@"Server=189.113.15.234;Database=D:\RG System\BD\PIUMA\PIUMA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string PontoBeloConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\PONTOBELO\PONTOBELO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string PresidenteKennedyConnection = $@"Server=189.113.6.42;Database=D:\BD\PRESIDENTEKENNEDY\PRESIDENTEKENNEDY.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string RioBananalConnection = $@"Server=177.70.4.134;Database=D:\RG System\BD\RioBananal\RIOBANANAL.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string RioNovoSulConnection = $@"Server=189.113.15.234;Database=D:\RG System\BD\RIONOVODOSUL\RIONOVODOSUL.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SaoDomingosConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\SaoDomingosdoNorte\SAODOMINGOSDONORTE.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SgpConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\SGP\SGP.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SaoJosedosCalcadosConnection = $@"Server=189.113.6.42;Database=D:\BD\SAOJOSEDOCALCADO\SAOJOSEDOCALCADO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SaoMateusConnection = $@"Server=177.70.7.254;Database=D:\RGSystem\BD\BD\SAOMATEUS\SAOMATEUS.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SaoRoqueDoCanaaConnection = $@"Server=177.70.4.134;Database=D:\RG SYSTEM\BD\SAOROQUEDOCANAA\SAOROQUEDOCANAA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SantaLeopoldinaConnection = $@"Server=177.70.4.134;Database=D:\RG SYSTEM\BD\SANTALEOPOLDINA\SANTALEOPOLDINA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SantaMariaConnection = $@"Server=177.71.35.91;Database=E:\BD\SMJ.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SantaTeresaConnection = $@"Server=177.70.4.134;Database=D:\RG SYSTEM\BD\SANTATERESA\SANTATERESA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string SooretamaConnection = $@"Server=177.70.4.134;Database=D:\RG SYSTEM\BD\SOORETAMA\SOORETAMA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string VargemAltaConnection = $@"Server=189.113.6.42;Database=D:\BD\VARGEMALTA\VARGEMALTA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string VendaNovaConnection = $@"Server=177.223.238.195;Database=C:\RG System\BD\VENDANOVA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string VianaConnection = $@"Server=177.154.162.253:62666;Database=E:\RGSystem\BD\VIANA\VIANA.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string VilaPavaoConnection = $@"Server=189.113.6.44;Database=H:\Servidor\BD\VilaPavao\VILAPAVAO.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";
        public static string VilaValerioConnection = $@"Server=177.54.110.55;Database=C:\RGSystem\BD\SAUDEVV.FDB;User Id=SYSDBA;Password=masterkey;Charset=ISO8859_1";

        //MUNICÍPIO FOTOS - DEBUGGER
        public static string DefaultFotosConnection = $@"initial catalog=localhost:D:\Desenvolvimento\Bancos\Cidadao\SAODOMINGOSFOTOS.FDB;User Id=sysdba;Password=masterkey;Charset=ISO8859_1";
        public static string DefaultConceicaoFotosConnection = @"initial catalog=localhost:D:\Desenvolvimento\Bancos\CONCEICAO_BARRA\CONCEICAO_BARRA\C_BARRA_FOTOS.FDB;User Id=sysdba;Password=masterkey;Charset=ISO8859_1";
    }
}
