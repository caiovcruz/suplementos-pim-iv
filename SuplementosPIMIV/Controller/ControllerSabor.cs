using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerSabor : ModelSabor
    {
        public ControllerSabor() : base() { }

        public ControllerSabor(string ds_sabor, bool incluir) : base(ds_sabor, incluir) { }

        public ControllerSabor(int id_sabor, string ds_sabor) : base(id_sabor, ds_sabor) { }

        public ControllerSabor(int id_sabor) : base(id_sabor) { }
    }
}