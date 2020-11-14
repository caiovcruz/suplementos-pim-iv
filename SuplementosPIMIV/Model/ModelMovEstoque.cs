using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelMovEstoque
    {
        public int ID_MovimentacaoEstoque { get; set; }
        public int ID_Produto { get; set; }
        public int QTD_MovimentacaoEstoque { get; set; }
        public string DS_MovimentacaoEstoque { get; set; }
        public DateTime DT_MovimentacaoEstoque { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelMovEstoque(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelMovEstoque(int id_movimentacaoEstoque, string connectionString)
        {
            ID_MovimentacaoEstoque = id_movimentacaoEstoque;
            ConnectionString = connectionString;

            Excluir();
        }

        public ModelMovEstoque(int id_produto, int qtd_movimentacaoEstoque, string ds_movimentacaoEstoque, DateTime dt_movimentacaoEstoque, 
            string connectionString)
        {
            ID_Produto = id_produto;
            QTD_MovimentacaoEstoque = qtd_movimentacaoEstoque;
            DS_MovimentacaoEstoque = ds_movimentacaoEstoque;
            DT_MovimentacaoEstoque = dt_movimentacaoEstoque;
            ConnectionString = connectionString;

            Movimentacao();
        }

        public void Movimentacao()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO TB_MovimentacaoEstoque (");
                stringSQL.Append("ID_Produto, ");
                stringSQL.Append("QTD_MovimentacaoEstoque, ");
                stringSQL.Append("DS_MovimentacaoEstoque, ");
                stringSQL.Append("DT_MovimentacaoEstoque, ");
                stringSQL.Append("QTD_Estoque)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_Produto + "', ");
                stringSQL.Append("'" + QTD_MovimentacaoEstoque + "', ");
                stringSQL.Append("'" + DS_MovimentacaoEstoque + "', ");
                stringSQL.Append("'" + DT_MovimentacaoEstoque + "', ");
                stringSQL.Append("'" + CalcularNovoTotalEstoque(ID_Produto, QTD_MovimentacaoEstoque, DS_MovimentacaoEstoque) + "');");
                stringSQL.Append("SELECT SCOPE_IDENTITY();");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                ID_MovimentacaoEstoque = Convert.ToInt32(sqlCommand.ExecuteScalar());

                DS_Mensagem = ID_MovimentacaoEstoque > 0 ? "OK" : "Erro ao cadastrar movimentação no estoque";

                if (DS_Mensagem.Equals("OK"))
                {
                    DS_Mensagem = "";
                    stringSQL.Clear();
                    sqlCommand.Dispose();
                    string sinal = DS_MovimentacaoEstoque.Equals("Entrada") ? "+=" : "-=";

                    stringSQL.Append("UPDATE TB_Estoque SET ");
                    stringSQL.Append("QTD_Estoque " + sinal + " '" + QTD_MovimentacaoEstoque + "' ");
                    stringSQL.Append("WHERE ID_Produto = '" + ID_Produto + "'");

                    sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                    int result = sqlCommand.ExecuteNonQuery();

                    DS_Mensagem = result > 0 ? "OK" : "Erro ao atualizar quantidade do estoque.";

                    if (!DS_Mensagem.Equals("OK"))
                    {
                        stringSQL.Clear();
                        sqlCommand.Dispose();
                        result = 0;

                        stringSQL.Append("DELETE FROM TB_MovimentacaoEstoque WHERE ");
                        stringSQL.Append("ID_MovimentacaoEstoque = '" + ID_MovimentacaoEstoque + "'");

                        sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                        result = sqlCommand.ExecuteNonQuery();

                        DS_Mensagem += result > 0 ? " Movimentação excluída, por favor cadastre novamente." : 
                            " Erro ao excluir movimentação, por favor exclua manualmente e recadastre-a.";
                    }
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
        }

        public void Excluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("DELETE FROM TB_MovimentacaoEstoque WHERE ");
                stringSQL.Append("ID_MovimentacaoEstoque = '" + ID_MovimentacaoEstoque + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem += result > 0 ? "OK" : "Erro ao excluir movimentação";
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
                stringSQL.Append("MOV.ID_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.ID_Produto, ");
                stringSQL.Append("PROD.NM_Produto, ");
                stringSQL.Append("MOV.QTD_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.DS_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.DT_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.QTD_Estoque ");
                stringSQL.Append("FROM TB_MovimentacaoEstoque AS MOV ");
                stringSQL.Append("INNER JOIN TB_Produto AS PROD ON MOV.ID_Produto = PROD.ID_Produto ");
                stringSQL.Append("WHERE PROD.NM_Produto LIKE '" + texto + "' + '%' ");
                stringSQL.Append("ORDER BY MOV.ID_MovimentacaoEstoque DESC");

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

        public DataTable Exibir()
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("MOV.ID_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.ID_Produto, ");
                stringSQL.Append("PROD.NM_Produto, ");
                stringSQL.Append("MOV.QTD_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.DS_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.DT_MovimentacaoEstoque, ");
                stringSQL.Append("MOV.QTD_Estoque ");
                stringSQL.Append("FROM TB_MovimentacaoEstoque AS MOV ");
                stringSQL.Append("INNER JOIN TB_Produto AS PROD ON MOV.ID_Produto = PROD.ID_Produto ");
                stringSQL.Append("ORDER BY MOV.ID_MovimentacaoEstoque DESC");

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

        public int CalcularNovoTotalEstoque(int id_produto, int qtd_movimentacaoEstoque, string ds_movimentacaoEstoque)
        {
            int qtd_total = 0;

            try
            {
                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("QTD_Estoque ");
                stringSQL.Append("FROM TB_Estoque ");
                stringSQL.Append("WHERE ID_Produto = '" + id_produto + "' ");
                stringSQL.Append("ORDER BY ID_Produto DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                qtd_total = Convert.ToInt32(sqlCommand.ExecuteScalar());
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
            }

            if (ds_movimentacaoEstoque.Equals("Entrada")) qtd_total += qtd_movimentacaoEstoque;
            else qtd_total -= qtd_movimentacaoEstoque;

            return qtd_total;
        }
    }
}