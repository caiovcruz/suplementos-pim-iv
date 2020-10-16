using SuplementosPIMIV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuplementosPIMIV.Controller
{
    public class ControllerMarca : ModelMarca
    {
        public ControllerMarca() : base() { }

        public ControllerMarca(string nm_marca, bool incluir) : base(nm_marca, incluir) { }

        public ControllerMarca(int id_marca, string nm_marca) : base(id_marca, nm_marca) { }

        public ControllerMarca(int id_marca) : base(id_marca) { }
    }
}