using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.Enums
{
    public static class SituacaoGestaoFamilia
    {
        [Description("visitados")]
        public static int visitados = 0;

        [Description("nao_visitados")]
        public static int nao_visitados = 1;

        [Description("ausentes_recusados")]
        public static int ausentes_recusados = 2;
    }
}
