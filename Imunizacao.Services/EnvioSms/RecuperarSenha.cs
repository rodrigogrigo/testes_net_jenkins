using RgCidadao.Services.ViewModels;
using SmsWebService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RgCidadao.Services.EnvioSms
{
    public static class RecuperarSenha
    {
        //wsdl para conexão com web service
        //http://webservices.twwwireless.com.br/reluzcap/wsreluzcap.asmx?WSDL
        public static Task<string> SmsContato(ViewModels.SmsViewModel model)
        {
            try
            {
                var address = new EndpointAddress("http://webservices.twwwireless.com.br/reluzcap/");
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                binding.MaxReceivedMessageSize = 768000;

                var sms = new ReluzCapWebServiceSoapClient(binding, address);
                var retorno = sms.EnviaSMSAsync("rgsystem", "sys2727", "127", $"55{model.numero}", model.mensagem);
                return retorno;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
