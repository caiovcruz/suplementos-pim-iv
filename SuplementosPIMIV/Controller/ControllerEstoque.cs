﻿using SuplementosPIMIV.Model;

namespace SuplementosPIMIV.Controller
{
    public class ControllerEstoque : ModelEstoque
    {
        public ControllerEstoque(string connectionString) : base(connectionString) { }
        public ControllerEstoque(int id_produto, int qtd_estoque, string connectionString) : base(id_produto, qtd_estoque, connectionString) { }
        public ControllerEstoque(int id_produto, string connectionString) : base(id_produto, connectionString) { }
    }
}