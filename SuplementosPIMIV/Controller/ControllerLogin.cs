using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerLogin : ModelLogin
    {
        public ControllerLogin(string connectionString) : base(connectionString) { }
        public ControllerLogin(int id_nivelAcesso, int id_funcionario, string ds_usuario, string ds_senha, string connectionString) :
            base(id_nivelAcesso, id_funcionario, ds_usuario, ds_senha, connectionString) { }
        public ControllerLogin(int id_login, int id_nivelAcesso, int id_funcionario, string ds_usuario, string ds_senha, string connectionString) :
            base(id_login, id_nivelAcesso, id_funcionario, ds_usuario, ds_senha, connectionString) { }
        public ControllerLogin(int id_login, string connectionString) : base(id_login, connectionString) { }
        public ControllerLogin(string ds_usuario, string ds_senha, string connectionString) : base(ds_usuario, ds_senha, connectionString) { }
    }
}