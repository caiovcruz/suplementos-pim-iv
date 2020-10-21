using DataBase;
using System.Data;

namespace SuplementosPIMIV.Model
{
    public class ModelProduto
    {
        private DAO dataAcessObject;

        public int ID_Produto { get; set; }
        public int ID_Marca { get; set; }
        public int ID_Categoria { get; set; }
        public int ID_Subcategoria { get; set; }
        public int ID_Sabor { get; set; }
        public string NM_Produto { get; set; }
        public string DS_Produto { get; set; }
        public int QTD_Estoque { get; set; }
        public double PR_Custo { get; set; }
        public double PR_Venda { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelProduto() { }

        public ModelProduto(int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nm_produto, string ds_produto, int qtd_estoque, double pr_custo, double pr_venda)
        {
            ID_Marca = id_marca;
            ID_Categoria = id_categoria;
            ID_Subcategoria = id_subcategoria;
            ID_Sabor = id_sabor;
            NM_Produto = nm_produto;
            DS_Produto = ds_produto;
            QTD_Estoque = qtd_estoque;
            PR_Venda = pr_venda;
            PR_Custo = pr_custo;

            Incluir();
        }

        public ModelProduto(int id_produto, int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nm_produto, string ds_produto, int qtd_estoque, double pr_custo, double pr_venda)
        {
            ID_Produto = id_produto;
            ID_Marca = id_marca;
            ID_Categoria = id_categoria;
            ID_Subcategoria = id_subcategoria;
            ID_Sabor = id_sabor;
            NM_Produto = nm_produto;
            DS_Produto = ds_produto;
            QTD_Estoque = qtd_estoque;
            PR_Venda = pr_venda;
            PR_Custo = pr_custo;

            Alterar();
        }

        public ModelProduto(string nm_produto)
        {
            NM_Produto = nm_produto;
        }

        public ModelProduto(int id_produto)
        {
            ID_Produto = id_produto;

            Excluir();
        }

        private void SetConnection()
        {
            dataAcessObject = new DAO();
            dataAcessObject.Setup(DataBase.DatabaseTypes.SqlServer,
                "Data Source=DRACCON/SQLEXPRESS; " +
                "Initial Catalog=DB_Suplementos_PDV; " +
                "Integrated Security=SSPI");
        }

        public void Incluir()
        {
            DS_Mensagem = "";

            SetConnection();
            if (dataAcessObject.Connector.Open())
            {
                string SQLInsert = 
                    "INSERT INTO TB_Produto (" +
                        "ID_Marca, " +
                        "ID_Categoria, " +
                        "ID_Subcategoria, " +
                        "ID_Sabor, " +
                        "NM_Produto, " +
                        "DS_Produto, " +
                        "QTD_Estoque, " +
                        "PR_Custo, " +
                        "PR_Venda, " +
                        "Ativo)" +
                    "VALUES (" +
                        "'" + ID_Marca + "', " +
                        "'" + ID_Categoria + "', " +
                        "'" + ID_Subcategoria + "', " +
                        "'" + ID_Sabor + "', " +
                        "'" + NM_Produto + "', " +
                        "'" + DS_Produto + "', " +
                        "'" + QTD_Estoque + "', " +
                        "REPLACE( REPLACE('" + PR_Custo + "', '.' ,'' ), ',', '.' ), " +
                        "REPLACE( REPLACE('" + PR_Venda + "', '.' ,'' ), ',', '.' ), " +
                        "1)";
                var result = dataAcessObject.Connector.Execute(SQLInsert);

                DS_Mensagem = result > 0 ? "OK" : "Erro ao cadastrar";
            }
            else
            {
                DS_Mensagem = "Erro de conexão";
            }
        }

        public void Alterar()
        {
            DS_Mensagem = "";

            SetConnection();
            if (dataAcessObject.Connector.Open())
            {
                string SQLUpdate = 
                    "UPDATE TB_Produto SET " +
                    "ID_Marca = '" + ID_Marca + "', " +
                    "ID_Categoria = '" + ID_Categoria + "', " +
                    "ID_Subcategoria = '" + ID_Subcategoria + "', " +
                    "ID_Sabor = '" + ID_Sabor + "', " +
                    "NM_Produto = '" + NM_Produto + "', " +
                    "DS_Produto = '" + DS_Produto + "', " +
                    "QTD_Estoque = '" + QTD_Estoque + "', " +
                    "PR_Venda = REPLACE( REPLACE('" + PR_Venda + "', '.' ,'' ), ',', '.' ), " +
                    "PR_Custo = REPLACE( REPLACE('" + PR_Custo + "', '.' ,'' ), ',', '.' )" +
                    "WHERE ID_Produto = '" + ID_Produto + "'";
                var result = dataAcessObject.Connector.Execute(SQLUpdate);

                DS_Mensagem = result > 0 ? "OK" : "Erro ao alterar";
            }
            else
            {
                DS_Mensagem = "Erro de conexão";
            }
        }

        public DataTable Consultar()
        {
            DataTable dataTable = new DataTable();

            SetConnection();
            if (dataAcessObject.Connector.Open())
            {
                string SQLSelect = 
                    "SELECT " +
                    "PROD.ID_Produto, " +
                    "PROD.NM_Produto, " +
                    "PROD.ID_Marca, " +
                    "MAR.NM_Marca, " +
                    "PROD.ID_Categoria, " +
                    "CAT.NM_Categoria, " +
                    "PROD.ID_Subcategoria, " +
                    "SUB.NM_Subcategoria, " +
                    "PROD.ID_Sabor, " +
                    "SAB.NM_Sabor, " +
                    "PROD.DS_Produto, " +
                    "PROD.QTD_Estoque, " +
                    "FORMAT(PROD.PR_Custo, 'N2') AS PR_Custo, " +
                    "FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda " +
                    "FROM TB_Produto AS PROD " +
                    "INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca " +
                    "INNER JOIN TB_Categoria AS CAT ON PROD.ID_Categoria = CAT.ID_Categoria " +
                    "INNER JOIN TB_Subcategoria AS SUB ON PROD.ID_Subcategoria = SUB.ID_Subcategoria " +
                    "INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor " +
                    "WHERE PROD.Ativo = 1 " +
                    "AND PROD.NM_Produto LIKE '" + NM_Produto + "' + '%' " +
                    "ORDER BY PROD.ID_Produto DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Produto";
                dataTable.Load(dataReader);
            }
            else
            {
                dataTable = null;
            }

            return dataTable;
        }

        public void Excluir()
        {
            DS_Mensagem = "";

            SetConnection();
            if (dataAcessObject.Connector.Open())
            {
                string SQLUpdate = 
                    "UPDATE TB_Produto SET " +
                    "Ativo = 0 " +
                    "WHERE ID_Produto = '" + ID_Produto + "'";
                var result = dataAcessObject.Connector.Execute(SQLUpdate);

                DS_Mensagem = result > 0 ? "OK" : "Erro ao excluir";
            }
            else
            {
                DS_Mensagem = "Erro de conexão";
            }
        }

        public DataTable Exibir()
        {
            DataTable dataTable = new DataTable();

            SetConnection();
            if (dataAcessObject.Connector.Open())
            {
                string SQLSelect = 
                    "SELECT " +
                    "PROD.ID_Produto, " +
                    "PROD.NM_Produto, " +
                    "PROD.ID_Marca, " +
                    "MAR.NM_Marca, " +
                    "PROD.ID_Categoria, " +
                    "CAT.NM_Categoria, " +
                    "PROD.ID_Subcategoria, " +
                    "SUB.NM_Subcategoria, " +
                    "PROD.ID_Sabor, " +
                    "SAB.NM_Sabor, " +
                    "PROD.DS_Produto, " +
                    "PROD.QTD_Estoque, " +
                    "FORMAT(PROD.PR_Custo, 'N2') AS PR_Custo, " +
                    "FORMAT(PROD.PR_Venda, 'N2') AS PR_Venda " +
                    "FROM TB_Produto AS PROD " +
                    "INNER JOIN TB_Marca AS MAR ON PROD.ID_Marca = MAR.ID_Marca " +
                    "INNER JOIN TB_Categoria AS CAT ON PROD.ID_Categoria = CAT.ID_Categoria " +
                    "INNER JOIN TB_Subcategoria AS SUB ON PROD.ID_Subcategoria = SUB.ID_Subcategoria " +
                    "INNER JOIN TB_Sabor AS SAB ON PROD.ID_Sabor = SAB.ID_Sabor " +
                    "WHERE PROD.Ativo = 1 " +
                    "ORDER BY PROD.ID_Produto DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Produto";
                dataTable.Load(dataReader);
            }
            else
            {
                dataTable = null;
                DS_Mensagem = "Erro de conexão";
            }

            return dataTable;
        }
    }
}