namespace Voluntr.Crosscutting.Domain.Helpers.Formatters
{
    public static class Formatter
    {
        public static string CNPJ(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string CPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        public static string PhoneNumber(string phoneNumber)
        {
            return Convert.ToInt64(phoneNumber).ToString(@"(00)0000-0000");
        }

        public static string MobileNumber(string mobileNumber)
        {
            return Convert.ToInt64(mobileNumber).ToString(@"(00)00000-0000");
        }

        public static string ZipCode(string zipCode)
        {
            var cleanedZip = string.Concat(zipCode.Where(char.IsDigit));

            if (cleanedZip.Length != 8)
            {
                return zipCode;
            }

            return cleanedZip.Insert(5, "-");
        }
    }
}
