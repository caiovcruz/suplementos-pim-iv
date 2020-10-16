using DataBase;
using System.Data;

namespace SuplementosPIMIV.Model
{
    public class ModelSubcategoria
    {
        private DAO dataAcessObject;

        public int ID_Subcategoria { get; set; }
        public int ID_Categoria { get; set; }
        public string NM_Subcategoria { get; set; }
        public string DS_Subcategoria { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelSubcategoria() { }

        public ModelSubcategoria(int id_categoria, string nm_subcategoria, string ds_subcategoria)
        {
            ID_Categoria = id_categoria;
            NM_Subcategoria = nm_subcategoria;
            DS_Subcategoria = ds_subcategoria;

            Incluir();
        }

        public ModelSubcategoria(int id_subcategoria, int id_categoria, string nm_subcategoria, string ds_subcategoria)
        {
            ID_Subcategoria = id_subcategoria;
            ID_Categoria = id_categoria;
            NM_Subcategoria = nm_subcategoria;
            DS_Subcategoria = ds_subcategoria;

            Alterar();
        }

        public ModelSubcategoria(string nm_subcategoria)
        {
            NM_Subcategoria = nm_subcategoria;
        }

        public ModelSubcategoria(int id_subcategoria)
        {
            ID_Subcategoria = id_subcategoria;

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
                string SQLInsert = "INSERT INTO tb_subcategoria " +
                    "(" +
                        "ID_Categoria, " +
                        "NM_Subcategoria, " +
                        "DS_Subcategoria, " +
                        "Ativo" +
                    ")" +
                    "VALUES " +
                    "(" +
                        "'" + ID_Categoria + "', " +
                        "'" + NM_Subcategoria + "', " +
                        "'" + DS_Subcategoria + "', " +
                        "1" +
                    ")";
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
                string SQLUpdate = "UPDATE tb_subcategoria SET " +
                    "ID_Categoria = '" + ID_Categoria + "', " +
                    "NM_Subcategoria = '" + NM_Subcategoria + "', " +
                    "DS_Subcategoria = '" + DS_Subcategoria + "' " +
                    "WHERE ID_Subcategoria = '" + ID_Subcategoria + "'";
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
                string SQLSelect = "SELECT " +
                    "SUB.ID_Subcategoria, " +
                    "SUB.NM_Subcategoria, " +
                    "SUB.ID_Categoria, " +
                    "CAT.NM_Categoria, " +
                    "SUB.DS_Subcategoria " +
                    "FROM tb_subcategoria AS SUB " +
                    "INNER JOIN tb_categoria AS CAT ON SUB.ID_Categoria = CAT.ID_Categoria " +
                    "WHERE SUB.Ativo = 1 " +
                    "AND SUB.NM_Subcategoria LIKE CONCAT('" + NM_Subcategoria + "', '%') " +
                    "ORDER BY SUB.ID_Subcategoria DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "tb_subcategoria";
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
                string SQLDelete = "UPDATE tb_subcategoria SET Ativo = 0 WHERE ID_Subcategoria = '" + ID_Subcategoria + "'";
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
                string SQLSelect = "SELECT " +
                    "SUB.ID_Subcategoria, " +
                    "SUB.NM_Subcategoria, " +
                    "SUB.ID_Categoria, " +
                    "CAT.NM_Categoria, " +
                    "SUB.DS_Subcategoria " +
                    "FROM tb_subcategoria AS SUB " +
                    "INNER JOIN tb_categoria AS CAT ON SUB.ID_Categoria = CAT.ID_Categoria " +
                    "WHERE SUB.Ativo = 1 " +
                    "ORDER BY SUB.ID_Subcategoria DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "tb_subcategoria";
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