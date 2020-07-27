using FluentValidation;
using FluentValidation.Validators;

namespace Solve.FinancialMarketIntegration.API.Areas.Tickets.Validators
{
    public class GenericValidator : PropertyValidator
    {
        public GenericValidator() : base("'{PropertyName}' is not a GenericValidator"){ }
        protected override bool IsValid(PropertyValidatorContext context){
            //var variavel = (type) context.PropertyValue;
            // TODO: Sua validação
            return false;
        }
    }

    public static class GenericValidatorExtension{
        public static IRuleBuilderOptions<T, string> GenericValidator<T>(this IRuleBuilder<T, string> rule){
            return rule.SetValidator(new GenericValidator());
        }
    }

    public class CPFValidator : PropertyValidator
    {
        public CPFValidator() : base("'{PropertyName}' is not a valid CPF"){ }
        protected override bool IsValid(PropertyValidatorContext context){
            var cpf = (string) context.PropertyValue;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }

    public static class CPFValidatorExtension{
        public static IRuleBuilderOptions<T, string> IsCPF<T>(this IRuleBuilder<T, string> rule){
            return rule.SetValidator(new CPFValidator());
        }
    }

    public class CNPJValidator : PropertyValidator
    {
        public CNPJValidator() : base("'{PropertyName}' is not a valid CNPJ"){ }
        protected override bool IsValid(PropertyValidatorContext context){
            var cnpj = (string) context.PropertyValue;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }

    public static class CNPJValidatorExtension{
        public static IRuleBuilderOptions<T, string> IsCNPJ<T>(this IRuleBuilder<T, string> rule){
            return rule.SetValidator(new CNPJValidator());
        }
    }
}