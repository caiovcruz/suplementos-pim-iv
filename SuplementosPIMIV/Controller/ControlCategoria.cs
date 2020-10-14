using PontoDeVenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoDeVenda.Control
{
    public class ControlCategoria : ModelCategoria
    {
        public ControlCategoria() { }

        public ControlCategoria(string nm_categoria, string ds_categoria) : base(nm_categoria, ds_categoria) { }

        public ControlCategoria(int id_categoria, string nm_categoria, string ds_categoria) : 
            base(id_categoria, nm_categoria, ds_categoria) { }

        public ControlCategoria(string nm_categoria) : base(nm_categoria) { }

        public ControlCategoria(int id_categoria) : base(id_categoria) { }
    }
}