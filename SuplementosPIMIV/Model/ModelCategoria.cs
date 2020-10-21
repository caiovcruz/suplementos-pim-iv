using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelCategoria
    {
        public int ID_Categoria { get; set; }
        public string NM_Categoria { get; set; }
        public string DS_Categoria { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelCategoria(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelCategoria(string nm_categoria, string ds_categoria, string connectionString)
        {
            NM_Categoria = nm_categoria;
            DS_Categoria = ds_categoria;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelCategoria(int id_categoria, string nm_categoria, string ds_categoria, string connectionString)
        {
            ID_Categoria = id_categoria;
            NM_Categoria = nm_categoria;
            DS_Categoria = ds_categoria;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelCategoria(string nm_categoria, string connectionString)
        {
            NM_Categoria = nm_categoria;
            ConnectionString = connectionString;
        }

        public ModelCategoria(int id_categoria, string connectionString)
        {
            ID_Categoria = id_categoria;
            ConnectionString = connectionString;

            Excluir();
        }

        public void Incluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO TB_Categoria (");
                stringSQL.Append("NM_Categoria, ");
                stringSQL.Append("DS_Categoria, ");
                stringSQL.Append("Ativo)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + NM_Categoria + "', ");
                stringSQL.Append("'" + DS_Categoria + "', ");
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
                stringSQL.Append("UPDATE TB_Categoria SET ");
                stringSQL.Append("NM_Categoria = '" + NM_Categoria + "', ");
                stringSQL.Append("DS_Categoria = '" + DS_Categoria + "' ");
                stringSQL.Append("WHERE ID_Categoria = '" + ID_Categoria + "'");

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

        public DataTable Consultar()
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Categoria, ");
                stringSQL.Append("NM_Categoria, ");
                stringSQL.Append("DS_Categoria ");
                stringSQL.Append("FROM TB_Categoria ");
                stringSQL.Append("WHERE Ativo = 1 ");
                stringSQL.Append("AND NM_Categoria LIKE '" + NM_Categoria + "' + '%' ");
                stringSQL.Append("ORDER BY ID_Categoria DESC");

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
                stringSQL.Append("UPDATE TB_Categoria SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Categoria = '" + ID_Categoria + "'");

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
                stringSQL.Append("ID_Categoria, ");
                stringSQL.Append("NM_Categoria, ");
                stringSQL.Append("DS_Categoria ");
                stringSQL.Append("FROM TB_Categoria ");
                stringSQL.Append("WHERE Ativo = 1 ");
                stringSQL.Append("ORDER BY ID_Categoria DESC");

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
    }
}