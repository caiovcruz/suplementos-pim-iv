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

        public ModelMarca(string nm_marca, bool incluir, string connectionString)
        {
            NM_Marca = nm_marca;
            ConnectionString = connectionString;

            if (incluir)
            {
                Incluir();
            }
        }

        public ModelMarca(int id_marca, string nm_marca, string connectionString)
        {
            ID_Marca = id_marca;
            NM_Marca = NM_Marca;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelMarca(int id_marca, string connectionString)
        {
            ID_Marca = id_marca;
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

        public DataTable Consultar()
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Marca, ");
                stringSQL.Append("NM_Marca ");
                stringSQL.Append("FROM TB_Marca ");
                stringSQL.Append("WHERE Ativo = 1 ");
                stringSQL.Append("AND NM_Marca LIKE '" + NM_Marca + "' + '%' ");
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

        public DataTable Exibir()
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Marca, ");
                stringSQL.Append("NM_Marca ");
                stringSQL.Append("FROM TB_Marca ");
                stringSQL.Append("WHERE Ativo = 1 ");
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
    }
}