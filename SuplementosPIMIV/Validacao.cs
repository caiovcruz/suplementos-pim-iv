using System.Text.RegularExpressions;

namespace Validacao
{
    public class Validar
    {
        public Validar() { }

        public bool CampoPreenchido(string campo)
        {
            if (campo == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool TamanhoCampo(string campo, int tamanho)
        {
            if (campo.Length > tamanho)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Valor(string campo)
        {
            if (!Regex.IsMatch(campo, @"^[1-9]\d{0,2}(\.\d{3})*,\d{2}$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
