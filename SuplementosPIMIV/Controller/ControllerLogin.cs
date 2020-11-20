using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerLogin : ModelLogin
    {
        public ControllerLogin(string connectionString) : base(connectionString) { }
        public ControllerLogin(int id_funcionario, string ds_nivelAcesso, string ds_usuario, string ds_senha, string connectionString) :
            base(id_funcionario, ds_nivelAcesso, ds_usuario, ds_senha, connectionString) { }
        public ControllerLogin(int id_login, int id_funcionario, string ds_nivelAcesso, string ds_usuario, string ds_senha, string connectionString) :
            base(id_login, id_funcionario, ds_nivelAcesso, ds_usuario, ds_senha, connectionString) { }
        public ControllerLogin(int id_login, char acao, string connectionString) : base(id_login, acao, connectionString) { }
        public ControllerLogin(string ds_usuario, string ds_senha, string connectionString) : base(ds_usuario, ds_senha, connectionString) { }
    }
}