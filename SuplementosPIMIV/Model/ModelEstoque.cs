﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SuplementosPIMIV.Model
{
    public class ModelEstoque
    {
        public int ID_Produto { get; set; }
        public int QTD_Estoque { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelEstoque(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelEstoque(int id_produto, int qtd_estoque, string connectionString)
        {
            ID_Produto = id_produto;
            QTD_Estoque = qtd_estoque;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelEstoque(int id_produto, string connectionString)
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
                stringSQL.Append("INSERT INTO TB_Estoque (");
                stringSQL.Append("ID_Produto, ");
                stringSQL.Append("QTD_Estoque, ");
                stringSQL.Append("Ativo)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_Produto + "', ");
                stringSQL.Append("'" + QTD_Estoque + "', ");
                stringSQL.Append("1)");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao cadastrar o estoque";
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

        public void Excluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Estoque SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Produto = '" + ID_Produto + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao excluir o estoque";
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
    }
}