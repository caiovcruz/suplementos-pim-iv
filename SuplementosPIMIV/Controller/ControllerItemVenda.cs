using SuplementosPIMIV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuplementosPIMIV.Controller
{
    public class ControllerItemVenda : ModelItemVenda
    {
        public ControllerItemVenda(string connectionString) : base(connectionString) { }
        public ControllerItemVenda(int id_venda, int id_produto, int qtd_itemVenda, double vl_subtotal, char acao, string connectionString) : 
            base(id_venda, id_produto, qtd_itemVenda, vl_subtotal, acao, connectionString) { }
        public ControllerItemVenda(int id_venda, int id_produto, string connectionString) :
            base(id_venda, id_produto, connectionString)
        { }
    }
}