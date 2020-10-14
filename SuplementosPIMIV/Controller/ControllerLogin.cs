using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoDeVenda.Control
{
    public class ControllerLogin : ModelLogin
    {
        public ControllerLogin(string ds_usuario, string ds_senha) : base(ds_usuario, ds_senha) { }
    }
}