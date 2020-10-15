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
            dataAcessObject.Setup(DataBase.DatabaseTypes.MySql,
                "Persist Security Info=False; " +
                "Server=localhost; " +
                "Database=pdv_suplementos; " +
                "Uid=root; " +
                "Pwd=011118");
        }

        public void Incluir()
        {
            DS_Mensagem = "";

            SetConnection();
            if (dataAcessObject.Connector.Open())
            {
                string SQLInsert = "INSERT INTO tb_categoria (NM_Categoria, DS_Categoria, Ativo)" +
                    "VALUES ('" + NM_Categoria + "','" + DS_Categoria + "', 1)";
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
                string SQLUpdate = "UPDATE tb_categoria SET NM_Categoria = '" + NM_Categoria + "', " +
                    "DS_Categoria = '" + DS_Categoria + "' WHERE ID_Categoria = '" + ID_Categoria + "'";
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
                string SQLSelect = "SELECT ID_Categoria, NM_Categoria, DS_Categoria " +
                    "FROM tb_categoria WHERE Ativo = 1 AND NM_Categoria LIKE CONCAT('" + NM_Categoria + "', '%') ORDER BY ID_Categoria DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "tb_categoria";
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
                string SQLDelete = "UPDATE tb_categoria SET Ativo = 0 WHERE ID_Categoria = '" + ID_Categoria + "'";
                var result = dataAcessObject.Connector.Execute(SQLDelete);

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
                string SQLSelect = "SELECT ID_Categoria, NM_Categoria, DS_Categoria " +
                    "FROM tb_categoria WHERE Ativo = 1 ORDER BY ID_Categoria DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "tb_categoria";
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