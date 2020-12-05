using SuplementosPIMIV.Model;
using System;
using System.Data;
using Validacao;

namespace SuplementosPIMIV.Controller
{
    public class ControllerVenda
    {
        private ModelVenda myModelVenda;
        public int ID_Venda { get; set; }
        public string DS_Mensagem { get; set; }

        public ControllerVenda() { }

        public ControllerVenda(string id_funcionario, DateTime dt_venda, string ds_tipoPagamento, string nr_parcelas, string vl_total, string vl_lucro, string connectionString)
        {
            myModelVenda = new ModelVenda(Convert.ToInt32(id_funcionario), dt_venda, ds_tipoPagamento, Convert.ToInt32(nr_parcelas), Convert.ToDouble(vl_total),
                Convert.ToDouble(vl_lucro), connectionString);
            ID_Venda = myModelVenda.ID_Venda;
            DS_Mensagem = myModelVenda.DS_Mensagem;
        }

        public ControllerVenda(string id_venda, string id_funcionario, DateTime dt_venda, string ds_tipoPagamento, string nr_parcelas, string vl_total, string vl_lucro, string connectionString)
        {
            myModelVenda = new ModelVenda(Convert.ToInt32(id_venda), Convert.ToInt32(id_funcionario), dt_venda, ds_tipoPagamento, Convert.ToInt32(nr_parcelas), Convert.ToDouble(vl_total),
                Convert.ToDouble(vl_lucro), connectionString);
            DS_Mensagem = myModelVenda.DS_Mensagem;
        }

        public ControllerVenda(string id_venda, string connectionString)
        {
            myModelVenda = new ModelVenda(Convert.ToInt32(id_venda), connectionString);
            DS_Mensagem = myModelVenda.DS_Mensagem;
        }

        public DataTable Exibir(string connectionString)
        {
            myModelVenda = new ModelVenda();
            return myModelVenda.Exibir(connectionString);
        }

        public DataTable Consultar(DateTime dataInicio, DateTime dataFinal, string connectionString)
        {
            myModelVenda = new ModelVenda();
            return myModelVenda.Consultar(dataInicio, dataFinal, connectionString);
        }
    }
}