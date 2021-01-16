using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class HistoricoExameFisicoPac
    {
        public int? id { get; set; }
        public string descricao { get; set; }
        public double? valor { get; set; }
        public DateTime? data { get; set; }
        public int? id_profissional { get; set; }
        public string profissional { get; set; }
        public string pressaoarterial { get; set; }

        public decimal? pressaoArterialSistolica { get; set; }

        public decimal? pressaoArterialDiastolica { get; set; }
    }

    //public class HistoricoExameFisicoPac
    //{
    //    public HistoricoExameFisicoPac()
    //    {
    //        peso = new List<HistoricoPesoPac>();
    //        altura = new List<HistoricoAlturaPac>();
    //    }

    //    public List<HistoricoPesoPac> peso { get; set; }
    //    public List<HistoricoAlturaPac> altura { get; set; }
    //}

    public class HistoricoPesoPac
    {
        public DateTime? data_peso { get; set; }
        public double peso { get; set; }
    }

    public class HistoricoAlturaPac
    {
        public DateTime? data_altura { get; set; }
        public double altura { get; set; }
    }

    public class HistoricoImcPac
    {
        public DateTime? data_imc { get; set; }
        public double imc { get; set; }
    }
}
