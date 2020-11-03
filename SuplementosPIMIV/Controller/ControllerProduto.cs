using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerProduto : ModelProduto
    {
        public ControllerProduto(string connectionString) : base(connectionString) { }

        public ControllerProduto(int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nr_ean, string nm_produto, 
            string ds_produto, int qtd_estoque, double pr_custo, double pr_venda, string connectionString) :
            base(id_marca, id_categoria, id_subcategoria, id_sabor, nr_ean, nm_produto, ds_produto, qtd_estoque, pr_custo, pr_venda, connectionString) { }

        public ControllerProduto(int id_produto, int id_marca, int id_categoria, int id_subcategoria, int id_sabor, string nr_ean, string nm_produto, 
            string ds_produto, int qtd_estoque, double pr_custo, double pr_venda, string connectionString) :
            base(id_produto, id_marca, id_categoria, id_subcategoria, id_sabor, nr_ean, nm_produto, ds_produto, qtd_estoque, pr_custo, pr_venda, connectionString) { }

        public ControllerProduto(int id_produto, string connectionString) : base(id_produto, connectionString) { }
    }
}