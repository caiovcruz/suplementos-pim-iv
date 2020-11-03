using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelProduto
    {
        public int ID_Produto { get; set; }
        public int ID_Marca { get; set; }
        public int ID_Categoria { get; set; }
        public int ID_Subcategoria { get; set; }
        public int ID_Sabor { get; set; }
        public string NR_EAN { get; set; }
        public string NM_Produto { get; set; }
        public string DS_Produto { get; set; }
        public int QTD_Estoque { get; set; }
        public double PR_Custo { get; set; }
        public double PR_Venda { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelProduto(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelProduto(int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nr_ean, string nm_produto, string ds_produto,
            int qtd_estoque, double pr_custo, double pr_venda, string connectionString)
        {
            ID_Marca = id_marca;
            ID_Categoria = id_categoria;
            ID_Subcategoria = id_subcategoria;
            ID_Sabor = id_sabor;
            NR_EAN = nr_ean;
            NM_Produto = nm_produto;
            DS_Produto = ds_produto;
            QTD_Estoque = qtd_estoque;
            PR_Venda = pr_venda;
            PR_Custo = pr_custo;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelProduto(int id_produto, int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nr_ean, string nm_produto,
            string ds_produto, int qtd_estoque, double pr_custo, double pr_venda, string connectionString)
        {
            ID_Produto = id_produto;
            ID_Marca = id_marca;
            ID_Categoria = id_categoria;
            ID_Subcategoria = id_subcategoria;
            ID_Sabor = id_sabor;
            NR_EAN = nr_ean;
            NM_Produto = nm_produto;
            DS_Produto = ds_produto;
            QTD_Estoque = qtd_estoque;
            PR_Venda = pr_venda;
            PR_Custo = pr_custo;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelProduto(int id_produto, string connectionString)
        {
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
                stringSQL.Append("INSERT INTO TB_Produto (");
                stringSQL.Append("ID_Marca, ");
                stringSQL.Append("ID_Categoria, ");
                stringSQL.Append("ID_Subcategoria, ");
                stringSQL.Append("ID_Sabor, ");
                stringSQL.Append("NR_EAN, ");
                stringSQL.Append("NM_Produto, ");
                stringSQL.Append("DS_Produto, ");
                stringSQL.Append("QTD_Estoque, ");
                stringSQL.Append("PR_Custo, ");
                stringSQL.Append("PR_Venda, ");
                stringSQL.Append("Ativo)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_Marca + "', ");
                stringSQL.Append("'" + ID_Categoria + "', ");
                stringSQL.Append("'" + ID_Subcategoria + "', ");
                stringSQL.Append("'" + ID_Sabor + "', ");
                stringSQL.Append("'" + NR_EAN + "', ");
                stringSQL.Append("'" + NM_Produto + "', ");
                stringSQL.Append("'" + DS_Produto + "', ");
                stringSQL.Append("'" + QTD_Estoque + "', ");
                stringSQL.Append("REPLACE( REPLACE('" + PR_Custo + "', '.' ,'' ), ',', '.' ), ");
                stringSQL.Append("REPLACE( REPLACE('" + PR_Venda + "', '.' ,'' ), ',', '.' ), ");
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
                stringSQL.Append("UPDATE TB_Produto SET ");
                stringSQL.Append("ID_Marca = '" + ID_Marca + "', ");
                stringSQL.Append("ID_Categoria = '" + ID_Categoria + "', ");
                stringSQL.Append("ID_Subcategoria = '" + ID_Subcategoria + "', ");
                stringSQL.Append("ID_Sabor = '" + ID_Sabor + "', ");
                stringSQL.Append("NR_EAN = '" + NR_EAN + "', ");
                stringSQL.Append("NM_Produto = '" + NM_Produto + "', ");
                stringSQL.Append("DS_Produto = '" + DS_Produto + "', ");
                stringSQL.Append("QTD_Estoque = '" + QTD_Estoque + "', ");
                stringSQL.Append("PR_Venda = REPLACE( REPLACE('" + PR_Venda + "', '.' ,'' ), ',', '.' ), ");
                stringSQL.Append("PR_Custo = REPLACE( REPLACE('" + PR_Custo + "', '.' ,'' ), ',', '.' )");
                stringSQL.Append("WHERE ID_Produto = '" + ID_Produto + "'");

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

        public DataTable Consultar(string filtro, string texto)
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("PROD.ID_Produto, ");
                stringSQL.Append("PROD.NR_EAN, ");
                stringSQL.Append("PROD.NM_Produto, ");
                stringSQL.Append("PROD.ID_Marca, ");
                stringSQL.Append("MAR.NM_Marca, ");
                stringSQL.Append("PROD.ID_Categoria, ");
                stringSQL.Append("CAT.NM_Categoria, ");
                stringSQL.Append("PROD.ID_Subcategoria, ");
                stringSQL.Append("SUB.NM_Subcategoria, ");
                stringSQL.Append("PROD.ID_Sabor, ");
                stringSQL.Append("SAB.NM_Sabor, ");
                stringSQL.Append("PROD.DS_Produto, ");
                stringSQL.Append("PROD.QTD_Estoque, ");
                stringSQL.Append("FORMAT(PROD.PR_Custo, 'N2') AS PR_Custo, ");
                stringSQL.Append("FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda ");
                stringSQL.Append("FROM TB_Produto AS PROD ");
                stringSQL.Append("INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca ");
                stringSQL.Append("INNER JOIN TB_Categoria AS CAT ON PROD.ID_Categoria = CAT.ID_Categoria ");
                stringSQL.Append("INNER JOIN TB_Subcategoria AS SUB ON PROD.ID_Subcategoria = SUB.ID_Subcategoria ");
                stringSQL.Append("INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor ");
                stringSQL.Append("WHERE PROD.Ativo = 1 ");
                stringSQL.Append("AND " + filtro + " LIKE '" + texto + "' + '%' ");
                stringSQL.Append("ORDER BY PROD.ID_Produto DESC");

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
                stringSQL.Append("UPDATE TB_Produto SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Produto = '" + ID_Produto + "'");

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
                stringSQL.Append("PROD.ID_Produto, ");
                stringSQL.Append("PROD.NR_EAN, ");
                stringSQL.Append("PROD.NM_Produto, ");
                stringSQL.Append("PROD.ID_Marca, ");
                stringSQL.Append("MAR.NM_Marca, ");
                stringSQL.Append("PROD.ID_Categoria, ");
                stringSQL.Append("CAT.NM_Categoria, ");
                stringSQL.Append("PROD.ID_Subcategoria, ");
                stringSQL.Append("SUB.NM_Subcategoria, ");
                stringSQL.Append("PROD.ID_Sabor, ");
                stringSQL.Append("SAB.NM_Sabor, ");
                stringSQL.Append("PROD.DS_Produto, ");
                stringSQL.Append("PROD.QTD_Estoque, ");
                stringSQL.Append("FORMAT(PROD.PR_Custo, 'N2') AS PR_Custo, ");
                stringSQL.Append("FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda ");
                stringSQL.Append("FROM TB_Produto AS PROD ");
                stringSQL.Append("INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca ");
                stringSQL.Append("INNER JOIN TB_Categoria AS CAT ON PROD.ID_Categoria = CAT.ID_Categoria ");
                stringSQL.Append("INNER JOIN TB_Subcategoria AS SUB ON PROD.ID_Subcategoria = SUB.ID_Subcategoria ");
                stringSQL.Append("INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor ");
                stringSQL.Append("WHERE PROD.Ativo = 1 ");
                stringSQL.Append("ORDER BY PROD.ID_Produto DESC");

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