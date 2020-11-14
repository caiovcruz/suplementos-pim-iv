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
            <a href="FrmMenuPrincipal.aspx">Menu</a>
            <a href="">PDV</a>
            <div class="dropdown">
                <button class="dropbtn">
                    Produtos
                    <i class="fa fa-caret-down"></i>
                </button>
                <div class="dropdown-content">
                    <a href="FrmProduto.aspx">Produtos</a>
                    <a href="FrmMovEstoque.aspx">Mov.Estoques</a>
                    <a href="FrmMarca.aspx">Marcas</a>
                    <a href="FrmSabor.aspx">Sabores</a>
                    <a href="FrmSubcategoria.aspx">Subcategorias</a>
                    <a href="FrmCategoria.aspx">Categorias</a>
                </div>
            </div>
            <a href="FrmFuncionario.aspx">Funcionários</a>
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
                                            <asp:TextBox CssClass="TextBox" ID="txbNM_Funcionario" runat="server" MaxLengh="50" placeholder="Nome do funcionário" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario2">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="CPF"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_CPF" runat="server" MaxLengh="11" AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender TargetControlID="txbNR_CPF" ID="mdeNR_CPF" Mask="999,999,999-99" MaskType="Number" ClearMaskOnLostFocus="false" runat="server" />
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Data de Nascimento"></asp:Label>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlDiaNascimentoFuncionario" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlMesNascimentoFuncionario" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlAnoNascimentoFuncionario" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Sexo"></asp:Label>
                                            <asp:DropDownList CssClass="DropDownList" ID="ddlDS_Sexo" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario3">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Telefone"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_Telefone" runat="server" MaxLengh="11" AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender TargetControlID="txbNR_Telefone" ID="mdeNR_Telefone" Mask="(99) 99999-9999" MaskType="Number" ClearMaskOnLostFocus="false" runat="server" />
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="E-mail"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbDS_Email" runat="server" MaxLengh="35" placeholder="exemplo.teste@outlook.com" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario4">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="CEP"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_CEP" runat="server" MaxLengh="8" AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender TargetControlID="txbNR_CEP" ID="mdeNR_CEP" Mask="99999-999" MaskType="Number" ClearMaskOnLostFocus="false" runat="server" />
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Logradouro"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbDS_Logradouro" runat="server" MaxLengh="50" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Nº"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNR_Casa" runat="server" MaxLengh="5" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="colunasFuncionario" id="linhaFuncionario5">

                                        <div>
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Bairro"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbNM_Bairro" runat="server" MaxLengh="50" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                        <div class="coluna">
                                            <asp:Label CssClass="Label" runat="server" Width="100%" Text="Complemento"></asp:Label>
                                            <asp:TextBox CssClass="TextBox" ID="txbDS_Complemento" runat="server" MaxLengh="50" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>

                                </div>

                                <div class="coluna" id="internoColunasFuncionario2">

                                    <div id="colunaCargo">

                                        <asp:Label CssClass="Label" runat="server" Text="Cargo"></asp:Label>
                                        <asp:DropDownList CssClass="DropDownList" ID="ddlDS_Cargo" runat="server" AutoPostBack="true"></asp:DropDownList>

                                        <asp:Label CssClass="Label" runat="server" Text="Salário"></asp:Label>
                                        <asp:TextBox CssClass="TextBox" ID="txbVL_Salario" runat="server" MaxLengh="20" placeholder="999.999.999,99" AutoPostBack="true"></asp:TextBox>

                                        <asp:Label CssClass="Label" runat="server" Text="Data de Admissão"></asp:Label>
                                        <asp:TextBox CssClass="TextBox" ID="txbDT_Admissao" runat="server" AutoPostBack="true"></asp:TextBox>

                                        <asp:Label CssClass="Label" runat="server" Text="UF"></asp:Label>
                                        <asp:DropDownList CssClass="DropDownList" ID="ddlDS_UF" runat="server" AutoPostBack="true"></asp:DropDownList>

                                        <asp:Label CssClass="Label" runat="server" Text="Cidade"></asp:Label>
                                        <asp:DropDownList CssClass="DropDownList" ID="ddlNM_Cidade" runat="server" AutoPostBack="true"></asp:DropDownList>

                                    </div>

                                </div>

                            </div>

                            <br />

                            <asp:Label CssClass="Msg" ID="lblDS_Mensagem" runat="server" Text=""></asp:Label>

                            <br />
                            <br />

                            <asp:Button CssClass="Button" ID="btnLimpar" runat="server" Text="Limpar" />
                            <asp:Button CssClass="Button" ID="btnIncluir" runat="server" Text="Incluir" />
                            <asp:Button CssClass="Button" ID="btnAlterar" runat="server" Text="Alterar" />
                            <asp:Button CssClass="Button" ID="btnExcluir" runat="server" Text="Excluir" />
                            <asp:Button CssClass="Button" ID="btnAtivarStatus" runat="server" Text="Ativar" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />
            </div>

            <br />

            <div id="exibeFuncionario">
                <div id="exibeInternoFuncionario">

                    <br />

                    <asp:UpdatePanel ID="updConsulta" runat="server">
                        <ContentTemplate>

                            <asp:CheckBox ID="chkStatusInativo" runat="server" Text="Inativos" AutoPostBack="true" />

                            <asp:DropDownList CssClass="TextBox" ID="ddlFiltro" runat="server" AutoPostBack="true">
                            </asp:DropDownList>

                            <asp:TextBox CssClass="TextBox" ID="txbConsultar" MaxLengh="50"
                                placeholder="Buscar funcionário" runat="server"
                                AutoPostBack="true"></asp:TextBox>

                            <asp:Button CssClass="Button" ID="btnConsultar" runat="server" Text="Consultar" />

                            <br />

                            <asp:GridView CssClass="gvwExibe" ID="gvwExibe" runat="server" CellPadding="5" GridLines="Horizontal"
                                AlternatingRowStyle-BackColor="WhiteSmoke" AllowPaging="true" PageSize="10">
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
