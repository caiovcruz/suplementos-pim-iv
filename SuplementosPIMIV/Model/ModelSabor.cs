using DataBase;
using System.Data;

namespace SuplementosPIMIV.Model
{
    public class ModelSabor
    {
        private DAO dataAcessObject;

        public int ID_Sabor { get; set; }
        public string NM_Sabor { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelSabor() { }

        public ModelSabor(string nm_sabor, bool incluir)
        {
            NM_Sabor = nm_sabor;

            if (incluir)
            {
                Incluir();
            }
        }

        public ModelSabor(int id_sabor, string nm_sabor)
        {
            ID_Sabor = id_sabor;
            NM_Sabor = nm_sabor;

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
                    "INSERT INTO TB_Sabor (" +
                        "NM_Sabor, " +
                        "Ativo)" +
                    "VALUES (" +
                        "'" + NM_Sabor + "', " +
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
                    "UPDATE TB_Sabor SET " +
                    "NM_Sabor = '" + NM_Sabor + "' " +
                    "WHERE ID_Sabor = '" + ID_Sabor + "'";
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
                    "ID_Sabor, " +
                    "NM_Sabor " +
                    "FROM TB_Sabor " +
                    "WHERE Ativo = 1 " +
                    "AND NM_Sabor LIKE '" + NM_Sabor + "' + '%' " +
                    "ORDER BY ID_Sabor DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Sabor";
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
                    "UPDATE TB_Sabor SET " +
                    "Ativo = 0 " +
                    "WHERE ID_Sabor = '" + ID_Sabor + "'";
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
                    "ID_Sabor, " +
                    "NM_Sabor " +
                    "FROM TB_Sabor " +
                    "WHERE Ativo = 1 " +
                    "ORDER BY ID_Sabor DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Sabor";
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