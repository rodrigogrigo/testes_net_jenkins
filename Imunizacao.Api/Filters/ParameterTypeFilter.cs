using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.Filters
{
    public class ParameterTypeFilter : TypeFilterAttribute
    {
        public ParameterTypeFilter(string para1) : base(typeof(PermissaoUsuarioFilter))
        {

            Arguments = new object[] { para1 };
        }
    }
}
