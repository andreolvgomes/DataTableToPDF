using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class DecimalExtensions
    {
        public static decimal ToDecimal(this object value)
        {
            string retorno = string.Empty;
            decimal testador = 0;
            retorno = value != null ? value.ToString() : "0";
            decimal.TryParse(retorno, out testador);
            return testador;
        }
    }
}
