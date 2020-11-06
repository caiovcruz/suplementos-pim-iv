using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerCategoria : ModelCategoria
    {
        public ControllerCategoria(string connectionString) : base(connectionString) { }

        public ControllerCategoria(string nm_categoria, string ds_categoria, string connectionString) : 
            base(nm_categoria, ds_categoria, connectionString) { }

        public ControllerCategoria(int id_categoria, string nm_categoria, string ds_categoria, string connectionString) : 
            base(id_categoria, nm_categoria, ds_categoria, connectionString) { }

        public ControllerCategoria(int id_categoria, char acao, string connectionString) : base(id_categoria, acao, connectionString) { }
    }
}