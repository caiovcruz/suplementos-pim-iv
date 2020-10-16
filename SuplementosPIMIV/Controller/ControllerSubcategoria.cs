﻿using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSubcategoria : ModelSubcategoria
    {
        public ControllerSubcategoria() { }

        public ControllerSubcategoria(int id_categoria, string nm_subcategoria, string ds_subcategoria) : 
            base(id_categoria, nm_subcategoria, ds_subcategoria) { }

        public ControllerSubcategoria(int id_subcategoria, int id_categoria, string nm_subcategoria, string ds_subcategoria) :
            base(id_subcategoria, id_categoria, nm_subcategoria, ds_subcategoria) { }

        public ControllerSubcategoria(string nm_subcategoria) : base(nm_subcategoria) { }

        public ControllerSubcategoria(int id_subcategoria) : base(id_subcategoria) { }
    }
}