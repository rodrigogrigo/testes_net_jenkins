using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Interfaces.Imunizacao
{
    public interface IImunobiologicoPacienteReport
    {
        string data_imunizacao { get; set; }
        string imunobiologico { get; set; }
        string dose { get; set; }
        string profissional { get; set; }
        int id_paciente { get; set; }
        string nome_paciente { get; set; }
        int idade { get; set; }
        string lote { get; set; }
    }
}
