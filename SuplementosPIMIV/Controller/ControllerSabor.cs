using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSabor : ModelSabor
    {
        public ControllerSabor() : base() { }

        public ControllerSabor(string nm_sabor, bool incluir) : base(nm_sabor, incluir) { }

        public ControllerSabor(int id_sabor, string nm_sabor) : base(id_sabor, nm_sabor) { }

        public ControllerSabor(int id_sabor) : base(id_sabor) { }
    }
}