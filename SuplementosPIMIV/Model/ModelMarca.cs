using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SuplementosPIMIV.Model
{
    public class ModelMarca
    {
        private DAO dataAcessObject;

        public int ID_Marca { get; set; }
        public string NM_Marca { get; set; }
        public string DS_Mensagem { get; set; }

        public ModelMarca() { }

        public ModelMarca(string nm_marca, bool incluir)
        {
            NM_Marca = nm_marca;

            if (incluir)
            {
                Incluir();
            }
        }

        public ModelMarca(int id_marca, string nm_marca)
        {
            ID_Marca = id_marca;
            NM_Marca = NM_Marca;

            Alterar();
        }

        public ModelMarca(int id_marca)
        {
            ID_Marca = id_marca;

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
                    "INSERT INTO TB_Marca (" +
                        "NM_Marca, " +
                        "Ativo) " +
                    "VALUES (" +
                        "'" + NM_Marca + "', " +
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
                    "UPDATE TB_Marca SET " +
                    "NM_Marca = '" + NM_Marca + "' " +
                    "WHERE ID_Marca = '" + ID_Marca + "'";
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
                    "ID_Marca, " +
                    "NM_Marca " +
                    "FROM TB_Marca " +
                    "WHERE Ativo = 1 " +
                    "AND NM_Marca LIKE '" + NM_Marca + "' + '%' " +
                    "ORDER BY ID_Marca DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Marca";
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
                    "UPDATE TB_Marca SET " +
                    "Ativo = 0 " +
                    "WHERE ID_Marca = '" + ID_Marca + "'";
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
                    "ID_Marca, " +
                    "NM_Marca " +
                    "FROM TB_Marca " +
                    "WHERE Ativo = 1 " +
                    "ORDER BY ID_Marca DESC";
                IDataReader dataReader = dataAcessObject.Connector.QueryWithReader(SQLSelect);
                dataTable.TableName = "TB_Marca";
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