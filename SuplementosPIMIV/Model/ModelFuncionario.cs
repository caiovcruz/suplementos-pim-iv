using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SuplementosPIMIV.Model
{
    public class ModelFuncionario
    {
        public int ID_Funcionario { get; set; }
        public string NM_Funcionario { get; set; }
        public string DS_Sexo { get; set; }
        public DateTime DT_Nascimento { get; set; }
        public string NR_CPF { get; set; }
        public string NR_Telefone { get; set; }
        public string DS_Email { get; set; }
        public string NR_CEP { get; set; }
        public string DS_Logradouro { get; set; }
        public string NR_Casa { get; set; }
        public string NM_Bairro { get; set; }
        public string DS_Complemento { get; set; }
        public string NM_Cidade { get; set; }
        public string DS_UF { get; set; }
        public string DS_Cargo { get; set; }
        public double VL_Salario { get; set; }
        public DateTime DT_Admissao { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";

        // Variaveis de Conexão
        SqlConnection sqlConnection;                            // Conexão do SGBD
        SqlCommand sqlCommand;                                  // Command que envia um 'comando' para o SGBD
        SqlDataReader sqlDataReader;                            // Retorno do Command (DataReader) espécie de tabela/leitura 'apenas pra frente'

        public ModelFuncionario(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ModelFuncionario(string nm_funcionario, string ds_sexo, DateTime dt_nascimento, string nr_cpf, string nr_telefone, 
            string ds_email, string nr_cep, string ds_logradouro, string nr_casa, string nm_bairro, string ds_complemento, string nm_cidade, 
            string ds_uf, string ds_cargo, double vl_salario, DateTime dt_admissao, string connectionString)
        {
            NM_Funcionario = nm_funcionario;
            DS_Sexo = ds_sexo;
            DT_Nascimento = dt_nascimento;
            NR_CPF = nr_cpf;
            NR_Telefone = nr_telefone;
            DS_Email = ds_email;
            NR_CEP = nr_cep;
            DS_Logradouro = ds_logradouro;
            NR_Casa = nr_casa;
            NM_Bairro = nm_bairro;
            DS_Complemento = ds_complemento;
            NM_Cidade = nm_cidade;
            DS_UF = ds_uf;
            DS_Cargo = ds_cargo;
            VL_Salario = vl_salario;
            DT_Admissao = dt_admissao;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelFuncionario(
            int id_funcionario,
            string nm_funcionario,
            string ds_sexo,
            DateTime dt_nascimento,
            string nr_cpf,
            string nr_telefone,
            string ds_email,
            string nr_cep,
            string ds_logradouro,
            string nr_casa,
            string nm_bairro,
            string ds_complemento,
            string nm_cidade,
            string ds_uf,
            string ds_cargo,
            double vl_salario,
            DateTime dt_admissao,
            string connectionString)
        {
            ID_Funcionario = id_funcionario;
            NM_Funcionario = nm_funcionario;
            DS_Sexo = ds_sexo;
            DT_Nascimento = dt_nascimento;
            NR_CPF = nr_cpf;
            NR_Telefone = nr_telefone;
            DS_Email = ds_email;
            NR_CEP = nr_cep;
            DS_Logradouro = ds_logradouro;
            NR_Casa = nr_casa;
            NM_Bairro = nm_bairro;
            DS_Complemento = ds_complemento;
            NM_Cidade = nm_cidade;
            DS_UF = ds_uf;
            DS_Cargo = ds_cargo;
            VL_Salario = vl_salario;
            DT_Admissao = dt_admissao;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelFuncionario(int id_funcionario, char acao, string connectionString)
        {
            ID_Funcionario = id_funcionario;
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

        public void Incluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO TB_Funcionario (");
                stringSQL.Append("NM_Funcionario, ");
                stringSQL.Append("DS_Sexo, ");
                stringSQL.Append("DT_Nascimento, ");
                stringSQL.Append("NR_CPF, ");
                stringSQL.Append("NR_Telefone, ");
                stringSQL.Append("DS_Email, ");
                stringSQL.Append("NR_CEP, ");
                stringSQL.Append("DS_Logradouro, ");
                stringSQL.Append("NR_Casa, ");
                stringSQL.Append("NM_Bairro, ");
                stringSQL.Append("DS_Complemento, ");
                stringSQL.Append("NM_Cidade, ");
                stringSQL.Append("DS_UF, ");
                stringSQL.Append("DS_Cargo, ");
                stringSQL.Append("VL_Salario, ");
                stringSQL.Append("DT_Admissao, ");
                stringSQL.Append("Ativo)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + NM_Funcionario + "', ");
                stringSQL.Append("'" + DS_Sexo + "', ");
                stringSQL.Append("'" + DT_Nascimento + "', ");
                stringSQL.Append("'" + NR_CPF + "', ");
                stringSQL.Append("'" + NR_Telefone + "', ");
                stringSQL.Append("'" + DS_Email + "', ");
                stringSQL.Append("'" + NR_CEP + "', ");
                stringSQL.Append("'" + DS_Logradouro + "', ");
                stringSQL.Append("'" + NR_Casa + "', ");
                stringSQL.Append("'" + NM_Bairro + "', ");
                stringSQL.Append("'" + DS_Complemento + "', ");
                stringSQL.Append("'" + NM_Cidade + "', ");
                stringSQL.Append("'" + DS_UF + "', ");
                stringSQL.Append("'" + DS_Cargo + "', ");
                stringSQL.Append("'" + VL_Salario + "', ");
                stringSQL.Append("'" + DT_Admissao + "', ");
                stringSQL.Append("1)");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao cadastrar";
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

        public void Alterar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Funcionario SET ");
                stringSQL.Append("NM_Funcionario = '" + NM_Funcionario + "', ");
                stringSQL.Append("DS_Sexo = '" + DS_Sexo + "', ");
                stringSQL.Append("DT_Nascimento = '" + DT_Nascimento + "', ");
                stringSQL.Append("NR_CPF = '" + NR_CPF + "', ");
                stringSQL.Append("NR_Telefone = '" + NR_Telefone + "', ");
                stringSQL.Append("DS_Email = '" + DS_Email + "', ");
                stringSQL.Append("NR_CEP = '" + NR_CEP + "', ");
                stringSQL.Append("DS_Logradouro = '" + DS_Logradouro + "', ");
                stringSQL.Append("NR_Casa = '" + NR_Casa + "', ");
                stringSQL.Append("NM_Bairro = '" + NM_Bairro + "', ");
                stringSQL.Append("DS_Complemento = '" + DS_Complemento + "', ");
                stringSQL.Append("NM_Cidade = '" + NM_Cidade + "', ");
                stringSQL.Append("DS_UF = '" + DS_UF + "', ");
                stringSQL.Append("DS_Cargo = '" + DS_Cargo + "', ");
                stringSQL.Append("VL_Salario = '" + VL_Salario + "', ");
                stringSQL.Append("DT_Admissao = '" + DT_Admissao + "' ");
                stringSQL.Append("WHERE ID_Funcionario = '" + ID_Funcionario + "'");

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

        public DataTable Consultar(int status, string texto)
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Funcionario, ");
                stringSQL.Append("NM_Funcionario, ");
                stringSQL.Append("DS_Sexo, ");
                stringSQL.Append("DT_Nascimento, ");
                stringSQL.Append("NR_CPF, ");
                stringSQL.Append("NR_Telefone, ");
                stringSQL.Append("DS_Email, ");
                stringSQL.Append("NR_CEP, ");
                stringSQL.Append("DS_Logradouro, ");
                stringSQL.Append("NR_Casa, ");
                stringSQL.Append("NM_Bairro, ");
                stringSQL.Append("DS_Complemento, ");
                stringSQL.Append("NM_Cidade, ");
                stringSQL.Append("DS_UF, ");
                stringSQL.Append("DS_Cargo, ");
                stringSQL.Append("FORMAT(VL_Salario, 'N2') AS VL_Salario, ");
                stringSQL.Append("DT_Admissao, ");
                stringSQL.Append("Ativo ");
                stringSQL.Append("FROM TB_Funcionario ");
                stringSQL.Append("WHERE Ativo = " + status + " ");
                stringSQL.Append("AND NM_Funcionario LIKE '" + texto + "' + '%' ");
                stringSQL.Append("ORDER BY ID_Funcionario DESC");

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

        public void Excluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Funcionario SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Funcionario = '" + ID_Funcionario + "'");

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

        public void Ativar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Funcionario SET ");
                stringSQL.Append("Ativo = 1 ");
                stringSQL.Append("WHERE ID_Funcionario = '" + ID_Funcionario + "'");

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

        public DataTable Exibir(int status)
        {
            DataTable dataTable = new DataTable();

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("ID_Funcionario, ");
                stringSQL.Append("NM_Funcionario, ");
                stringSQL.Append("DS_Sexo, ");
                stringSQL.Append("DT_Nascimento, ");
                stringSQL.Append("NR_CPF, ");
                stringSQL.Append("NR_Telefone, ");
                stringSQL.Append("DS_Email, ");
                stringSQL.Append("NR_CEP, ");
                stringSQL.Append("DS_Logradouro, ");
                stringSQL.Append("NR_Casa, ");
                stringSQL.Append("NM_Bairro, ");
                stringSQL.Append("DS_Complemento, ");
                stringSQL.Append("NM_Cidade, ");
                stringSQL.Append("DS_UF, ");
                stringSQL.Append("DS_Cargo, ");
                stringSQL.Append("FORMAT(VL_Salario, 'N2') AS VL_Salario, ");
                stringSQL.Append("DT_Admissao, ");
                stringSQL.Append("Ativo ");
                stringSQL.Append("FROM TB_Funcionario ");
                stringSQL.Append("WHERE Ativo = " + status + " ");
                stringSQL.Append("ORDER BY ID_Funcionario DESC");

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

        public string VerificarFuncionarioCadastrado(string id_funcionario, string nm_funcionario)
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("1 ");
                stringSQL.Append("FROM TB_Funcionario ");
                stringSQL.Append("WHERE ID_Funcionario != '" + id_funcionario + "' ");
                stringSQL.Append("AND NM_Funcionario = '" + nm_funcionario + "' ");
                stringSQL.Append("ORDER BY ID_Funcionario DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "Funcionário já cadastrado.";
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