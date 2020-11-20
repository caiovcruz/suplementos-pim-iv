<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmFuncionario.aspx.cs" Inherits="SuplementosPIMIV.View.FrmFuncionario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Cadastro de Funcionários</title>

    <link href="~/Assets/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-32x32.png" sizes="32x32" />
    <link rel="icon" type="image/png" href="~/Assets/img/favicon-16x16.png" sizes="16x16" />
    <script src="https://kit.fontawesome.com/de6e3c9fed.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="formFuncionario" runat="server">
        <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>

        <!-- Menu -------------------------------------- -->
        <div class="navbar">
            <asp:LinkButton CssClass="dropbtn" ID="lbtnMeuLogin" Font-Underline="false" runat="server" PostBackUrl="~/View/FrmMeuLogin.aspx">
                <i class="fas fa-user-circle fa-lg" style="margin-right: 2px;"></i>
                <asp:Label ID="lblNM_FuncionarioLogin" runat="server"></asp:Label>
            </asp:LinkButton>
            <a href="FrmPDV.aspx"><i class="fas fa-store-alt" style="margin-right: 5px;"></i>PDV</a>
            <div class="dropdown">
                <asp:LinkButton CssClass="dropbtn" ID="lbtnDropProdutos" Font-Underline="false" runat="server">
                    <i class="fas fa-tablets" style="margin-right: 2px;"></i> Produtos <i class="fa fa-caret-down"></i></asp:LinkButton>
                <div class="dropdown-content">
                    <a href="FrmProduto.aspx">Produtos</a>
                    <a href="FrmMovEstoque.aspx">Mov.Estoques</a>
                    <a href="FrmMarca.aspx">Marcas</a>
                    <a href="FrmSabor.aspx">Sabores</a>
                    <a href="FrmSubcategoria.aspx">Subcategorias</a>
                    <a href="FrmCategoria.aspx">Categorias</a>
                </div>
            </div>
            <div class="dropdown">
                <asp:LinkButton CssClass="dropbtn" ID="lbtnDropFuncionarios" Font-Underline="false" runat="server">
                   <i class="fas fa-user-tie fa-sm" style="margin-right: 2px;"></i> Funcionários <i class="fa fa-caret-down"></i></asp:LinkButton>
                <div class="dropdown-content">
                    <a href="FrmFuncionario.aspx">Funcionários</a>
                    <a href="FrmCadastroLogin.aspx">Cadastro de Login</a>
                </div>
            </div>
        </div>

        <div class="conteiner">

            <div id="cadastroFuncionario">

                <div id="cadInternoFuncionario">

                    <asp:Label CssClass="Titulo" runat="server" Width="100%" Text="Cadastro de Funcionários"></asp:Label>

                    <br />

                    <asp:UpdatePanel ID="updCadastro" runat="server">
                        <ContentTemplate>

                            <asp:TextBox CssClass="TextBox" ID="txbID_Funcionario" Visible="false" runat="server"></asp:TextBox>

                            <div id="internoColunasFuncionario">

                                <div id="internoColunasFuncionario1">

                                    <div class="colunasFuncionario" id="linhaFuncionario1">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nome"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNM_Funcionario" runat="server" MaxLengh="50"
                                                placeholder="Nome do funcionário" OnTextChanged="txbNM_Funcionario_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario2">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="CPF"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_CPF" runat="server" MaxLengh="11"
                                                OnTextChanged="txbNR_CPF_TextChanged" placeholder="999.999.999-99" 
                                                AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender TargetControlID="txbNR_CPF" ID="mdeNR_CPF"
                                                Mask="999,999,999-99" MaskType="Number" ClearMaskOnLostFocus="true"
                                                runat="server" />
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%"
                                                Text="Data de Nascimento"></asp:Label>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlDiaNascimentoFuncionario"
                                                runat="server" OnSelectedIndexChanged="ddlDiaNascimentoFuncionario_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlMesNascimentoFuncionario"
                                                runat="server" OnSelectedIndexChanged="ddlMesNascimentoFuncionario_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlAnoNascimentoFuncionario"
                                                runat="server" OnSelectedIndexChanged="ddlAnoNascimentoFuncionario_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Sexo"></asp:Label>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlDS_Sexo" runat="server"
                                                OnSelectedIndexChanged="ddlDS_Sexo_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario3">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Telefone"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_Telefone" runat="server"
                                                MaxLengh="11" OnTextChanged="txbNR_Telefone_TextChanged" placeholder="(99)99999-9999" 
                                                AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender TargetControlID="txbNR_Telefone" ID="mdeNR_Telefone"
                                                Mask="(99)99999-9999" MaskType="Number" ClearMaskOnLostFocus="true"
                                                runat="server" />
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="E-mail"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbDS_Email" runat="server" MaxLengh="35"
                                                placeholder="exemplo.teste@outlook.com" OnTextChanged="txbDS_Email_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario4">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="CEP"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_CEP" runat="server" MaxLengh="8"
                                                OnTextChanged="txbNR_CEP_TextChanged" placeholder="99999-999" 
                                                AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender TargetControlID="txbNR_CEP" ID="mdeNR_CEP"
                                                Mask="99999-999" MaskType="Number" ClearMaskOnLostFocus="true" runat="server" />
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Logradouro"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbDS_Logradouro" runat="server" MaxLengh="50"
                                                OnTextChanged="txbDS_Logradouro_TextChanged" placeholder="Rua/Avenida/Alameda das flores" 
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nº"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_Casa" runat="server" MaxLengh="5"
                                                OnTextChanged="txbNR_Casa_TextChanged" placeholder="99999" 
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario5">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Bairro"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNM_Bairro" runat="server" MaxLengh="50"
                                                OnTextChanged="txbNM_Bairro_TextChanged" placeholder="Jardim/Conjunto/Vila/Parque Três Meninos" 
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Complemento"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbDS_Complemento" runat="server" MaxLengh="50"
                                                OnTextChanged="txbDS_Complemento_TextChanged" placeholder="QD/APTO/SN/CASA 2" 
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                </div>

                                <div class="coluna" id="internoColunasFuncionario2">

                                    <div id="colunaCargo">

                                        <asp:Label CssClass="Label" runat="server" Text="Cargo"></asp:Label>
                                        <asp:DropDownList CssClass="DropDownList" ID="ddlDS_Cargo" runat="server"
                                            OnSelectedIndexChanged="ddlDS_Cargo_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>

                                        <asp:Label CssClass="Label" runat="server" Text="Salário"></asp:Label>
                                        <asp:TextBox CssClass="TextBox" ID="txbVL_Salario" runat="server" MaxLengh="20"
                                            placeholder="999.999.999,99" OnTextChanged="txbVL_Salario_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>

                                        <asp:Label CssClass="Label" runat="server" Text="Data de Admissão"></asp:Label>
                                        <asp:TextBox CssClass="TextBox" ID="txbDT_Admissao" runat="server"
                                            OnTextChanged="txbDT_Admissao_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        <asp:Label CssClass="Label" runat="server" Text="UF"></asp:Label>
                                        <asp:DropDownList CssClass="DropDownList" ID="ddlDS_UF" runat="server"
                                            OnSelectedIndexChanged="ddlDS_UF_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <asp:Label CssClass="Label" runat="server" Text="Cidade"></asp:Label>
                                        <asp:DropDownList CssClass="DropDownList" ID="ddlNM_Cidade" runat="server"
                                            OnSelectedIndexChanged="ddlNM_Cidade_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimpar" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" OnClick="btnIncluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" OnClick="btnAlterar_Click" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />
                            <asp:Button CssClass="Button" ID="btnAtivarStatus" runat="server" Text="Ativar" OnClick="btnAtivarStatus_Click" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />
            </div>

            <br />
            <br />
            <br />

            <div id="exibeFuncionario">
                <div id="exibeInternoFuncionario">

                    <br />

                    <asp:UpdatePanel ID="updConsulta" runat="server">
                        <ContentTemplate>

                            <asp:CheckBox ID="chkStatusInativo" runat="server" Text="Inativos" AutoPostBack="true"
                                OnCheckedChanged="chkStatusInativo_CheckedChanged" />

                            <asp:DropDownList CssClass="TextBox" ID="ddlFiltro" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:TextBox CssClass="TextBox" ID="txbConsultar" MaxLengh="50"
                                placeholder="Buscar funcionário" runat="server" OnTextChanged="txbConsultar_TextChanged"
                                AutoPostBack="true"></asp:TextBox>

                            <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar"
                                OnClick="btnConsultar_Click" />

                            <br />

                            <div style="overflow: auto;">
                                <asp:GridView CssClass="gvwExibe" ID="gvwExibe" runat="server" CellPadding="5" GridLines="Horizontal"
                                    AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="gvwExibe_RowDataBound"
                                    OnSelectedIndexChanged="gvwExibe_SelectedIndexChanged" OnPageIndexChanging="gvwExibe_PageIndexChanging"
                                    AllowPaging="true" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbSelecionar" runat="server" CausesValidation="False"
                                                    CommandName="Select" Style="display: none;"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="WhiteSmoke" BorderStyle="Solid" BorderColor="Black" Font-Bold="True" />
                                </asp:GridView>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <br />
                <br />

            </div>

            <br />

        </div>
    </form>
</body>
</html>
