using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PontoDeVenda.Model
{
    public class ModelSubcategoria
    {
        private DAO dataAcessObject;

        public int ID_Subcategoria { get; set; }
        public string NM_Subcategoria { get; set; }
        public string DS_Subcategoria { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelSubcategoria() { }

        public ModelSubcategoria(string nm_subcategoria, string ds_subcategoria)
        {
            NM_Subcategoria = nm_subcategoria;
            DS_Subcategoria = ds_subcategoria;

            Incluir();
        }

        public ModelSubcategoria(int id_subcategoria, string nm_subcategoria, string ds_subcategoria)
        {
            ID_Subcategoria = id_subcategoria;
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
                "Database=ponto_de_venda; " +
                "Uid=root; " +
                "Pwd=011118");
        }

        public void Incluir()
        {
            DS_Mensagem = "";

            SetConnection();
            if (dataAcessObject.Connector.Open())
            {
                string SQLInsert = "INSERT INTO tb_subcategoria (NM_Subcategoria, DS_Subcategoria, Ativo)" +
                    "VALUES ('" + NM_Subcategoria + "','" + DS_Subcategoria + "', 1)";
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
                string SQLUpdate = "UPDATE tb_subcategoria SET NM_Subcategoria = '" + NM_Subcategoria + "', " +
                    "DS_Subcategoria = '" + DS_Subcategoria + "' WHERE ID_Subcategoria = '" + ID_Subcategoria + "'";
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
                string SQLSelect = "SELECT ID_Subcategoria, NM_Subcategoria, DS_Subcategoria " +
                    "FROM tb_subcategoria WHERE Ativo = 1 AND NM_Subcategoria LIKE CONCAT('" + NM_Subcategoria + "', '%') " +
                    "ORDER BY ID_Subcategoria DESC";
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
                string SQLSelect = "SELECT ID_Subcategoria, NM_Subcategoria, DS_Subcategoria " +
                    "FROM tb_subcategoria WHERE Ativo = 1 ORDER BY ID_Subcategoria DESC";
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