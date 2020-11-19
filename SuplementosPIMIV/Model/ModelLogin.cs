using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelLogin
    {
        public int ID_Login { get; set; }
        public int ID_NivelAcesso { get; set; }
        public int ID_Funcionario { get; set; }
        public string NM_FuncionarioLogin { get; set; }
        public string DS_Usuario { get; set; }
        public string DS_Senha { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelLogin(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelLogin(int id_nivelAcesso, int id_funcionario, string ds_usuario, string ds_senha, string connectionString)
        {
            ID_NivelAcesso = id_nivelAcesso;
            ID_Funcionario = id_funcionario;
            DS_Usuario = ds_usuario;
            DS_Senha = ds_senha;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelLogin(int id_login, int id_nivelAcesso, int id_funcionario, string ds_usuario, string ds_senha, string connectionString)
        {
            ID_Login = id_login;
            ID_NivelAcesso = id_nivelAcesso;
            ID_Funcionario = id_funcionario;
            DS_Usuario = ds_usuario;
            DS_Senha = ds_senha;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelLogin(int id_login, string connectionString)
        {
            ID_Login = id_login;
            ConnectionString = connectionString;

            Excluir();
        }

        public ModelLogin(string ds_usuario, string ds_senha, string connectionString)
        {
            DS_Usuario = ds_usuario;
            DS_Senha = ds_senha;
            ConnectionString = connectionString;
        }

        public void Incluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO TB_Login (");
                stringSQL.Append("ID_NivelAcesso, ");
                stringSQL.Append("ID_Funcionario, ");
                stringSQL.Append("DS_Usuario, ");
                stringSQL.Append("DS_Senha)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_NivelAcesso + "', ");
                stringSQL.Append("'" + ID_Funcionario + "', ");
                stringSQL.Append("'" + DS_Usuario + "', ");
                stringSQL.Append("'" + DS_Senha + "')");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao cadastrar";
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public void Alterar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Login SET ");
                stringSQL.Append("ID_NivelAcesso = '" + ID_NivelAcesso + "', ");
                stringSQL.Append("DS_Usuario = '" + DS_Usuario + "', ");
                stringSQL.Append("DS_Senha = '" + DS_Senha + "' ");
                stringSQL.Append("WHERE ID_Login = '" + ID_Login + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao alterar";
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public DataTable Consultar(string texto)
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("LOG.ID_Funcionario, ");
                stringSQL.Append("FUN.NM_Funcionario, ");
                stringSQL.Append("LOG.ID_NivelAcesso, ");
                stringSQL.Append("NAC.DS_NivelAcesso, ");
                stringSQL.Append("LOG.DS_Usuario, ");
                stringSQL.Append("LOG.DS_Senha ");
                stringSQL.Append("FROM TB_Login AS LOG ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN ON LOG.ID_Funcionario = FUN.ID_Funcionario ");
                stringSQL.Append("INNER JOIN TB_NivelAcesso AS NAC ON LOG.ID_NivelAcesso = NAC.ID_NivelAcesso ");
                stringSQL.Append("WHERE FUN.NM_Funcionario LIKE '" + texto + "' + '%' ");
                stringSQL.Append("ORDER BY ID_Login DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return dataTable;
        }

        public void Excluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("DELETE FROM TB_Login ");
                stringSQL.Append("WHERE ID_Login = '" + ID_Login + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao excluir";
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public DataTable Exibir()
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("LOG.ID_Funcionario, ");
                stringSQL.Append("FUN.NM_Funcionario, ");
                stringSQL.Append("LOG.ID_NivelAcesso, ");
                stringSQL.Append("NAC.DS_NivelAcesso, ");
                stringSQL.Append("LOG.DS_Usuario, ");
                stringSQL.Append("LOG.DS_Senha ");
                stringSQL.Append("FROM TB_Login AS LOG ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN ON LOG.ID_Funcionario = FUN.ID_Funcionario ");
                stringSQL.Append("INNER JOIN TB_NivelAcesso AS NAC ON LOG.ID_NivelAcesso = NAC.ID_NivelAcesso ");
                stringSQL.Append("ORDER BY ID_Login DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return dataTable;
        }

        public string VerificarLoginCadastrado(string id_funcionario, string ds_usuario)
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("1 ");
                stringSQL.Append("FROM TB_Login ");
                stringSQL.Append("WHERE ID_Funcionario != '" + id_funcionario + "' ");
                stringSQL.Append("AND DS_Usuario = '" + ds_usuario + "' ");
                stringSQL.Append("ORDER BY ID_Login DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "Este nome de usuário já está cadastrado.";
                }
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return DS_Mensagem;
        }

        public string Acessar()
        {
            DS_Mensagem = "";
            ID_NivelAcesso = 0;
            NM_FuncionarioLogin = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("LOG.ID_Login, ");
                stringSQL.Append("LOG.ID_NivelAcesso, ");
                stringSQL.Append("FUN.NM_Funcionario ");
                stringSQL.Append("FROM TB_Login AS LOG ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN ON LOG.ID_Funcionario = FUN.ID_Funcionario ");
                stringSQL.Append("WHERE DS_Usuario = '" + DS_Usuario + "' ");
                stringSQL.Append("AND DS_Senha = '" + DS_Senha + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "OK";

                    while (sqlDataReader.Read())
                    {
                        ID_Login = Convert.ToInt32(sqlDataReader["ID_Login"].ToString());
                        ID_NivelAcesso = Convert.ToInt32(sqlDataReader["ID_NivelAcesso"].ToString());
                        NM_FuncionarioLogin = sqlDataReader["NM_Funcionario"].ToString();
                    }
                }
                else
                {
                    DS_Mensagem = "Login inválido";
                }
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlDataReader.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return DS_Mensagem;
        }
    }
}