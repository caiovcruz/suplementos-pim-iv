using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerCategoria : ModelCategoria
    {
        public ControllerCategoria() { }

        public ControllerCategoria(string nm_categoria, string ds_categoria) : base(nm_categoria, ds_categoria) { }

        public ControllerCategoria(int id_categoria, string nm_categoria, string ds_categoria) : 
            base(id_categoria, nm_categoria, ds_categoria) { }

        public ControllerCategoria(string nm_categoria) : base(nm_categoria) { }

        public ControllerCategoria(int id_categoria) : base(id_categoria) { }
    }
}