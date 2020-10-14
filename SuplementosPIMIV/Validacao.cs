using System.Text.RegularExpressions;

namespace Validacao
{
    public class Validar
    {
        public string DS_Mensagem { get; set; }

        public Validar(string nm_produto, string ds_produto, string qtd_estoque, string pr_venda, string pr_custo)
        {
            Produto(nm_produto, ds_produto, qtd_estoque, pr_venda, pr_custo);
        }

        public Validar(string nm_produto)
        {
            ConsultarProduto(nm_produto);
        }

        public bool CampoNulo(string campo)
        {
            if (campo == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool TamanhoCampo(string campo, int tamanho)
        {
            if (campo.Length > tamanho)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Valor(string campo)
        {
            if (!Regex.IsMatch(campo, @"^[1-9]\d{0,2}(\.\d{3})*,\d{2}$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Produto(string nm_produto, string ds_produto, string qtd_estoque, string pr_venda, string pr_custo)
        {
            DS_Mensagem = "";

            if (CampoNulo(nm_produto))
            {
                if (!TamanhoCampo(nm_produto, 50))
                {
                    DS_Mensagem = " Limite de caracteres para o nome excedido, " +
                                  "o limite para este campo é: 50 caracteres, " +
                                  "quantidade utilizada: " + nm_produto.Length + ".";
                }
            }
            else
            {
                DS_Mensagem = " O nome deve estar preenchido.";
            }


            if (CampoNulo(ds_produto))
            {
                if (!TamanhoCampo(ds_produto, 3000))
                {
                    DS_Mensagem += " Limite de caracteres para descrição excedido, " +
                                  "o limite para este campo é: 3000 caracteres, " +
                                  "quantidade utilizada: " + ds_produto.Length + ".";
                }
            }
            else
            {
                DS_Mensagem += " A descrição deve estar preenchida.";
            }


            if (CampoNulo(qtd_estoque))
            {
                if (!Valor(qtd_estoque))
                {
                    DS_Mensagem += " A quantidade em estoque deve ser um valor numérico, no formato: 9.999.999,99.";
                }
            }
            else
            {
                DS_Mensagem += " A quantidade em estoque deve estar preenchida.";
            }


            if (CampoNulo(pr_venda))
            {
                if (!Valor(pr_venda))
                {
                    DS_Mensagem += " O preço de venda deve ser um valor numérico, no formato: 9.999.999,99.";
                }
            }
            else
            {
                DS_Mensagem += " O preço de venda deve estar preenchido.";
            }


            if (CampoNulo(pr_custo))
            {
                if (!Valor(pr_custo))
                {
                    DS_Mensagem += " O preço de custo deve ser um valor numérico, no formato: 9.999.999,99.";
                }
            }
            else
            {
                DS_Mensagem += " O preço de custo deve estar preenchido.";
            }
        }

        private void ConsultarProduto(string nm_produto)
        {
            DS_Mensagem = "";

            if (!TamanhoCampo(nm_produto, 50))
            {
                DS_Mensagem = " Limite de caracteres para o nome excedido, " +
                              "o limite para este campo é: 50 caracteres, " +
                              "quantidade utilizada: " + nm_produto.Length + ".";
            }
        }
    }
}
