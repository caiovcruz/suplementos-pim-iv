using SuplementosPIMIV.Model;
using System;

namespace SuplementosPIMIV.Controller
{
    public class ControllerEstoque
    {
        private ModelEstoque myModelEstoque;
        public string DS_Mensagem { get; set; }

        public ControllerEstoque() { }

        public ControllerEstoque(string id_produto, string qtd_estoque, string connectionString)
        {
            myModelEstoque = new ModelEstoque(Convert.ToInt32(id_produto), Convert.ToInt32(qtd_estoque), connectionString);
            DS_Mensagem = myModelEstoque.DS_Mensagem;
        }

        public ControllerEstoque(string id_produto, string connectionString)
        {
            myModelEstoque = new ModelEstoque(Convert.ToInt32(id_produto), connectionString);
            DS_Mensagem = myModelEstoque.DS_Mensagem;
        }

        public int QuantidadeTotalEstoque(string id_produto, string connectionString)
        {
            myModelEstoque = new ModelEstoque();
            return myModelEstoque.QuantidadeTotalEstoque(Convert.ToInt32(id_produto), connectionString);
        }
    }
}