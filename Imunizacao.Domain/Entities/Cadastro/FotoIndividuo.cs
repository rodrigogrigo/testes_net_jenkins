using System;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class FotoIndividuo
    {
        public int csi_id { get; set; }
        public int csi_matricula { get; set; }
        public string csi_tipo { get; set; }
        public byte[] csi_foto { get; set; }
        public DateTime data_alteracao { get; set; }
    }
}
