using PontoDeVenda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoDeVenda.Control
{
    public class ControlSubcategoria : ModelSubcategoria
    {
        public ControlSubcategoria() { }

        public ControlSubcategoria(string nm_subcategoria, string ds_subcategoria) : base(nm_subcategoria, ds_subcategoria) { }

        public ControlSubcategoria(int id_subcategoria, string nm_subcategoria, string ds_subcategoria) :
            base(id_subcategoria, nm_subcategoria, ds_subcategoria) { }

        public ControlSubcategoria(string nm_subcategoria) : base(nm_subcategoria) { }

        public ControlSubcategoria(int id_subcategoria) : base(id_subcategoria) { }
    }
}