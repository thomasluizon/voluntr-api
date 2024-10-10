namespace Voluntr.Crosscutting.Domain.Helpers.Extensions
{
    public static class CalculationExtension
    {
        public static int CalculateAge(this DateTime birthDate)
        {
            int age = DateTime.Now.ToBrazilianTimezone().Year - birthDate.Year;

            if (DateTime.Now.ToBrazilianTimezone().DayOfYear < birthDate.DayOfYear)
                age -= 1;

            return age;
        }
    }
}
