using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelMarca
    {
        public int ID_Marca { get; set; }
        public string NM_Marca { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelMarca(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelMarca(string nm_marca, string connectionString)
        {
            NM_Marca = nm_marca;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelMarca(int id_marca, string nm_marca, string connectionString)
        {
            ID_Marca = id_marca;
            NM_Marca = nm_marca;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelMarca(int id_marca, char acao, string connectionString)
        {
            ID_Marca = id_marca;
            ConnectionString = connectionString;

            if (acao.Equals('E'))
            {
                Excluir();
            }
            else if (acao.Equals('A'))
            {
                Ativar();
            }
        }

        public void Incluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO TB_Marca (");
                stringSQL.Append("NM_Marca, ");
                stringSQL.Append("Ativo) ");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + NM_Marca + "', ");
                stringSQL.Append("1)");

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
                stringSQL.Append("UPDATE TB_Marca SET ");
                stringSQL.Append("NM_Marca = '" + NM_Marca + "' ");
                stringSQL.Append("WHERE ID_Marca = '" + ID_Marca + "'");

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

        public DataTable Consultar(int status, string texto)
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Marca, ");
                stringSQL.Append("NM_Marca, ");
                stringSQL.Append("Ativo ");
                stringSQL.Append("FROM TB_Marca ");
                stringSQL.Append("WHERE Ativo = " + status + " ");
                stringSQL.Append("AND NM_Marca LIKE '" + texto + "' + '%' ");
                stringSQL.Append("ORDER BY ID_Marca DESC");

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
                stringSQL.Append("UPDATE TB_Marca SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Marca = '" + ID_Marca + "'");

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

        public void Ativar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Marca SET ");
                stringSQL.Append("Ativo = 1 ");
                stringSQL.Append("WHERE ID_Marca = '" + ID_Marca + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao ativar";
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

        public DataTable Exibir(int status)
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Marca, ");
                stringSQL.Append("NM_Marca, ");
                stringSQL.Append("Ativo ");
                stringSQL.Append("FROM TB_Marca ");
                stringSQL.Append("WHERE Ativo = " + status + " ");
                stringSQL.Append("ORDER BY ID_Marca DESC");

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

        public string VerificarMarcaCadastrada(string id_marca, string nm_marca)
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("1 ");
                stringSQL.Append("FROM TB_Marca ");
                stringSQL.Append("WHERE ID_Marca != '" + id_marca + "' ");
                stringSQL.Append("AND NM_Marca = '" + nm_marca + "' ");
                stringSQL.Append("ORDER BY ID_Marca DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "Marca já cadastrada.";
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
    }
}