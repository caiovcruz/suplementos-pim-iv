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

        public ModelCategoria() { }

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

        public ModelCategoria(int id_categoria, char acao, string connectionString)
        {
            ID_Categoria = id_categoria;
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

        private void Incluir()
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

        private void Alterar()
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

        private void Excluir()
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

        private void Ativar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Categoria SET ");
                stringSQL.Append("Ativo = 1 ");
                stringSQL.Append("WHERE ID_Categoria = '" + ID_Categoria + "'");

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

        public DataTable Exibir(int status, string connectionString)
        {
            DataTable dataTable = new DataTable();
            ConnectionString = connectionString;

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Categoria, ");
                stringSQL.Append("NM_Categoria, ");
                stringSQL.Append("DS_Categoria, ");
                stringSQL.Append("Ativo ");
                stringSQL.Append("FROM TB_Categoria ");
                stringSQL.Append("WHERE Ativo = " + status + " ");
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

        public DataTable Consultar(int status, string texto, string connectionString)
        {
            DataTable dataTable = new DataTable();
            ConnectionString = connectionString;

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Categoria, ");
                stringSQL.Append("NM_Categoria, ");
                stringSQL.Append("DS_Categoria, ");
                stringSQL.Append("Ativo ");
                stringSQL.Append("FROM TB_Categoria ");
                stringSQL.Append("WHERE Ativo = " + status + " ");
                stringSQL.Append("AND NM_Categoria LIKE '" + texto + "' + '%' ");
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

        public string VerificarCategoriaCadastrada(string id_categoria, string nm_categoria, string connectionString)
        {
            DS_Mensagem = "";
            ConnectionString = connectionString;

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("1 ");
                stringSQL.Append("FROM TB_Categoria ");
                stringSQL.Append("WHERE ID_Categoria != '" + id_categoria + "' ");
                stringSQL.Append("AND NM_Categoria = '" + nm_categoria + "' ");
                stringSQL.Append("ORDER BY ID_Categoria DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "Categoria já cadastrada.";
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