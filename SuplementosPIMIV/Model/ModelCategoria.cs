using DataBase;
using System.Data;

namespace SuplementosPIMIV.Model
{
    public class ModelCategoria
    {
        private DAO dataAcessObject;

        public int ID_Categoria { get; set; }
        public string NM_Categoria { get; set; }
        public string DS_Categoria { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelCategoria() { }

        public ModelCategoria(string nm_categoria, string ds_categoria)
        {
            NM_Categoria = nm_categoria;
            DS_Categoria = ds_categoria;

            Incluir();
        }

        public ModelCategoria(int id_categoria, string nm_categoria, string ds_categoria)
        {
            ID_Categoria = id_categoria;
            NM_Categoria = nm_categoria;
            DS_Categoria = ds_categoria;

            Alterar();
        }

        public ModelCategoria(string nm_categoria)
        {
            NM_Categoria = nm_categoria;
        }

        public ModelCategoria(int id_categoria)
        {
            ID_Categoria = id_categoria;

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
                    "INSERT INTO TB_Categoria (" +
                        "NM_Categoria, " +
                        "DS_Categoria, " +
                        "Ativo)" +
                    "VALUES (" +
                        "'" + NM_Categoria + "', " +
                        "'" + DS_Categoria + "', " +
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
                    "UPDATE TB_Categoria SET " +
                    "NM_Categoria = '" + NM_Categoria + "', " +
                    "DS_Categoria = '" + DS_Categoria + "' " +
                    "WHERE ID_Categoria = '" + ID_Categoria + "'";
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
                    "ID_Categoria, " +
                    "NM_Categoria, " +
                    "DS_Categoria " +
                    "FROM TB_Categoria " +
                    "WHERE Ativo = 1 " +
                    "AND NM_Categoria LIKE '" + NM_Categoria + "' + '%' " +
                    "ORDER BY ID_Categoria DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Categoria";
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
                    "UPDATE TB_Categoria SET " +
                    "Ativo = 0 " +
                    "WHERE ID_Categoria = '" + ID_Categoria + "'";
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
                    "ID_Categoria, " +
                    "NM_Categoria, " +
                    "DS_Categoria " +
                    "FROM TB_Categoria " +
                    "WHERE Ativo = 1 " +
                    "ORDER BY ID_Categoria DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Categoria";
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