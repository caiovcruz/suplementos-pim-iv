using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelSubcategoria
    {
        public int ID_Subcategoria { get; set; }
        public int ID_Categoria { get; set; }
        public string NM_Subcategoria { get; set; }
        public string DS_Subcategoria { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelSubcategoria() { }

        public ModelSubcategoria(int id_categoria, string nm_subcategoria, string ds_subcategoria, string connectionString)
        {
            ID_Categoria = id_categoria;
            NM_Subcategoria = nm_subcategoria;
            DS_Subcategoria = ds_subcategoria;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelSubcategoria(int id_subcategoria, int id_categoria, string nm_subcategoria, string ds_subcategoria, string connectionString)
        {
            ID_Subcategoria = id_subcategoria;
            ID_Categoria = id_categoria;
            NM_Subcategoria = nm_subcategoria;
            DS_Subcategoria = ds_subcategoria;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelSubcategoria(int id_subcategoria, char acao, string connectionString)
        {
            ID_Subcategoria = id_subcategoria;
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
                stringSQL.Append("INSERT INTO TB_Subcategoria (");
                stringSQL.Append("ID_Categoria, ");
                stringSQL.Append("NM_Subcategoria, ");
                stringSQL.Append("DS_Subcategoria, ");
                stringSQL.Append("Ativo)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_Categoria + "', ");
                stringSQL.Append("'" + NM_Subcategoria + "', ");
                stringSQL.Append("'" + DS_Subcategoria + "', ");
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
                stringSQL.Append("UPDATE TB_Subcategoria SET ");
                stringSQL.Append("ID_Categoria = '" + ID_Categoria + "', ");
                stringSQL.Append("NM_Subcategoria = '" + NM_Subcategoria + "', ");
                stringSQL.Append("DS_Subcategoria = '" + DS_Subcategoria + "' ");
                stringSQL.Append("WHERE ID_Subcategoria = '" + ID_Subcategoria + "'");

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
                stringSQL.Append("UPDATE TB_Subcategoria SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Subcategoria = '" + ID_Subcategoria + "'");

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
                stringSQL.Append("UPDATE TB_Subcategoria SET ");
                stringSQL.Append("Ativo = 1 ");
                stringSQL.Append("WHERE ID_Subcategoria = '" + ID_Subcategoria + "'");

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
                stringSQL.Append("SUB.ID_Subcategoria, ");
                stringSQL.Append("SUB.NM_Subcategoria, ");
                stringSQL.Append("SUB.ID_Categoria, ");
                stringSQL.Append("CAT.NM_Categoria, ");
                stringSQL.Append("SUB.DS_Subcategoria, ");
                stringSQL.Append("SUB.Ativo ");
                stringSQL.Append("FROM TB_Subcategoria AS SUB ");
                stringSQL.Append("INNER JOIN TB_Categoria AS CAT ON SUB.ID_Categoria = CAT.ID_Categoria ");
                stringSQL.Append("WHERE SUB.Ativo = " + status + " ");
                stringSQL.Append("ORDER BY SUB.ID_Subcategoria DESC");

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
                stringSQL.Append("SUB.ID_Subcategoria, ");
                stringSQL.Append("SUB.NM_Subcategoria, ");
                stringSQL.Append("SUB.ID_Categoria, ");
                stringSQL.Append("CAT.NM_Categoria, ");
                stringSQL.Append("SUB.DS_Subcategoria, ");
                stringSQL.Append("SUB.Ativo ");
                stringSQL.Append("FROM TB_Subcategoria AS SUB ");
                stringSQL.Append("INNER JOIN TB_Categoria AS CAT ON SUB.ID_Categoria = CAT.ID_Categoria ");
                stringSQL.Append("WHERE SUB.Ativo = " + status + " ");
                stringSQL.Append("AND SUB.NM_Subcategoria LIKE '" + texto + "' + '%' ");
                stringSQL.Append("ORDER BY SUB.ID_Subcategoria DESC");

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

        public DataTable Filtrar(int status, int id_categoria, string connectionString)
        {
            DataTable dataTable = new DataTable();
            ConnectionString = connectionString;

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("SUB.ID_Subcategoria, ");
                stringSQL.Append("SUB.NM_Subcategoria, ");
                stringSQL.Append("SUB.ID_Categoria, ");
                stringSQL.Append("CAT.NM_Categoria, ");
                stringSQL.Append("SUB.DS_Subcategoria, ");
                stringSQL.Append("SUB.Ativo ");
                stringSQL.Append("FROM TB_Subcategoria AS SUB ");
                stringSQL.Append("INNER JOIN TB_Categoria AS CAT ON SUB.ID_Categoria = CAT.ID_Categoria ");
                stringSQL.Append("WHERE SUB.Ativo = " + status + " ");
                stringSQL.Append("AND SUB.ID_Categoria = " + id_categoria + " ");
                stringSQL.Append("ORDER BY SUB.ID_Subcategoria DESC");

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

        public string VerificarSubcategoriaCadastrada(string id_subcategoria, string nm_subcategoria, string id_categoria, string connectionString)
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
                stringSQL.Append("FROM TB_Subcategoria ");
                stringSQL.Append("WHERE ID_Subcategoria != '" + id_subcategoria + "' ");
                stringSQL.Append("AND NM_Subcategoria = '" + nm_subcategoria + "' ");
                stringSQL.Append("AND ID_Categoria = '" + id_categoria + "' ");
                stringSQL.Append("ORDER BY ID_Subcategoria DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "Subcategoria já cadastrada para a categoria selecionada.";
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