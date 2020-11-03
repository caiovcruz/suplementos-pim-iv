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

        public bool Numero(string campo)
        {
            if (!Regex.IsMatch(campo, @"^[\d]+$"))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool EAN(string campo)
        {
            bool result = (campo.Length == 13);

            if (result)
            {
                const string checkSum = "131313131313";

                int digito = int.Parse(campo[campo.Length - 1].ToString());

                string ean = campo.Substring(0, campo.Length - 1);

                int sum = 0;

                for (int i = 0; i <= ean.Length - 1; i++)
                {
                    sum += int.Parse(ean[i].ToString()) * int.Parse(checkSum[i].ToString());
                }

                int calculo = 10 - (sum % 10);

                result = (digito == calculo);
            }

            return result;
        }
    }
}
