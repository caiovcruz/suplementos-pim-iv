﻿using SuplementosPIMIV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuplementosPIMIV.Controller
{
    public class ControllerVenda : ModelVenda
    {
        public ControllerVenda(string connectionString) : base(connectionString) { }
        public ControllerVenda(int id_funcionario, DateTime dt_venda, string connectionString) : base(id_funcionario, dt_venda, connectionString) { }

        public ControllerVenda(int id_venda, int id_funcionario, DateTime dt_venda, string ds_tipoPagamento, int nr_parcelas, double vl_total, string connectionString) : 
            base(id_venda, id_funcionario, dt_venda, ds_tipoPagamento, nr_parcelas, vl_total, connectionString) { }
        public ControllerVenda(int id_venda, string connectionString) : base(id_venda, connectionString) { }
    }
}