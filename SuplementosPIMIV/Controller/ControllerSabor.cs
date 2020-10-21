using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSabor : ModelSabor
    {
        public ControllerSabor(string connectionString) : base(connectionString) { }

        public ControllerSabor(string nm_sabor, bool incluir, string connectionString) : base(nm_sabor, incluir, connectionString) { }

        public ControllerSabor(int id_sabor, string nm_sabor, string connectionString) : base(id_sabor, nm_sabor, connectionString) { }

        public ControllerSabor(int id_sabor, string connectionString) : base(id_sabor, connectionString) { }
    }
}