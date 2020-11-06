using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSubcategoria : ModelSubcategoria
    {
        public ControllerSubcategoria(string connectionString) : base(connectionString) { }

        public ControllerSubcategoria(int id_categoria, string nm_subcategoria, string ds_subcategoria, string connectionString) : 
            base(id_categoria, nm_subcategoria, ds_subcategoria, connectionString) { }

        public ControllerSubcategoria(int id_subcategoria, int id_categoria, string nm_subcategoria, string ds_subcategoria, string connectionString) :
            base(id_subcategoria, id_categoria, nm_subcategoria, ds_subcategoria, connectionString) { }

        public ControllerSubcategoria(int id_subcategoria, char acao, string connectionString) : base(id_subcategoria, acao, connectionString) { }
    }
}