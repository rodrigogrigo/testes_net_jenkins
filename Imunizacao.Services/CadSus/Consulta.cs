using RgCidadao.Services.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace RgCidadao.Services.CadSus
{
    public class Consulta
    {
        public List<PacientesCNSViewModel> ConsultaDadosCidadao(string nome, string cns, string sexo, string datanasc, string nomemae, string LoginCadSus, string SenhaCadSus)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(datanasc))
                    datanasc = Convert.ToDateTime(datanasc).ToString("dd/MM/yyyy");

                //string path = Directory.GetParent(Directory.GetCurrentDirectory()) + $"\\Imunizacao.Services\\CadSus\\Servico\\RGSvrCadSus_.jar";
                //var caminho = Directory.GetParent(Directory.GetCurrentDirectory()) + $"\\Imunizacao.Services\\CadSus\\Servico\\Consultas\\ConsultaCNS{DateTime.Now.ToString("yyyyMMdd HHmmss")}.txt";

                string path = Directory.GetParent(Directory.GetCurrentDirectory()) + $"\\RgCidadao.Services\\CadSus\\Servico\\RGSvrCadSus_.jar";
                var caminho = Directory.GetParent(Directory.GetCurrentDirectory()) + $"\\RgCidadao.Services\\CadSus\\Servico\\Consultas\\ConsultaCNS{DateTime.Now.ToString("yyyyMMdd HHmmss")}.txt";

                if (!Directory.Exists(path))
                    throw new Exception("Sem conexão com o servidor.");

                var comando = "java -jar " + '"' + path + '"' + " "
                                           + '"' + caminho + '"' + " " +
                                           '"' + cns + '"' + " " + //cns
                                           '"' + sexo + '"' + " " + //sexo
                                           '"' + nome?.ToUpper() + '"' + " " +//nome cidadão
                                           '"' + "" + '"' + " " +//cpf
                                           '"' + nomemae?.ToUpper() + '"' + " " + // nome mae
                                           '"' + datanasc + '"' + " " + //data de nascimento
                                           '"' + LoginCadSus + '"' + " " +  //login
                                           '"' + SenhaCadSus + '"'; //senha  

                string saida = ExecutarCMD(comando);

                var lista = new List<PacientesCNSViewModel>();
                if (System.IO.File.Exists(caminho))
                {

                    var texto = System.IO.File.ReadAllText(caminho);
                    dynamic retorno = JsonConvert.DeserializeObject(texto);

                    foreach (var item in retorno)
                    {
                        try
                        {
                            var erro = item.erro;
                            if (erro != null)
                                throw new Exception();
                        }
                        catch (Exception)
                        {
                            throw new Exception("O Web Service pode estar indisponível ou suas credenciais estão incorretas.");
                        }

                        var model = new PacientesCNSViewModel();

                        //contato
                        var contato = new contatos
                        {
                            contato0 = item.contatos.contato0
                        };
                        model.contatos = contato;

                        //endereço
                        model.endereco.uf = item.endereco.uf;
                        model.endereco.cidade = item.endereco.cidade;
                        model.endereco.nomeRua = item.endereco.nomeRua;
                        model.endereco.nCasa = item.endereco.nCasa;
                        model.endereco.bairro = item.endereco.bairro;
                        model.endereco.tipoRua = item.endereco.tipoRua;
                        model.endereco.cep = item.endereco.cep;
                        model.endereco.pais = item.endereco.pais;

                        //cns
                        foreach (var itemcns in item.cns)
                        {
                            var modelcns = new cns
                            {
                                Tipo = itemcns.Tipo,
                                CNS = itemcns.CNS
                            };
                            model.cns.Add(modelcns);
                        }

                        if (item.rg.rg != null)
                        {
                            //rg
                            model.rg.rg = item.rg.rg;
                            model.rg.ufemissaorg = item.rg.ufemissaorg;
                            model.rg.orgaoemissorrg = item.rg.orgaoemissorrg;
                            model.rg.dataEmissaoRg = item.rg.dataEmissaoRg;
                        }

                        if (item.cnh.uf != null)
                        {
                            model.cnh.uf = item.cnh.uf;
                        }

                        model.nome = item.nome;
                        //model.passaporte = item.passaporte == null ? string.Empty : item.passaporte;
                        model.racaCor = item.racaCor;
                        model.cpf = item.cpf;
                        model.nomePai = item.nomePai;
                        //model.ctps = item.ctps;
                        //model.tituloEleitor = item.tituloEleitor;
                        model.sexo = item.sexo;
                        model.dataNascimento = item.dataNascimento;
                        model.nomeMae = item.nomeMae;

                        lista.Add(model);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ExecutarCMD(string comando)
        {
            using (Process processo = new Process())
            {
                processo.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");

                // Formata a string para passar como argumento para o cmd.exe
                processo.StartInfo.Arguments = string.Format("/c {0}", comando);

                processo.StartInfo.RedirectStandardOutput = true;
                processo.StartInfo.UseShellExecute = false;
                processo.StartInfo.CreateNoWindow = true;

                processo.Start();
                processo.WaitForExit();

                string saida = processo.StandardOutput.ReadToEnd();
                return saida;
            }
        }
    }
}
