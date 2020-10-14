using PontoDeVenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoDeVenda.Control
{
    public class ControlSabor : ModelSabor
    {
        public ControlSabor() : base() { }

        public ControlSabor(string ds_sabor, bool incluir) : base(ds_sabor, incluir) { }

        public ControlSabor(int id_sabor, string ds_sabor) : base(id_sabor, ds_sabor) { }

        public ControlSabor(int id_sabor) : base(id_sabor) { }
    }
}