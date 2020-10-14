using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Validacao;

namespace PontoDeVenda
{
    public class ControlProduto : ModelProduto
    {
        public ControlProduto() { }

        public ControlProduto(int id_categoria, int id_subcategoria, int id_sabor, string nm_produto, string ds_produto, double qtd_estoque, double pr_custo, double pr_venda) :
            base(id_categoria, id_subcategoria, id_sabor, nm_produto, ds_produto, qtd_estoque, pr_custo, pr_venda) { }

        public ControlProduto(int id_produto, int id_categoria, int id_subcategoria, int id_sabor, string nm_produto, string ds_produto, double qtd_estoque, double pr_custo, double pr_venda) :
            base(id_produto, id_categoria, id_subcategoria, id_sabor, nm_produto, ds_produto, qtd_estoque, pr_custo, pr_venda) { }

        public ControlProduto(string nm_produto) : base(nm_produto) { }

        public ControlProduto(int id_produto) : base(id_produto) { }
    }
}