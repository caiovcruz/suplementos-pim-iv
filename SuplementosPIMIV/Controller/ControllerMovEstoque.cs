using SuplementosPIMIV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuplementosPIMIV.Controller
{
    public class ControllerMovEstoque : ModelMovEstoque
    {
        public ControllerMovEstoque(string connectionString) : base(connectionString) { }
        public ControllerMovEstoque(int id_movimentacaoEstoque, string connectionString) : base(id_movimentacaoEstoque, connectionString) { }
        public ControllerMovEstoque(int id_produto, int qtd_movimentacaoEstoque, string ds_movimentacaoEstoque, DateTime dt_movimentacaoEstoque,
            string connectionString) : base(id_produto, qtd_movimentacaoEstoque, ds_movimentacaoEstoque, dt_movimentacaoEstoque, connectionString) { }
    }
}