using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelSabor
    {
        public int ID_Sabor { get; set; }
        public string NM_Sabor { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelSabor(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelSabor(string nm_sabor, bool incluir, string connectionString)
        {
            NM_Sabor = nm_sabor;
            ConnectionString = connectionString;

            if (incluir)
            {
                Incluir();
            }
        }

        public ModelSabor(int id_sabor, string nm_sabor, string connectionString)
        {
            ID_Sabor = id_sabor;
            NM_Sabor = nm_sabor;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelSabor(int id_sabor, string connectionString)
        {
            ID_Sabor = id_sabor;
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
                stringSQL.Append("INSERT INTO TB_Sabor (");
                stringSQL.Append("NM_Sabor, ");
                stringSQL.Append("Ativo)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + NM_Sabor + "', ");
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
                stringSQL.Append("UPDATE TB_Sabor SET ");
                stringSQL.Append("NM_Sabor = '" + NM_Sabor + "' ");
                stringSQL.Append("WHERE ID_Sabor = '" + ID_Sabor + "'");

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
                stringSQL.Append("ID_Sabor, ");
                stringSQL.Append("NM_Sabor ");
                stringSQL.Append("FROM TB_Sabor ");
                stringSQL.Append("WHERE Ativo = 1 ");
                stringSQL.Append("AND NM_Sabor LIKE '" + NM_Sabor + "' + '%' ");
                stringSQL.Append("ORDER BY ID_Sabor DESC");

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
                stringSQL.Append("UPDATE TB_Sabor SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Sabor = '" + ID_Sabor + "'");

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
                stringSQL.Append("ID_Sabor, ");
                stringSQL.Append("NM_Sabor ");
                stringSQL.Append("FROM TB_Sabor ");
                stringSQL.Append("WHERE Ativo = 1 ");
                stringSQL.Append("ORDER BY ID_Sabor DESC");

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