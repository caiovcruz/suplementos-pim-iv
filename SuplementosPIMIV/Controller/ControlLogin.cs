using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoDeVenda.Control
{
    public class ControlLogin : ModelLogin
    {
        public ControlLogin(string ds_usuario, string ds_senha) : base(ds_usuario, ds_senha) { }
    }
}