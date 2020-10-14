using DataBase;
using System;
using System.Data;

namespace PontoDeVenda
{
    public class ModelLogin
    {
        private DAO dataAcessObject;

        public string DS_Usuario { get; set; }
        public string DS_Senha { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelLogin(string ds_usuario, string ds_senha)
        {
            DS_Usuario = ds_usuario;
            DS_Senha = ds_senha;
        }

        private void SetConnection()
        {
            dataAcessObject = new DAO();
            dataAcessObject.Setup(DataBase.DatabaseTypes.MySql,
                "Persist Security Info=False; " +
                "Server=localhost; " +
                "Database=ponto_de_venda; " +
                "Uid=root; " +
                "Pwd=011118");
        }

        public string Acessar()
        {
            DS_Mensagem = "";

            if (DS_Mensagem == "")
            {
                DataTable dataTable = new DataTable();

                SetConnection();
                if (dataAcessObject.Connector.Open())
                {
                    string SQLSelect = "SELECT DS_Usuario, DS_Senha FROM tb_login " +
                        "WHERE DS_Usuario = '" + DS_Usuario + "' AND DS_Senha = '" + DS_Senha + "'";
                    IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                    dataTable.TableName = "tb_login";
                    dataTable.Load(dataReader);

                    if (dataTable.Rows.Count > 0)
                    {
                        DS_Mensagem = "Ok";
                    }
                    else
                    {
                        DS_Mensagem = "Login inválido";
                    }
                }
            }

            return DS_Mensagem;
        }
    }
}