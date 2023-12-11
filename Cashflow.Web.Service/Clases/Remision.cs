using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cashflow.Web.Service.Clases
{
    public class Remision
    {
        public string ClaveUsuario
        {
            get;
            set;
        }


        public string Contrasena
        {
            get;
            set;
        }

        public string ClaveSucursal
        {
            get;
            set;
        }


        public string NumeroRemision
        {
            get;
            set;
        }


        public string ClaveCliente
        {
            get;
            set;
        }

        public bool OK { set; get; }
        public string Respuesta { set; get; }
    }
}