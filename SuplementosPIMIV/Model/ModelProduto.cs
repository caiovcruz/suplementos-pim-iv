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
        public double PR_Custo { get; set; }
        public double PR_Venda { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelProduto() { }

        public ModelProduto(int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nr_ean, string nm_produto, string ds_produto,
            double pr_custo, double pr_venda, string connectionString)
        {
            ID_Marca = id_marca;
            ID_Categoria = id_categoria;
            ID_Subcategoria = id_subcategoria;
            ID_Sabor = id_sabor;
            NR_EAN = nr_ean;
            NM_Produto = nm_produto;
            DS_Produto = ds_produto;
            PR_Venda = pr_venda;
            PR_Custo = pr_custo;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelProduto(int id_produto, int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nr_ean, string nm_produto,
            string ds_produto, double pr_custo, double pr_venda, string connectionString)
        {
            ID_Produto = id_produto;
            ID_Marca = id_marca;
            ID_Categoria = id_categoria;
            ID_Subcategoria = id_subcategoria;
            ID_Sabor = id_sabor;
            NR_EAN = nr_ean;
            NM_Produto = nm_produto;
            DS_Produto = ds_produto;
            PR_Venda = pr_venda;
            PR_Custo = pr_custo;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelProduto(int id_produto, char acao, string connectionString)
        {
            ID_Produto = id_produto;
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
                stringSQL.Append("INSERT INTO TB_Produto (");
                stringSQL.Append("ID_Marca, ");
                stringSQL.Append("ID_Categoria, ");
                stringSQL.Append("ID_Subcategoria, ");
                stringSQL.Append("ID_Sabor, ");
                stringSQL.Append("NR_EAN, ");
                stringSQL.Append("NM_Produto, ");
                stringSQL.Append("DS_Produto, ");
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
                stringSQL.Append("REPLACE( REPLACE('" + PR_Custo + "', '.' ,'' ), ',', '.' ), ");
                stringSQL.Append("REPLACE( REPLACE('" + PR_Venda + "', '.' ,'' ), ',', '.' ), ");
                stringSQL.Append("1);");
                stringSQL.Append("SELECT SCOPE_IDENTITY();");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                ID_Produto = Convert.ToInt32(sqlCommand.ExecuteScalar());

                DS_Mensagem = ID_Produto > 0 ? "OK" : "Erro ao cadastrar";
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
                stringSQL.Append("UPDATE TB_Produto SET ");
                stringSQL.Append("ID_Marca = '" + ID_Marca + "', ");
                stringSQL.Append("ID_Categoria = '" + ID_Categoria + "', ");
                stringSQL.Append("ID_Subcategoria = '" + ID_Subcategoria + "', ");
                stringSQL.Append("ID_Sabor = '" + ID_Sabor + "', ");
                stringSQL.Append("NR_EAN = '" + NR_EAN + "', ");
                stringSQL.Append("NM_Produto = '" + NM_Produto + "', ");
                stringSQL.Append("DS_Produto = '" + DS_Produto + "', ");
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

        private void Excluir()
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

        private void Ativar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Produto SET ");
                stringSQL.Append("Ativo = 1 ");
                stringSQL.Append("WHERE ID_Produto = '" + ID_Produto + "'");

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

        public DataTable ListarProdutos(int status, string connectionString)
        {
            DataTable dataTable = new DataTable();
            ConnectionString = connectionString;

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("PROD.ID_Produto, ");
                stringSQL.Append("CONCAT(PROD.NM_Produto, ' – ', MAR.NM_Marca, ' – ', SAB.NM_Sabor) AS NM_Produto ");
                stringSQL.Append("FROM TB_Produto AS PROD ");
                stringSQL.Append("INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca ");
                stringSQL.Append("INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor ");
                stringSQL.Append("WHERE PROD.Ativo = " + status + " ");
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
                stringSQL.Append("EST.QTD_Estoque, ");
                stringSQL.Append("FORMAT(PROD.PR_Custo, 'N2') AS PR_Custo, ");
                stringSQL.Append("FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda, ");
                stringSQL.Append("PROD.Ativo ");
                stringSQL.Append("FROM TB_Produto AS PROD ");
                stringSQL.Append("INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca ");
                stringSQL.Append("INNER JOIN TB_Categoria AS CAT ON PROD.ID_Categoria = CAT.ID_Categoria ");
                stringSQL.Append("INNER JOIN TB_Subcategoria AS SUB ON PROD.ID_Subcategoria = SUB.ID_Subcategoria ");
                stringSQL.Append("INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor ");
                stringSQL.Append("INNER JOIN TB_Estoque AS EST ON PROD.ID_Produto = EST.ID_Produto ");
                stringSQL.Append("WHERE PROD.Ativo = " + status + " ");
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

        public DataTable Consultar(int status, string filtro, string connectionString)
        {
            DataTable dataTable = new DataTable();
            ConnectionString = connectionString;

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
                stringSQL.Append("EST.QTD_Estoque, ");
                stringSQL.Append("FORMAT(PROD.PR_Custo, 'N2') AS PR_Custo, ");
                stringSQL.Append("FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda, ");
                stringSQL.Append("PROD.Ativo ");
                stringSQL.Append("FROM TB_Produto AS PROD ");
                stringSQL.Append("INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca ");
                stringSQL.Append("INNER JOIN TB_Categoria AS CAT ON PROD.ID_Categoria = CAT.ID_Categoria ");
                stringSQL.Append("INNER JOIN TB_Subcategoria AS SUB ON PROD.ID_Subcategoria = SUB.ID_Subcategoria ");
                stringSQL.Append("INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor ");
                stringSQL.Append("INNER JOIN TB_Estoque AS EST ON PROD.ID_Produto = EST.ID_Produto ");
                stringSQL.Append("WHERE PROD.Ativo = " + status + " ");
                stringSQL.Append("AND " + filtro + " ");
                stringSQL.Append("ORDER BY PROD.ID_Produto DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);

                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        ID_Produto = Convert.ToInt32(sqlDataReader["ID_Produto"].ToString());
                        NM_Produto = sqlDataReader["NM_Produto"].ToString() + " ➯ " +
                            sqlDataReader["NM_Marca"].ToString() + " ➯ " +
                            sqlDataReader["NM_Sabor"].ToString();
                        PR_Custo = Convert.ToDouble(sqlDataReader["PR_Custo"].ToString());
                        PR_Venda = Convert.ToDouble(sqlDataReader["PR_Venda"].ToString());
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

            return dataTable;
        }

        public string VerificarProdutoCadastrado(string id_produto, string nr_ean, string nm_produto, string id_marca, string connectionString)
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
                stringSQL.Append("FROM TB_Produto ");
                stringSQL.Append("WHERE ID_Produto != '" + id_produto + "' ");
                stringSQL.Append("AND NR_EAN = '" + nr_ean + "' ");
                stringSQL.Append("ORDER BY ID_Produto DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "EAN já cadastrado.";
                }

                sqlCommand.Dispose();
                sqlDataReader.Close();
                stringSQL.Clear();

                stringSQL.Append("SELECT ");
                stringSQL.Append("1 ");
                stringSQL.Append("FROM TB_Produto ");
                stringSQL.Append("WHERE ID_Produto != '" + id_produto + "' ");
                stringSQL.Append("AND NM_Produto = '" + nm_produto + "' ");
                stringSQL.Append("AND ID_Marca = '" + id_marca + "' ");
                stringSQL.Append("ORDER BY ID_Produto DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem += " Produto já cadastrado para a marca selecionada.";
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