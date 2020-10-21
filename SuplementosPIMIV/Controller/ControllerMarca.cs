using SuplementosPIMIV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuplementosPIMIV.Controller
{
    public class ControllerMarca : ModelMarca
    {
        public ControllerMarca(string connectionString) : base(connectionString) { }

        public ControllerMarca(string nm_marca, bool incluir, string connectionString) : base(nm_marca, incluir, connectionString) { }

        public ControllerMarca(int id_marca, string nm_marca, string connectionString) : base(id_marca, nm_marca, connectionString) { }

        public ControllerMarca(int id_marca, string connectionString) : base(id_marca, connectionString) { }
    }
}