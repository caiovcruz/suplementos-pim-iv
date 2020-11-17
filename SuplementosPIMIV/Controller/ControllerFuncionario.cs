using SuplementosPIMIV.Model;
using System;

namespace SuplementosPIMIV.Controller
{
    public class ControllerFuncionario : ModelFuncionario
    {
        public ControllerFuncionario(string connectionString) : base(connectionString) { }
        public ControllerFuncionario(string nm_funcionario, string ds_sexo, DateTime dt_nascimento, string nr_cpf, string nr_telefone,
            string ds_email, string nr_cep, string ds_logradouro, string nr_casa, string nm_bairro, string ds_complemento, int id_uf,
            int id_cidade, string ds_cargo, double vl_salario, DateTime dt_admissao, string connectionString) : 
            base(nm_funcionario, ds_sexo, dt_nascimento, nr_cpf, nr_telefone, ds_email, nr_cep, ds_logradouro, nr_casa, nm_bairro, 
                ds_complemento, id_uf, id_cidade, ds_cargo, vl_salario, dt_admissao, connectionString) { }
        public ControllerFuncionario(int id_funcionario, string nm_funcionario, string ds_sexo, DateTime dt_nascimento, string nr_cpf, string nr_telefone,
            string ds_email, string nr_cep, string ds_logradouro, string nr_casa, string nm_bairro, string ds_complemento, int id_uf,
            int id_cidade, string ds_cargo, double vl_salario, DateTime dt_admissao, string connectionString) : 
            base(id_funcionario, nm_funcionario, ds_sexo, dt_nascimento, nr_cpf, nr_telefone, ds_email, nr_cep, ds_logradouro, nr_casa, nm_bairro, 
                ds_complemento, id_uf, id_cidade, ds_cargo, vl_salario, dt_admissao, connectionString) { }

        public ControllerFuncionario(int id_funcionario, char acao, string connectionString) : base(id_funcionario, acao, connectionString) { }
    }
}