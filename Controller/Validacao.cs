using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using TrabalhoCantina.Models;

// fazer em analizes tecinas um mapGet que liste todas as garantias e as notas realcionadas ao cliente
// fazer em analizes tecinas um mapGet que liste todas os cliente pelo rg
// fazer em analizes tecinas um mapGet que liste todas os cliente pelo cpf do cliente
// fazer em analizes tecinas um mapGet que liste todas os cliente pelo Nome completo
// fazer em analizes tecinas um mapGet que liste todas os cliente pelo cep

namespace TrabalhoCantina.Controller
{

    public partial class Validaçao{

        public static bool ValidarCep(string cep){
            bool resp = false;
            string cepTratado = MyRegex().Replace(cep, "");

            if(cepTratado.Length == 8){
                resp = true;
            }
            return resp;
        }
        
        public static bool ValidarTelefone(string telefone){
            bool resp = false;
            string telefoneTratado = Regex.Replace(telefone, "[^0-9]", "");

            if(telefoneTratado.Length == 11){
                resp = true;
            }
            return resp;
        }
        
        public static bool ValidarEmail(string email){
            bool resp = false;
              string padrao = @"^[\w\.-]+@(outlook\.com|gmail\.com|hotmail\.com)$";

             if(Regex.IsMatch(email, padrao)){
                resp = true;
             }
        return resp;
        }               

        public static bool ValidarCpf(string cpf)
	    {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for(int i=0; i<9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if ( resto < 2 )
                resto = 0;
            else
            resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for(int i=0; i<10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
            resto = 0;
            else
            resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
	    }
        
        public static string PadronizarCpf(string Cpf){
            if (!Regex.IsMatch(Cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$")) {
				Cpf = $"{Cpf.Substring(0, 3)}.{Cpf.Substring(3, 3)}.{Cpf.Substring(6, 3)}-{Cpf.Substring(9, 2)}";
			}
            return Cpf;
        }
    
        [GeneratedRegex("[^0-9]")]
        private static partial Regex MyRegex();
    }
}