﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmsWebService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Name="ReluzCap Web ServiceSoap", Namespace="https://www.twwwireless.com.br/reluzcap/wsreluzcap", ConfigurationName="SmsWebService.ReluzCapWebServiceSoap")]
    public interface ReluzCapWebServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS2SN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, string Celular, string Mensagem, System.DateTime Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSQuebra", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoSemAcento", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSConcatenadoSemAcentoAsync(string NumUsu, string Senha, string SeuNum, int Serie, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoSemAcento2N" +
            "", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSConcatenadoSemAcento2NAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, int Serie, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoComAcento", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSConcatenadoComAcentoAsync(string NumUsu, string Senha, string SeuNum, int Serie, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSConcatenadoComAcento2N" +
            "", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSConcatenadoComAcento2NAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, int Serie, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAlt", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSAltAsync(string user, string pwd, string msgid, string phone, string msgtext);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAge", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSAgeAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTemplate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSTemplateAsync(string NumUsu, string Senha, string Template, string SeuNum, string Celular, string VarNome, string Var1, string Var2, string Var3, string Var4, string Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAgeQuebra", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSAgeQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSDataSetAsync(string NumUsu, string Senha, SmsWebService.ArrayOfXElement DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSXMLAsync(string NumUsu, string Senha, string StrXML);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTIM", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<System.Xml.Linq.XElement> EnviaSMSTIMAsync(string XMLString);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSOTA8Bit", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSOTA8BitAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Header, string Data);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSESMDCS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> EnviaSMSESMDCSAsync(string NumUsu, string Senha, string SeuNum, string Celular, string ESM, string DCS, string Header, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMSAsync(string NumUsu, string Senha, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS2SN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMSDataSetAsync(string NumUsu, string Senha, SmsWebService.ArrayOfXElement DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMO", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSMOAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgenda", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSAgendaAsync(string NumUsu, string Senha, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgendaDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSAgendaDataSetAsync(string NumUsu, string Senha, SmsWebService.ArrayOfXElement DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgenda", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> DelSMSAgendaAsync(string NumUsu, string Senha, System.DateTime Agendamento, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgendaIdLote", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> DelSMSAgendaIdLoteAsync(string NumUsu, string Senha, string IdLote);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/AlteraSenha", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<bool> AlteraSenhaAsync(string NumUsu, string SenhaAntiga, string SenhaNova);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerBL", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> VerBLAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/InsBL", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> InsBLAsync(string NumUsu, string Senha, string Celular);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerCredito", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> VerCreditoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerValidade", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<System.DateTime> VerValidadeAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaStatusLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> ResetaStatusLidoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaMOLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> ResetaMOLidoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSNaoLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMSNaoLidoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSMONaoLidoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLidoQuant", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<SmsWebService.MONLIDOQUANT> BuscaSMSMONaoLidoQuantAsync(string NumUsu, string Senha, int Quant);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www.twwwireless.com.br/reluzcap/wsreluzcap")]
    public partial class MONLIDOQUANT
    {
        
        private int qUANTNLField;
        
        private SmsWebService.ArrayOfXElement dsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int QUANTNL
        {
            get
            {
                return this.qUANTNLField;
            }
            set
            {
                this.qUANTNLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public SmsWebService.ArrayOfXElement DS
        {
            get
            {
                return this.dsField;
            }
            set
            {
                this.dsField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface ReluzCapWebServiceSoapChannel : SmsWebService.ReluzCapWebServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class ReluzCapWebServiceSoapClient : System.ServiceModel.ClientBase<SmsWebService.ReluzCapWebServiceSoap>, SmsWebService.ReluzCapWebServiceSoap
    {
        
        public ReluzCapWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem)
        {
            return base.Channel.EnviaSMSAsync(NumUsu, Senha, SeuNum, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, string Celular, string Mensagem, System.DateTime Agendamento)
        {
            return base.Channel.EnviaSMS2SNAsync(NumUsu, Senha, SeuNum1, SeuNum2, Celular, Mensagem, Agendamento);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem)
        {
            return base.Channel.EnviaSMSQuebraAsync(NumUsu, Senha, SeuNum, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSConcatenadoSemAcentoAsync(string NumUsu, string Senha, string SeuNum, int Serie, string Celular, string Mensagem)
        {
            return base.Channel.EnviaSMSConcatenadoSemAcentoAsync(NumUsu, Senha, SeuNum, Serie, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSConcatenadoSemAcento2NAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, int Serie, string Celular, string Mensagem)
        {
            return base.Channel.EnviaSMSConcatenadoSemAcento2NAsync(NumUsu, Senha, SeuNum1, SeuNum2, Serie, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSConcatenadoComAcentoAsync(string NumUsu, string Senha, string SeuNum, int Serie, string Celular, string Mensagem)
        {
            return base.Channel.EnviaSMSConcatenadoComAcentoAsync(NumUsu, Senha, SeuNum, Serie, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSConcatenadoComAcento2NAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, int Serie, string Celular, string Mensagem)
        {
            return base.Channel.EnviaSMSConcatenadoComAcento2NAsync(NumUsu, Senha, SeuNum1, SeuNum2, Serie, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAltAsync(string user, string pwd, string msgid, string phone, string msgtext)
        {
            return base.Channel.EnviaSMSAltAsync(user, pwd, msgid, phone, msgtext);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAgeAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento)
        {
            return base.Channel.EnviaSMSAgeAsync(NumUsu, Senha, SeuNum, Celular, Mensagem, Agendamento);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSTemplateAsync(string NumUsu, string Senha, string Template, string SeuNum, string Celular, string VarNome, string Var1, string Var2, string Var3, string Var4, string Agendamento)
        {
            return base.Channel.EnviaSMSTemplateAsync(NumUsu, Senha, Template, SeuNum, Celular, VarNome, Var1, Var2, Var3, Var4, Agendamento);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAgeQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento)
        {
            return base.Channel.EnviaSMSAgeQuebraAsync(NumUsu, Senha, SeuNum, Celular, Mensagem, Agendamento);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSDataSetAsync(string NumUsu, string Senha, SmsWebService.ArrayOfXElement DS)
        {
            return base.Channel.EnviaSMSDataSetAsync(NumUsu, Senha, DS);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSXMLAsync(string NumUsu, string Senha, string StrXML)
        {
            return base.Channel.EnviaSMSXMLAsync(NumUsu, Senha, StrXML);
        }
        
        public System.Threading.Tasks.Task<System.Xml.Linq.XElement> EnviaSMSTIMAsync(string XMLString)
        {
            return base.Channel.EnviaSMSTIMAsync(XMLString);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSOTA8BitAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Header, string Data)
        {
            return base.Channel.EnviaSMSOTA8BitAsync(NumUsu, Senha, SeuNum, Celular, Header, Data);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSESMDCSAsync(string NumUsu, string Senha, string SeuNum, string Celular, string ESM, string DCS, string Header, string Mensagem)
        {
            return base.Channel.EnviaSMSESMDCSAsync(NumUsu, Senha, SeuNum, Celular, ESM, DCS, Header, Mensagem);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMSAsync(string NumUsu, string Senha, string SeuNum)
        {
            return base.Channel.StatusSMSAsync(NumUsu, Senha, SeuNum);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2)
        {
            return base.Channel.StatusSMS2SNAsync(NumUsu, Senha, SeuNum1, SeuNum2);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMSDataSetAsync(string NumUsu, string Senha, SmsWebService.ArrayOfXElement DS)
        {
            return base.Channel.StatusSMSDataSetAsync(NumUsu, Senha, DS);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim)
        {
            return base.Channel.BuscaSMSAsync(NumUsu, Senha, DataIni, DataFim);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSMOAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim)
        {
            return base.Channel.BuscaSMSMOAsync(NumUsu, Senha, DataIni, DataFim);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSAgendaAsync(string NumUsu, string Senha, string SeuNum)
        {
            return base.Channel.BuscaSMSAgendaAsync(NumUsu, Senha, SeuNum);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSAgendaDataSetAsync(string NumUsu, string Senha, SmsWebService.ArrayOfXElement DS)
        {
            return base.Channel.BuscaSMSAgendaDataSetAsync(NumUsu, Senha, DS);
        }
        
        public System.Threading.Tasks.Task<string> DelSMSAgendaAsync(string NumUsu, string Senha, System.DateTime Agendamento, string SeuNum)
        {
            return base.Channel.DelSMSAgendaAsync(NumUsu, Senha, Agendamento, SeuNum);
        }
        
        public System.Threading.Tasks.Task<string> DelSMSAgendaIdLoteAsync(string NumUsu, string Senha, string IdLote)
        {
            return base.Channel.DelSMSAgendaIdLoteAsync(NumUsu, Senha, IdLote);
        }
        
        public System.Threading.Tasks.Task<bool> AlteraSenhaAsync(string NumUsu, string SenhaAntiga, string SenhaNova)
        {
            return base.Channel.AlteraSenhaAsync(NumUsu, SenhaAntiga, SenhaNova);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> VerBLAsync(string NumUsu, string Senha)
        {
            return base.Channel.VerBLAsync(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<int> InsBLAsync(string NumUsu, string Senha, string Celular)
        {
            return base.Channel.InsBLAsync(NumUsu, Senha, Celular);
        }
        
        public System.Threading.Tasks.Task<int> VerCreditoAsync(string NumUsu, string Senha)
        {
            return base.Channel.VerCreditoAsync(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<System.DateTime> VerValidadeAsync(string NumUsu, string Senha)
        {
            return base.Channel.VerValidadeAsync(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<string> ResetaStatusLidoAsync(string NumUsu, string Senha)
        {
            return base.Channel.ResetaStatusLidoAsync(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<string> ResetaMOLidoAsync(string NumUsu, string Senha)
        {
            return base.Channel.ResetaMOLidoAsync(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> StatusSMSNaoLidoAsync(string NumUsu, string Senha)
        {
            return base.Channel.StatusSMSNaoLidoAsync(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.ArrayOfXElement> BuscaSMSMONaoLidoAsync(string NumUsu, string Senha)
        {
            return base.Channel.BuscaSMSMONaoLidoAsync(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<SmsWebService.MONLIDOQUANT> BuscaSMSMONaoLidoQuantAsync(string NumUsu, string Senha, int Quant)
        {
            return base.Channel.BuscaSMSMONaoLidoQuantAsync(NumUsu, Senha, Quant);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
    }
    
    [System.Xml.Serialization.XmlSchemaProviderAttribute(null, IsAny=true)]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class ArrayOfXElement : object, System.Xml.Serialization.IXmlSerializable
    {
        
        private System.Collections.Generic.List<System.Xml.Linq.XElement> nodesList = new System.Collections.Generic.List<System.Xml.Linq.XElement>();
        
        public ArrayOfXElement()
        {
        }
        
        public virtual System.Collections.Generic.List<System.Xml.Linq.XElement> Nodes
        {
            get
            {
                return this.nodesList;
            }
        }
        
        public virtual System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new System.NotImplementedException();
        }
        
        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            System.Collections.Generic.IEnumerator<System.Xml.Linq.XElement> e = nodesList.GetEnumerator();
            for (
            ; e.MoveNext(); 
            )
            {
                ((System.Xml.Serialization.IXmlSerializable)(e.Current)).WriteXml(writer);
            }
        }
        
        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            for (
            ; (reader.NodeType != System.Xml.XmlNodeType.EndElement); 
            )
            {
                if ((reader.NodeType == System.Xml.XmlNodeType.Element))
                {
                    System.Xml.Linq.XElement elem = new System.Xml.Linq.XElement("default");
                    ((System.Xml.Serialization.IXmlSerializable)(elem)).ReadXml(reader);
                    Nodes.Add(elem);
                }
                else
                {
                    reader.Skip();
                }
            }
        }
    }
}
