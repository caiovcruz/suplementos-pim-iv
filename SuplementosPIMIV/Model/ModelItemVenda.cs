using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SuplementosPIMIV.Model
{
    public class ModelItemVenda
    {
        public int ID_Venda { get; set; }
        public int ID_Produto { get; set; }
        public int QTD_ItemVenda { get; set; }
        public double VL_Subtotal { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelItemVenda(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelItemVenda(int id_venda, int id_produto, int qtd_itemVenda, double vl_subtotal, char acao, string connectionString)
        {
            ID_Venda = id_venda;
            ID_Produto = id_produto;
            QTD_ItemVenda = qtd_itemVenda;
            VL_Subtotal = vl_subtotal;
            ConnectionString = connectionString;

            if (acao.Equals('I'))
            {
                Incluir();
            }
            else if (acao.Equals('A'))
            {
                Alterar();
            }
        }

        public ModelItemVenda(int id_venda, int id_produto, string connectionString)
        {
            ID_Venda = id_venda;
            ID_Produto = id_produto;
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
                stringSQL.Append("INSERT INTO TB_ItemVenda (");
                stringSQL.Append("ID_Venda, ");
                stringSQL.Append("ID_Produto, ");
                stringSQL.Append("QTD_ItemVenda, ");
                stringSQL.Append("VL_Subtotal)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_Venda + "', ");
                stringSQL.Append("'" + ID_Produto + "', ");
                stringSQL.Append("'" + QTD_ItemVenda + "', ");
                stringSQL.Append("'" + VL_Subtotal + "')");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao incluir produto na venda";
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
                stringSQL.Append("UPDATE TB_ItemVenda SET ");
                stringSQL.Append("QTD_ItemVenda = '" + QTD_ItemVenda + "', ");
                stringSQL.Append("VL_Subtotal = REPLACE( REPLACE('" + VL_Subtotal + "', '.' ,'' ), ',', '.' ) ");
                stringSQL.Append("WHERE ID_Venda = '" + ID_Venda + "' ");
                stringSQL.Append("AND ID_Produto = '" + ID_Produto + "' ");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao alterar produto da venda";
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
                stringSQL.Append("DELETE FROM TB_ItemVenda ");
                stringSQL.Append("WHERE ID_Venda = '" + ID_Venda + "'");
                stringSQL.Append("AND ID_Produto = '" + ID_Produto + "' ");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao excluir produto da venda";
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

        public DataTable Exibir(int id_venda)
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ITEM.ID_Venda, ");
                stringSQL.Append("ITEM.ID_Produto, ");
                stringSQL.Append("PROD.NR_EAN, ");
                stringSQL.Append("PROD.NM_Produto, ");
                stringSQL.Append("MAR.NM_Marca, ");
                stringSQL.Append("SAB.NM_Sabor, ");
                stringSQL.Append("FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda, ");
                stringSQL.Append("ITEM.QTD_ItemVenda, ");
                stringSQL.Append("FORMAT(ITEM.VL_Subtotal, 'N2') AS VL_Subtotal ");
                stringSQL.Append("FROM TB_ItemVenda AS ITEM ");
                stringSQL.Append("INNER JOIN TB_Produto AS PROD ON ITEM.ID_Produto = PROD.ID_Produto ");
                stringSQL.Append("INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca ");
                stringSQL.Append("INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor ");
                stringSQL.Append("WHERE ITEM.ID_Venda = " + id_venda + " ");
                stringSQL.Append("ORDER BY ITEM.ID_Venda DESC");

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