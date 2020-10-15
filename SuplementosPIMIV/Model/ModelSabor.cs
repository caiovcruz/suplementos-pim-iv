using DataBase;
using System.Data;

namespace SuplementosPIMIV.Model
{
    public class ModelSabor
    {
        private DAO dataAcessObject;

        public int ID_Sabor { get; set; }
        public string DS_Sabor { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelSabor() { }

        public ModelSabor(string ds_sabor, bool incluir)
        {
            DS_Sabor = ds_sabor;

            if (incluir)
            {
                Incluir();
            }
        }

        public ModelSabor(int id_sabor, string ds_sabor)
        {
            ID_Sabor = id_sabor;
            DS_Sabor = ds_sabor;

            Alterar();
        }

        public ModelSabor(int id_sabor)
        {
            ID_Sabor = id_sabor;

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
                string SQLInsert = "INSERT INTO tb_sabor (DS_Sabor, Ativo)" +
                    "VALUES ('" + DS_Sabor + "', 1)";
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
                string SQLUpdate = "UPDATE tb_sabor SET DS_Sabor = '" + DS_Sabor + "', WHERE ID_Sabor = '" + ID_Sabor + "'";
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
                string SQLSelect = "SELECT ID_Sabor, DS_Sabor " +
                    "FROM tb_sabor WHERE Ativo = 1 AND DS_Sabor LIKE CONCAT('" + DS_Sabor + "', '%') ORDER BY ID_Sabor DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "tb_sabor";
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
                string SQLDelete = "UPDATE tb_sabor SET Ativo = 0 WHERE ID_Sabor = '" + ID_Sabor + "'";
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
                string SQLSelect = "SELECT ID_Sabor, DS_Sabor FROM tb_sabor WHERE Ativo = 1 ORDER BY ID_Sabor DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "tb_sabor";
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