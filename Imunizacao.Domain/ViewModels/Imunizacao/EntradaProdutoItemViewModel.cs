using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Imunizacao
{
    public class EntradaProdutoItem
    {
      public int id { get; set; }
      public int id_unidade { get; set; }
      public DateTime validade { get; set; }
      public string lote { get; set; }
      public int id_produtor { get; set; }
      public int id_produto { get; set; }
    }
    
}
