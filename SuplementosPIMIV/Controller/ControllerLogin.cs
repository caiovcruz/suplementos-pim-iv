using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerLogin : ModelLogin
    {
        public ControllerLogin(string ds_usuario, string ds_senha, string connectionString) : base(ds_usuario, ds_senha, connectionString) { }
    }
}