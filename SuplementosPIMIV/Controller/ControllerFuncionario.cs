using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerFuncionario
    {
        private ModelFuncionario myModelFuncionario;
        private Validar myValidar;
        public string DS_Mensagem { get; set; }

        public ControllerFuncionario() { }

        public ControllerFuncionario(string nm_funcionario, string ds_sexo, string diaNascimentoFuncionario, string mesNascimentoFuncionario, string anoNascimentoFuncionario,
            string nr_cpf, string nr_telefone, string ds_email, string nr_cep, string ds_logradouro, string nr_casa, string nm_bairro, string ds_complemento, string id_uf,
            string id_cidade, string ds_cargo, string vl_salario, DateTime dt_admissao, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields("", nm_funcionario, ds_sexo, diaNascimentoFuncionario, mesNascimentoFuncionario, anoNascimentoFuncionario, nr_cpf, nr_telefone, ds_email, nr_cep,
                ds_logradouro, nr_casa, nm_bairro, ds_complemento, id_uf, id_cidade, ds_cargo, vl_salario, dt_admissao, connectionString);

            if (mDs_Msg == "")
            {
                // criando a data de nascimento com datetime
                DateTime dt_nascimento = new DateTime(
                    Convert.ToInt32(anoNascimentoFuncionario),
                    Convert.ToInt32(mesNascimentoFuncionario),
                    Convert.ToInt32(diaNascimentoFuncionario));

                // tudo certinho
                // instanciar um objeto da classe funcionário, carregar tela e incluir
                myModelFuncionario = new ModelFuncionario(nm_funcionario, ds_sexo, dt_nascimento, nr_cpf, nr_telefone, ds_email, nr_cep, ds_logradouro, nr_casa, nm_bairro, ds_complemento,
                    Convert.ToInt32(id_uf), Convert.ToInt32(id_cidade), ds_cargo, Convert.ToDouble(vl_salario), dt_admissao, connectionString);
                DS_Mensagem = myModelFuncionario.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerFuncionario(string id_funcionario, string nm_funcionario, string ds_sexo, string diaNascimentoFuncionario, string mesNascimentoFuncionario, string anoNascimentoFuncionario,
            string nr_cpf, string nr_telefone, string ds_email, string nr_cep, string ds_logradouro, string nr_casa, string nm_bairro, string ds_complemento, string id_uf,
            string id_cidade, string ds_cargo, string vl_salario, DateTime dt_admissao, string connectionString)
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields(id_funcionario, nm_funcionario, ds_sexo, diaNascimentoFuncionario, mesNascimentoFuncionario, anoNascimentoFuncionario, nr_cpf, nr_telefone, ds_email, nr_cep,
                ds_logradouro, nr_casa, nm_bairro, ds_complemento, id_uf, id_cidade, ds_cargo, vl_salario, dt_admissao, connectionString);

            if (mDs_Msg == "")
            {
                // criando a data de nascimento com datetime
                DateTime dt_nascimento = new DateTime(
                    Convert.ToInt32(anoNascimentoFuncionario),
                    Convert.ToInt32(mesNascimentoFuncionario),
                    Convert.ToInt32(diaNascimentoFuncionario));

                // tudo certinho
                // instanciar um objeto da classe funcionário, carregar tela e alterar
                myModelFuncionario = new ModelFuncionario(Convert.ToInt32(id_funcionario), nm_funcionario, ds_sexo, dt_nascimento, nr_cpf, nr_telefone, ds_email, nr_cep, ds_logradouro, nr_casa,
                    nm_bairro, ds_complemento, Convert.ToInt32(id_uf), Convert.ToInt32(id_cidade), ds_cargo, Convert.ToDouble(vl_salario), dt_admissao, connectionString);
                DS_Mensagem = myModelFuncionario.DS_Mensagem;
            }
            else
            {
                // exibir erro!
                DS_Mensagem = mDs_Msg;
            }
        }

        public ControllerFuncionario(string id_funcionario, char acao, string connectionString)
        {
            myModelFuncionario = new ModelFuncionario(Convert.ToInt32(id_funcionario), acao, connectionString);
            DS_Mensagem = myModelFuncionario.DS_Mensagem;
        }

        public DataTable Exibir(string status, string connectionString)
        {
            myModelFuncionario = new ModelFuncionario();
            return myModelFuncionario.Exibir(Convert.ToInt32(status), connectionString);
        }

        public DataTable Consultar(string status, string filtro, string texto, string connectionString)
        {
            myModelFuncionario = new ModelFuncionario();
            return myModelFuncionario.Consultar(Convert.ToInt32(status), filtro, texto, connectionString);
        }

        public string VerificarFuncionarioCadastrado(string id_funcionario, string nr_cpf, string connectionString)
        {
            myModelFuncionario = new ModelFuncionario();
            return myModelFuncionario.VerificarFuncionarioCadastrado(id_funcionario, nr_cpf, connectionString);
        }

        public DataTable ExibirUFs(string connectionString)
        {
            myModelFuncionario = new ModelFuncionario();
            return myModelFuncionario.ExibirUFs(connectionString);
        }

        public DataTable ExibirCidades(string id_uf, string connectionString)
        {
            myModelFuncionario = new ModelFuncionario();
            return myModelFuncionario.ExibirCidades(Convert.ToInt32(id_uf), connectionString);
        }

        private string ValidateFields(string id_funcionario, string nm_funcionario, string ds_sexo, string diaNascimentoFuncionario, string mesNascimentoFuncionario, string anoNascimentoFuncionario,
            string nr_cpf, string nr_telefone, string ds_email, string nr_cep, string ds_logradouro, string nr_casa, string nm_bairro, string ds_complemento, string id_uf,
            string id_cidade, string ds_cargo, string vl_salario, DateTime dt_admissao, string connectionString)
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(nm_funcionario))
            {
                if (!myValidar.TamanhoCampo(nm_funcionario, 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + nm_funcionario.Length + ".";
                }
                else
                {
                    if (!myValidar.Letra(nm_funcionario))
                    {
                        mDs_Msg = " O nome deve conter somente letras";
                    }
                    else
                    {
                        if (myValidar.CampoPreenchido(nr_cpf))
                        {
                            if (!myValidar.ValidaCPF(nr_cpf))
                            {
                                mDs_Msg = " CPF inválido, por favor verifique e tente novamente.";
                            }
                            else
                            {
                                if (VerificarFuncionarioCadastrado(id_funcionario, nr_cpf.Replace(".", "").Replace("-", ""), connectionString).Equals(""))
                                {
                                    if (diaNascimentoFuncionario.Equals("Dia"))
                                    {
                                        mDs_Msg += " É necessário selecionar o dia do nascimento.";
                                    }

                                    if (mesNascimentoFuncionario.Equals("0"))
                                    {
                                        mDs_Msg += " É necessário selecionar o mês do nascimento.";
                                    }

                                    if (anoNascimentoFuncionario.Equals("Ano"))
                                    {
                                        mDs_Msg += " É necessário selecionar o ano do nascimento.";
                                    }

                                    if (ds_sexo.Equals("Sexo"))
                                    {
                                        mDs_Msg += " É necessário selecionar o sexo.";
                                    }

                                    if (myValidar.CampoPreenchido(nr_telefone))
                                    {
                                        if (!myValidar.Numero(nr_telefone.Replace("(", "").Replace(")", "").Replace("-", "")))
                                        {
                                            mDs_Msg += " O telefone deve conter somente números.";
                                        }
                                        else
                                        {
                                            if (nr_telefone.Replace("(", "").Replace(")", "").Replace("-", "").Length < 10)
                                            {
                                                mDs_Msg += " O telefone deve conter ao menos 10 números, contando com o DDD.";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mDs_Msg += " O telefone deve estar preenchido.";
                                    }

                                    if (myValidar.CampoPreenchido(ds_email))
                                    {
                                        if (!myValidar.TamanhoCampo(ds_email, 35))
                                        {
                                            mDs_Msg += " Limite de caracteres para e-mail excedido, " +
                                                          "o limite para este campo é: 35 caracteres, " +
                                                          "quantidade utilizada: " + ds_email.Length + ".";
                                        }
                                    }
                                    else
                                    {
                                        mDs_Msg += " O e-mail deve estar preenchido.";
                                    }

                                    if (myValidar.CampoPreenchido(nr_cep))
                                    {
                                        if (!myValidar.Numero(nr_cep.Replace("-", "")))
                                        {
                                            mDs_Msg += " O CEP deve conter somente números.";
                                        }
                                        else
                                        {
                                            if (nr_cep.Replace("-", "").Length < 8)
                                            {
                                                mDs_Msg += " O CEP deve conter ao menos 8 números.";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mDs_Msg += " O CEP deve estar preenchido.";
                                    }

                                    if (myValidar.CampoPreenchido(ds_logradouro))
                                    {
                                        if (!myValidar.TamanhoCampo(ds_logradouro, 50))
                                        {
                                            mDs_Msg += " Limite de caracteres para logradouro excedido, " +
                                                          "o limite para este campo é: 3000 caracteres, " +
                                                          "quantidade utilizada: " + ds_logradouro.Length + ".";
                                        }
                                    }
                                    else
                                    {
                                        mDs_Msg += " O logradouro deve estar preenchido.";
                                    }

                                    if (myValidar.CampoPreenchido(nr_casa))
                                    {
                                        if (!myValidar.TamanhoCampo(nr_casa, 5))
                                        {
                                            mDs_Msg += " Limite de caracteres para número da casa excedido, " +
                                                          "o limite para este campo é: 5 caracteres, " +
                                                          "quantidade utilizada: " + nr_casa.Length + ".";
                                        }
                                        else
                                        {
                                            if (!myValidar.Numero(nr_casa))
                                            {
                                                mDs_Msg += " O número da casa deve conter somente números.";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mDs_Msg += " O número da casa deve estar preenchida.";
                                    }

                                    if (myValidar.CampoPreenchido(nm_bairro))
                                    {
                                        if (!myValidar.TamanhoCampo(nm_bairro, 50))
                                        {
                                            mDs_Msg += " Limite de caracteres para bairro excedido, " +
                                                          "o limite para este campo é: 3000 caracteres, " +
                                                          "quantidade utilizada: " + nm_bairro.Length + ".";
                                        }
                                    }
                                    else
                                    {
                                        mDs_Msg += " O bairro deve estar preenchido.";
                                    }

                                    if (!string.IsNullOrWhiteSpace(ds_complemento))
                                    {
                                        if (!myValidar.TamanhoCampo(ds_complemento, 50))
                                        {
                                            mDs_Msg += " Limite de caracteres para complemento excedido, " +
                                                          "o limite para este campo é: 50 caracteres, " +
                                                          "quantidade utilizada: " + ds_complemento.Length + ".";
                                        }
                                    }

                                    if (ds_cargo.Equals("Cargo"))
                                    {
                                        mDs_Msg += " É necessário selecionar o cargo.";
                                    }

                                    if (myValidar.CampoPreenchido(vl_salario))
                                    {
                                        if (!myValidar.Valor(vl_salario))
                                        {
                                            mDs_Msg += " O salário deve ser um valor numérico, no formato: 9.999.999,99.";
                                        }
                                    }
                                    else
                                    {
                                        mDs_Msg += " O salário deve estar preenchido.";
                                    }

                                    if (id_uf.Equals("Estado"))
                                    {
                                        mDs_Msg += " É necessário selecionar a UF.";
                                    }

                                    if (id_cidade.Equals("Cidade"))
                                    {
                                        mDs_Msg += " É necessário selecionar a cidade.";
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " Funcionário já cadastrado com este CPF. Verifique nos funcionários ativos e inativos!";
                                }
                            }
                        }
                        else
                        {
                            mDs_Msg += " O CPF deve estar preenchido.";
                        }
                    }
                }
            }
            else
            {
                mDs_Msg = " O nome deve estar preenchido.";
            }

            return mDs_Msg;
        }
    }
}