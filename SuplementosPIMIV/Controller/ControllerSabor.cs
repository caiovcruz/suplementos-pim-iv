using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSabor : ModelSabor
    {
        public ControllerSabor(string connectionString) : base(connectionString) { }

        public ControllerSabor(string nm_sabor, string connectionString) : base(nm_sabor, connectionString) { }

        public ControllerSabor(int id_sabor, string nm_sabor, string connectionString) : base(id_sabor, nm_sabor, connectionString) { }

        public ControllerSabor(int id_sabor, char acao, string connectionString) : base(id_sabor, acao, connectionString) { }
    }
}