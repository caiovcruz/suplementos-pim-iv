using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SuplementosPIMIV.Model
{
    public class ModelVenda
    {
        public int ID_Venda { get; set; }
        public int ID_Funcionario { get; set; }
        public DateTime DT_Venda { get; set; }
        public string DS_TipoPagamento { get; set; }
        public int NR_Parcelas { get; set; }
        public double VL_Total { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelVenda(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelVenda(int id_funcionario, DateTime dt_venda, string connectionString)
        {
            ID_Funcionario = id_funcionario;
            DT_Venda = dt_venda;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelVenda(int id_venda, int id_funcionario, DateTime dt_venda, string ds_tipoPagamento, int nr_parcelas, double vl_total, string connectionString)
        {
            ID_Venda = id_venda;
            ID_Funcionario = id_funcionario;
            DT_Venda = dt_venda;
            DS_TipoPagamento = ds_tipoPagamento;
            NR_Parcelas = nr_parcelas;
            VL_Total = vl_total;
            ConnectionString = connectionString;

            Finalizar();
        }

        public ModelVenda(int id_venda, string connectionString)
        {
            ID_Venda = id_venda;
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
                stringSQL.Append("INSERT INTO TB_Venda (");
                stringSQL.Append("ID_Funcionario, ");
                stringSQL.Append("DT_Venda)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_Funcionario + "', ");
                stringSQL.Append("'" + DT_Venda + "');");
                stringSQL.Append("SELECT SCOPE_IDENTITY();");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                ID_Venda = Convert.ToInt32(sqlCommand.ExecuteScalar());

                DS_Mensagem = ID_Venda > 0 ? "OK" : "Erro ao iniciar venda";
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

        public void Finalizar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Venda SET ");
                stringSQL.Append("ID_Funcionario = '" + ID_Funcionario + "', ");
                stringSQL.Append("DT_Venda = '" + DT_Venda + "', ");
                stringSQL.Append("DS_TipoPagamento = '" + DS_TipoPagamento + "', ");
                stringSQL.Append("NR_Parcelas = '" + NR_Parcelas + "', ");
                stringSQL.Append("VL_Total = REPLACE( REPLACE('" + VL_Total + "', '.' ,'' ), ',', '.' ) ");
                stringSQL.Append("WHERE ID_Venda = '" + ID_Venda + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao finalizar venda";
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
                stringSQL.Append("DELETE FROM TB_Venda ");
                stringSQL.Append("WHERE ID_Venda = '" + ID_Venda + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao excluir venda";
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
                stringSQL.Append("VEN.ID_Venda, ");
                stringSQL.Append("FUN.NM_Funcionario, ");
                stringSQL.Append("VEN.DT_Venda, ");
                stringSQL.Append("VEN.DS_TipoPagamento, ");
                stringSQL.Append("VEN.NR_Parcelas, ");
                stringSQL.Append("FORMAT(VEN.VL_Total, 'N2') AS VL_Total ");
                stringSQL.Append("FROM TB_Venda AS VEN ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN ON VEN.ID_Funcionario = FUN.ID_Funcionario ");
                stringSQL.Append("ORDER BY VEN.ID_Venda DESC");

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