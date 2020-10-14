using PontoDeVenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoDeVenda.Control
{
    public class ControllerSabor : ModelSabor
    {
        public ControllerSabor() : base() { }

        public ControllerSabor(string ds_sabor, bool incluir) : base(ds_sabor, incluir) { }

        public ControllerSabor(int id_sabor, string ds_sabor) : base(id_sabor, ds_sabor) { }

        public ControllerSabor(int id_sabor) : base(id_sabor) { }
    }
}