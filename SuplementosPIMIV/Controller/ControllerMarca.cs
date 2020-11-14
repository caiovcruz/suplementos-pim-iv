using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerMarca : ModelMarca
    {
        public ControllerMarca(string connectionString) : base(connectionString) { }

        public ControllerMarca(string nm_marca, string connectionString) : base(nm_marca, connectionString) { }

        public ControllerMarca(int id_marca, string nm_marca, string connectionString) : base(id_marca, nm_marca, connectionString) { }

        public ControllerMarca(int id_marca, char acao, string connectionString) : base(id_marca, acao, connectionString) { }
    }
}