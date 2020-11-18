using SuplementosPIMIV.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validacao;

namespace SuplementosPIMIV.View
{
    public partial class FrmFuncionario : System.Web.UI.Page
    {
        private Validar myValidar;
        private ControllerFuncionario myControllerFuncionario;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimparCampos();
                CarregarFuncionarios();
                CarregarSexo();
                CarregarDatasNascimento();
                CarregarCargos();
                CarregarUFs();
                CarregarFiltrosDeBusca();
                BloquearComponentesCadastro();
                BloquearComponentesExibe();
            }
        }

        private void LimparCampos()
        {
            txbID_Funcionario.Text = "";
            txbNM_Funcionario.Text = "";
            txbNR_CPF.Text = "";
            ddlDiaNascimentoFuncionario.SelectedIndex = 0;
            ddlMesNascimentoFuncionario.SelectedIndex = 0;
            ddlAnoNascimentoFuncionario.SelectedIndex = 0;
            ddlDS_Sexo.SelectedIndex = 0;
            txbNR_Telefone.Text = "";
            txbDS_Email.Text = "";
            txbNR_CEP.Text = "";
            txbDS_Logradouro.Text = "";
            txbNR_Casa.Text = "";
            txbNM_Bairro.Text = "";
            txbDS_Complemento.Text = "";
            ddlDS_Cargo.SelectedIndex = 0;
            txbVL_Salario.Text = "";
            txbDT_Admissao.Text = DateTime.Now.ToString();
            txbDT_Admissao.Enabled = false;
            ddlDS_UF.SelectedIndex = 0;
            ddlNM_Cidade.DataSource = null;
            ddlNM_Cidade.Enabled = false;
            lblDS_Mensagem.Text = "";
        }

        private void BloquearComponentesCadastro()
        {
            btnAtivarStatus.Enabled = false;
            btnIncluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
        }

        private void BloquearComponentesExibe()
        {
            btnConsultar.Enabled = false;
            txbConsultar.Enabled = false;
        }

        private void CarregarFuncionarios()
        {
            // instanciando um objeto da classe ControllerFuncionario
            myControllerFuncionario = new ControllerFuncionario(Session["ConnectionString"].ToString());

            // passando a fonte de dados para o GridView
            gvwExibe.DataSource = myControllerFuncionario.Exibir(chkStatusInativo.Checked ? 0 : 1);

            // associando os dados para carregar e exibir
            gvwExibe.DataBind();
        }

        private void CarregarSexo()
        {
            ddlDS_Sexo.Items.Insert(0, "Sexo");
            ddlDS_Sexo.Items.Insert(1, "Masculino");
            ddlDS_Sexo.Items.Insert(2, "Feminino");

            ddlDS_Sexo.SelectedIndex = 0;
        }

        private void CarregarDatasNascimento()
        {
            ddlDiaNascimentoFuncionario.Items.Insert(0, "Dia");

            for (int i = 1; i <= 31 ; i++)
            {
                ddlDiaNascimentoFuncionario.Items.Insert(i, Convert.ToString(i));
            }                    

            ddlMesNascimentoFuncionario.Items.Insert(0, "Mês");
            ddlMesNascimentoFuncionario.Items.Insert(1, "Janeiro");
            ddlMesNascimentoFuncionario.Items.Insert(2, "Fevereiro");
            ddlMesNascimentoFuncionario.Items.Insert(3, "Março");
            ddlMesNascimentoFuncionario.Items.Insert(4, "Abril");
            ddlMesNascimentoFuncionario.Items.Insert(5, "Maio");
            ddlMesNascimentoFuncionario.Items.Insert(6, "Junho");
            ddlMesNascimentoFuncionario.Items.Insert(7, "Julho");
            ddlMesNascimentoFuncionario.Items.Insert(8, "Agosto");
            ddlMesNascimentoFuncionario.Items.Insert(9, "Setembro");
            ddlMesNascimentoFuncionario.Items.Insert(10, "Outubro");
            ddlMesNascimentoFuncionario.Items.Insert(11, "Novembro");
            ddlMesNascimentoFuncionario.Items.Insert(12, "Dezembro");

            ddlMesNascimentoFuncionario.SelectedIndex = 0;

            ddlAnoNascimentoFuncionario.Items.Insert(0, "Ano");

            for (int i = DateTime.Today.Year; i >= (DateTime.Today.Year - 100); i--)
            {
                ddlAnoNascimentoFuncionario.Items.Add(Convert.ToString(i));
            }

            ddlAnoNascimentoFuncionario.SelectedIndex = 0;
        }

        private void CarregarCargos()
        {
            ddlDS_Cargo.Items.Insert(0, "Cargo");
            ddlDS_Cargo.Items.Insert(1, "Vendedor");
            ddlDS_Cargo.Items.Insert(2, "Gerente");

            ddlDS_Cargo.SelectedIndex = 0;
        }

        private void CarregarUFs()
        {
            myControllerFuncionario = new ControllerFuncionario(Session["ConnectionString"].ToString());

            ddlDS_UF.DataSource = myControllerFuncionario.ExibirUFs();
            ddlDS_UF.DataTextField = "DS_UF";
            ddlDS_UF.DataValueField = "ID_UF";
            ddlDS_UF.DataBind();

            ddlDS_UF.Items.Insert(0, "Estado");
            ddlDS_UF.SelectedIndex = 0;
        }

        private void CarregarCidades()
        {
            myControllerFuncionario = new ControllerFuncionario(Session["ConnectionString"].ToString());

            ddlNM_Cidade.DataSource = myControllerFuncionario.ExibirCidades(Convert.ToInt32(ddlDS_UF.SelectedValue));
            ddlNM_Cidade.DataTextField = "NM_Cidade";
            ddlNM_Cidade.DataValueField = "ID_Cidade";
            ddlNM_Cidade.DataBind();

            ddlNM_Cidade.Items.Insert(0, "Cidade");
            ddlNM_Cidade.SelectedIndex = 0;
        }

        private void CarregarFuncionariosConsultar()
        {
            // validar a entrada de dados para consulta
            myValidar = new Validar();
            string mDs_Msg = (myValidar.TamanhoCampo(txbConsultar.Text.Trim(), 50)) ? "" : " Limite de caracteres para o nome excedido, " +
                                                                                              "o limite para este campo é: 50 caracteres, " +
                                                                                              "quantidade utilizada: " + txbConsultar.Text.Trim().Length + "."; ;

            if (mDs_Msg == "")
            {
                // tudo certinho
                // instanciar um objeto da classe produto, carregar tela e consultar
                myControllerFuncionario = new ControllerFuncionario(Session["ConnectionString"].ToString());

                string filtro = "";

                if (ddlFiltro.SelectedValue.Equals("ID")) filtro = "FUNC.ID_Funcionario";
                if (ddlFiltro.SelectedValue.Equals("Nome")) filtro = "FUNC.NM_Funcionario";
                if (ddlFiltro.SelectedValue.Equals("CPF")) filtro = "FUNC.NR_CPF";
                if (ddlFiltro.SelectedValue.Equals("Telefone")) filtro = "FUNC.NR_Telefone";
                if (ddlFiltro.SelectedValue.Equals("CEP")) filtro = "FUNC.NR_CEP";
                if (ddlFiltro.SelectedValue.Equals("Cargo")) filtro = "FUNC.DS_Cargo";

                if (ddlFiltro.SelectedValue.Equals("Salário"))
                {
                    filtro = "FUNC.VL_Salario";
                    txbConsultar.Text = txbConsultar.Text.Trim().Replace(",", ".");
                }

                if (ddlFiltro.SelectedValue.Equals("UF")) filtro = "UF.DS_UF";
                if (ddlFiltro.SelectedValue.Equals("Cidade")) filtro = "CID.NM_Cidade";

                gvwExibe.DataSource = myControllerFuncionario.Consultar(chkStatusInativo.Checked ? 0 : 1, filtro, txbConsultar.Text.Trim());
                gvwExibe.DataBind();
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void CarregarFiltrosDeBusca()
        {
            ddlFiltro.Items.Insert(0, "Filtro");
            ddlFiltro.Items.Insert(1, "ID");
            ddlFiltro.Items.Insert(2, "Nome");
            ddlFiltro.Items.Insert(3, "CPF");
            ddlFiltro.Items.Insert(4, "Telefone");
            ddlFiltro.Items.Insert(5, "CEP");
            ddlFiltro.Items.Insert(6, "Cargo");
            ddlFiltro.Items.Insert(7, "Salário");
            ddlFiltro.Items.Insert(8, "UF");
            ddlFiltro.Items.Insert(9, "Cidade");

            ddlFiltro.SelectedIndex = 0;
        }

        private void IncludeFields()
        {
            btnIncluir.Enabled =
                txbID_Funcionario.Text.Trim().Length == 0 &&
                txbNM_Funcionario.Text.Trim().Length > 0 &&
                txbNR_CPF.Text.Trim().Length > 0 &&
                ddlDiaNascimentoFuncionario.SelectedIndex != 0 &&
                ddlMesNascimentoFuncionario.SelectedIndex != 0 &&
                ddlAnoNascimentoFuncionario.SelectedIndex != 0 &&
                ddlDS_Sexo.SelectedIndex != 0 &&
                txbNR_Telefone.Text.Trim().Length > 0 &&
                txbDS_Email.Text.Trim().Length > 0 &&
                txbNR_CEP.Text.Trim().Length > 0 &&
                txbDS_Logradouro.Text.Trim().Length > 0 &&
                txbNR_Casa.Text.Trim().Length > 0 &&
                txbNM_Bairro.Text.Trim().Length > 0 &&
                ddlDS_Cargo.SelectedIndex != 0 &&
                txbVL_Salario.Text.Trim().Length > 0 &&
                txbDT_Admissao.Text.Trim().Length > 0 &&
                ddlDS_UF.SelectedIndex != 0 &&
                ddlNM_Cidade.SelectedIndex != 0;



            btnLimpar.Enabled =
                txbNM_Funcionario.Text.Trim().Length > 0 ||
                txbNR_CPF.Text.Trim().Length > 0 ||
                ddlDiaNascimentoFuncionario.SelectedIndex != 0 ||
                ddlMesNascimentoFuncionario.SelectedIndex != 0 ||
                ddlAnoNascimentoFuncionario.SelectedIndex != 0 ||
                ddlDS_Sexo.SelectedIndex != 0 ||
                txbNR_Telefone.Text.Trim().Length > 0 ||
                txbDS_Email.Text.Trim().Length > 0 ||
                txbNR_CEP.Text.Trim().Length > 0 ||
                txbDS_Logradouro.Text.Trim().Length > 0 ||
                txbNR_Casa.Text.Trim().Length > 0 ||
                txbNM_Bairro.Text.Trim().Length > 0 ||
                txbDS_Complemento.Text.Trim().Length > 0 ||
                ddlDS_Cargo.SelectedIndex != 0 ||
                txbVL_Salario.Text.Trim().Length > 0 ||
                txbDT_Admissao.Text.Trim().Length > 0 ||
                ddlDS_UF.SelectedIndex != 0 ||
                ddlNM_Cidade.SelectedIndex != 0;
        }

        private string ValidateFields()
        {
            // validar a entrada de dados para incluir
            myValidar = new Validar();
            myControllerFuncionario = new ControllerFuncionario(Session["ConnectionString"].ToString());
            string mDs_Msg = "";

            if (myValidar.CampoPreenchido(txbNM_Funcionario.Text.Trim()))
            {
                if (!myValidar.TamanhoCampo(txbNM_Funcionario.Text.Trim(), 50))
                {
                    mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + txbNM_Funcionario.Text.Trim().Length + ".";
                }
                else
                {
                    if (myValidar.CampoPreenchido(txbNR_CPF.Text.Trim()))
                    {
                        if (!myValidar.ValidaCPF(txbNR_CPF.Text.Trim()))
                        {
                            mDs_Msg = " CPF inválido, por favor verifique e tente novamente.";
                        }
                        else
                        {
                            if (myControllerFuncionario.VerificarFuncionarioCadastrado(txbID_Funcionario.Text.Trim(), txbNR_CPF.Text.Trim().Replace(".", "").Replace("-", "")).Equals(""))
                            {
                                if (ddlDiaNascimentoFuncionario.SelectedIndex.Equals(0))
                                {
                                    mDs_Msg += " É necessário selecionar o dia do nascimento.";
                                }

                                if (ddlMesNascimentoFuncionario.SelectedIndex.Equals(0))
                                {
                                    mDs_Msg += " É necessário selecionar o mês do nascimento.";
                                }

                                if (ddlAnoNascimentoFuncionario.SelectedIndex.Equals(0))
                                {
                                    mDs_Msg += " É necessário selecionar o ano do nascimento.";
                                }

                                if (ddlDS_Sexo.SelectedIndex.Equals(0))
                                {
                                    mDs_Msg += " É necessário selecionar o sexo.";
                                }

                                if (myValidar.CampoPreenchido(txbNR_Telefone.Text.Trim()))
                                {
                                    if (!myValidar.Numero(txbNR_Telefone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "")))
                                    {
                                        mDs_Msg += " O telefone deve conter somente números.";
                                    }
                                    else
                                    {
                                        if (txbNR_Telefone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Length < 10)
                                        {
                                            mDs_Msg += " O telefone deve conter ao menos 10 números, contando com o DDD.";
                                        }
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " O telefone deve estar preenchido.";
                                }

                                if (myValidar.CampoPreenchido(txbDS_Email.Text.Trim()))
                                {
                                    if (!myValidar.TamanhoCampo(txbDS_Email.Text.Trim(), 35))
                                    {
                                        mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                      "o limite para este campo é: 3000 caracteres, " +
                                                      "quantidade utilizada: " + txbDS_Email.Text.Trim().Length + ".";
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " O e-mail deve estar preenchido.";
                                }

                                if (myValidar.CampoPreenchido(txbNR_CEP.Text.Trim()))
                                {
                                    if (!myValidar.Numero(txbNR_CEP.Text.Trim().Replace("-", "")))
                                    {
                                        mDs_Msg += " O CEP deve conter somente números.";
                                    }
                                    else
                                    {
                                        if (txbNR_CEP.Text.Trim().Replace("-", "").Length < 8)
                                        {
                                            mDs_Msg += " O CEP deve conter ao menos 8 números.";
                                        }
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " O CEP deve estar preenchido.";
                                }

                                if (myValidar.CampoPreenchido(txbDS_Logradouro.Text.Trim()))
                                {
                                    if (!myValidar.TamanhoCampo(txbDS_Logradouro.Text.Trim(), 50))
                                    {
                                        mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                      "o limite para este campo é: 3000 caracteres, " +
                                                      "quantidade utilizada: " + txbDS_Logradouro.Text.Trim().Length + ".";
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " O logradouro deve estar preenchido.";
                                }

                                if (myValidar.CampoPreenchido(txbNM_Bairro.Text.Trim()))
                                {
                                    if (!myValidar.TamanhoCampo(txbNM_Bairro.Text.Trim(), 50))
                                    {
                                        mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                      "o limite para este campo é: 3000 caracteres, " +
                                                      "quantidade utilizada: " + txbNM_Bairro.Text.Trim().Length + ".";
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " O bairro deve estar preenchido.";
                                }

                                if (!string.IsNullOrWhiteSpace(txbDS_Complemento.Text.Trim()))
                                {
                                    if (!myValidar.TamanhoCampo(txbDS_Complemento.Text.Trim(), 50))
                                    {
                                        mDs_Msg += " Limite de caracteres para descrição excedido, " +
                                                      "o limite para este campo é: 3000 caracteres, " +
                                                      "quantidade utilizada: " + txbDS_Complemento.Text.Trim().Length + ".";
                                    }
                                }

                                if (ddlDS_Cargo.SelectedIndex.Equals(0))
                                {
                                    mDs_Msg += " É necessário selecionar o cargo.";
                                }

                                if (myValidar.CampoPreenchido(txbVL_Salario.Text.Trim()))
                                {
                                    if (!myValidar.Valor(txbVL_Salario.Text.Trim()))
                                    {
                                        mDs_Msg += " O salário deve ser um valor numérico, no formato: 9.999.999,99.";
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " O salário deve estar preenchido.";
                                }

                                if (ddlDS_UF.SelectedIndex.Equals(0))
                                {
                                    mDs_Msg += " É necessário selecionar a UF.";
                                }

                                if (ddlNM_Cidade.SelectedIndex.Equals(0))
                                {
                                    mDs_Msg += " É necessário selecionar a cidade.";
                                }
                            }
                            else
                            {
                                mDs_Msg += " " + myControllerFuncionario.DS_Mensagem + " Verifique nos funcionários ativos e inativos!";
                            }
                        }
                    }
                    else
                    {
                        mDs_Msg += " O CPF deve estar preenchido.";
                    }
                }
            }
            else
            {
                mDs_Msg = " O nome deve estar preenchido.";
            }

            return mDs_Msg;
        }

        private void Incluir()
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                // criando a data de nascimento com datetime
                DateTime date = new DateTime(
                    Convert.ToInt32(ddlAnoNascimentoFuncionario.SelectedValue),
                    ddlMesNascimentoFuncionario.SelectedIndex,
                    Convert.ToInt32(ddlDiaNascimentoFuncionario.SelectedValue));

                // tudo certinho
                // instanciar um objeto da classe funcionário, carregar tela e incluir
                myControllerFuncionario = new ControllerFuncionario(
                    txbNM_Funcionario.Text.Trim(),
                    ddlDS_Sexo.SelectedValue,
                    date,
                    txbNR_CPF.Text.Trim(),
                    txbNR_Telefone.Text.Trim(),
                    txbDS_Email.Text.Trim(),
                    txbNR_CEP.Text.Trim(),
                    txbDS_Logradouro.Text.Trim(),
                    txbNR_Casa.Text.Trim(),
                    txbNM_Bairro.Text.Trim(),
                    txbDS_Complemento.Text.Trim(),
                    Convert.ToInt32(ddlDS_UF.SelectedValue),
                    Convert.ToInt32(ddlNM_Cidade.SelectedValue),
                    ddlDS_Cargo.SelectedValue,
                    Convert.ToDouble(txbVL_Salario.Text.Trim()),
                    DateTime.Now,
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerFuncionario.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearComponentesCadastro();
                    CarregarFuncionarios();
                    lblDS_Mensagem.Text = "Incluído com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerFuncionario.DS_Mensagem;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void Alterar()
        {
            // validar a entrada de dados para inclusão
            string mDs_Msg = ValidateFields();

            if (mDs_Msg == "")
            {
                // criando a data de nascimento com datetime
                DateTime date = new DateTime(
                    Convert.ToInt32(ddlAnoNascimentoFuncionario.SelectedValue),
                    ddlMesNascimentoFuncionario.SelectedIndex,
                    Convert.ToInt32(ddlDiaNascimentoFuncionario.SelectedValue));

                // tudo certinho
                // instanciar um objeto da classe funcionário, carregar tela e alterar
                myControllerFuncionario = new ControllerFuncionario(
                    Convert.ToInt32(txbID_Funcionario.Text.Trim()),
                    txbNM_Funcionario.Text.Trim(),
                    ddlDS_Sexo.SelectedValue,
                    date,
                    txbNR_CPF.Text.Trim(),
                    txbNR_Telefone.Text.Trim(),
                    txbDS_Email.Text.Trim(),
                    txbNR_CEP.Text.Trim(),
                    txbDS_Logradouro.Text.Trim(),
                    txbNR_Casa.Text.Trim(),
                    txbNM_Bairro.Text.Trim(),
                    txbDS_Complemento.Text.Trim(),
                    Convert.ToInt32(ddlDS_UF.SelectedValue),
                    Convert.ToInt32(ddlNM_Cidade.SelectedValue),
                    ddlDS_Cargo.SelectedValue,
                    Convert.ToDouble(txbVL_Salario.Text.Trim()),
                    Convert.ToDateTime(txbDT_Admissao.Text.Trim()),
                    Session["ConnectionString"].ToString());

                // o que ocorreu?
                if (myControllerFuncionario.DS_Mensagem == "OK")
                {
                    // tudo certinho!
                    LimparCampos();
                    BloquearComponentesCadastro();
                    CarregarFuncionarios();
                    lblDS_Mensagem.Text = "Alterado com sucesso!";
                }
                else
                {
                    // exibir erro!
                    lblDS_Mensagem.Text = myControllerFuncionario.DS_Mensagem;
                }
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = mDs_Msg;
            }
        }

        private void Excluir()
        {
            // instanciar um objeto da classe funcionário e carregar tela e excluir
            myControllerFuncionario = new ControllerFuncionario(Convert.ToInt32(txbID_Funcionario.Text.Trim()), 'E', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerFuncionario.DS_Mensagem == "OK")
            {
                // tudo certinho!
                LimparCampos();
                BloquearComponentesCadastro();
                CarregarFuncionarios();
                lblDS_Mensagem.Text = "Excluído com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerFuncionario.DS_Mensagem;
            }
        }

        private void Ativar()
        {
            // instanciar um objeto da classe funcionário e carregar tela e ativar
            myControllerFuncionario = new ControllerFuncionario(Convert.ToInt32(txbID_Funcionario.Text.Trim()), 'A', Session["ConnectionString"].ToString());

            // o que ocorreu?
            if (myControllerFuncionario.DS_Mensagem == "OK")
            {
                // tudo certinho!
                CarregarFuncionarios();
                lblDS_Mensagem.Text = "Ativado com sucesso!";
            }
            else
            {
                // exibir erro!
                lblDS_Mensagem.Text = myControllerFuncionario.DS_Mensagem;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
            BloquearComponentesCadastro();
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Incluir();
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            Alterar();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        protected void btnAtivarStatus_Click(object sender, EventArgs e)
        {
            Ativar();
            btnAtivarStatus.Enabled = false;
        }

        protected void chkStatusInativo_CheckedChanged(object sender, EventArgs e)
        {
            CarregarFuncionarios();
        }

        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlFiltro.SelectedIndex.Equals(0))
            {
                txbConsultar.Enabled = true;
                txbConsultar.Focus();
            }
            else
            {
                txbConsultar.Text = "";
                txbConsultar.Enabled = false;
            }
        }

        protected void txbConsultar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbConsultar.Text.Trim()))
            {
                btnConsultar.Enabled = true;
                btnConsultar.Focus();
            }
            else
            {
                btnConsultar.Enabled = false;
                CarregarFuncionarios();
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            CarregarFuncionariosConsultar();
        }

        protected void gvwExibe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("lbSelecionar");
                e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackEventReference(lb, ""));

                e.Row.Cells[13].Visible = false;
                e.Row.Cells[15].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "#";
                e.Row.Cells[2].Text = "Nome";
                e.Row.Cells[3].Text = "CPF";
                e.Row.Cells[4].Text = "Nascimento";
                e.Row.Cells[5].Text = "Sexo";
                e.Row.Cells[6].Text = "Telefone";
                e.Row.Cells[7].Text = "Email";
                e.Row.Cells[8].Text = "CEP";
                e.Row.Cells[9].Text = "Logradouro";
                e.Row.Cells[10].Text = "Nº";
                e.Row.Cells[11].Text = "Bairro";
                e.Row.Cells[12].Text = "Complemento";
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Text = "Cidade";
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Text = "UF";
                e.Row.Cells[17].Text = "Cargo";
                e.Row.Cells[18].Text = "Salário";
                e.Row.Cells[19].Text = "Admissão";
            }
        }

        protected void gvwExibe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDS_Mensagem.Text = "";

            txbID_Funcionario.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[1].Text.Trim());
            txbNM_Funcionario.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[2].Text.Trim());
            txbNR_CPF.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[3].Text.Trim());

            try
            {
                string[] splitDataSemHoras = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[4].Text.Trim()).Split(' ');
                string data = splitDataSemHoras[0];
                string[] splitData = data.Split('/');

                ddlDiaNascimentoFuncionario.SelectedIndex = Convert.ToInt32(splitData[0]);
                ddlMesNascimentoFuncionario.SelectedIndex = Convert.ToInt32(splitData[1]);
                ddlAnoNascimentoFuncionario.SelectedValue = splitData[2];
            }
            catch (Exception)
            {
                lblDS_Mensagem.Text += " Erro ao exibir data de nascimento";
                ddlDiaNascimentoFuncionario.SelectedIndex = 0;
                ddlMesNascimentoFuncionario.SelectedIndex = 0;
                ddlAnoNascimentoFuncionario.SelectedIndex = 0;
            }

            ddlDS_Sexo.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[5].Text.Trim());
            txbNR_Telefone.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[6].Text.Trim());
            txbDS_Email.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[7].Text.Trim());
            txbNR_CEP.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[8].Text.Trim());
            txbDS_Logradouro.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[9].Text.Trim());
            txbNR_Casa.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[10].Text.Trim());
            txbNM_Bairro.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[11].Text.Trim());
            txbDS_Complemento.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[12].Text.Trim());

            try
            {
                ddlDS_UF.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[15].Text.Trim());
                CarregarCidades();
            }
            catch (Exception)
            {
                lblDS_Mensagem.Text += " Erro ao exibir a UF";
                ddlNM_Cidade.SelectedIndex = 0;
            }

            try
            {
                ddlNM_Cidade.SelectedValue = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[13].Text.Trim());
                ddlNM_Cidade.Enabled = true;
            }
            catch (Exception)
            {
                lblDS_Mensagem.Text += " Erro ao exibir a cidade";
                ddlNM_Cidade.SelectedIndex = 0;
            }

            ddlDS_Cargo.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[17].Text.Trim());
            txbVL_Salario.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[18].Text.Trim());
            txbDT_Admissao.Text = Server.HtmlDecode(gvwExibe.SelectedRow.Cells[19].Text.Trim());

            CheckBox ativo = (CheckBox)gvwExibe.SelectedRow.Cells[20].Controls[0];
            if (!ativo.Checked)
            {
                btnAtivarStatus.Enabled = true;
                btnExcluir.Enabled = false;
            }
            else
            {
                btnAtivarStatus.Enabled = false;
                btnExcluir.Enabled = true;
            }

            btnIncluir.Enabled = false;
            btnAlterar.Enabled = true;
            btnLimpar.Enabled = true;
        }

        protected void gvwExibe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwExibe.PageIndex = e.NewPageIndex;
            CarregarFuncionarios();
        }

        protected void txbNM_Funcionario_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbNR_CPF.Focus();
        }

        protected void txbNR_CPF_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlDiaNascimentoFuncionario.Focus();
        }

        protected void ddlDiaNascimentoFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlMesNascimentoFuncionario.Focus();
        }

        protected void ddlMesNascimentoFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlAnoNascimentoFuncionario.Focus();
        }

        protected void ddlAnoNascimentoFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlDS_Sexo.Focus();
        }

        protected void ddlDS_Sexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbNR_Telefone.Focus();
        }

        protected void txbNR_Telefone_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Email.Focus();
        }

        protected void txbDS_Email_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbNR_CEP.Focus();
        }

        protected void txbNR_CEP_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Logradouro.Focus();
        }

        protected void txbDS_Logradouro_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbNR_Casa.Focus();
        }

        protected void txbNR_Casa_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbNM_Bairro.Focus();
        }

        protected void txbNM_Bairro_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDS_Complemento.Focus();
        }

        protected void txbDS_Complemento_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlDS_Cargo.Focus();
        }

        protected void ddlDS_Cargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbVL_Salario.Focus();
        }

        protected void txbVL_Salario_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            txbDT_Admissao.Focus();
        }

        protected void txbDT_Admissao_TextChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlDS_UF.Focus();
        }

        protected void ddlDS_UF_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
            ddlNM_Cidade.Focus();

            if (ddlDS_UF.SelectedIndex != 0)
            {
                ddlNM_Cidade.Enabled = true;
                CarregarCidades();
            }
        }

        protected void ddlNM_Cidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            IncludeFields();
        }
    }
}