using Voluntr.Crosscutting.Domain.Commands;

namespace Voluntr.Domain.Commands
{
    public abstract class AccountCommand<TResponse> : CommandResponse<TResponse>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Phone { get; set; }
        public AddressCommand Address { get; set; }
        public VolunteerRegisterCommand VolunteerRegister { get; set; }
        public NgoRegisterCommand NgoRegister { get; set; }
        public CompanyRegisterCommand CompanyRegister { get; set; }
        public string NewPassword { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public string Token { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthProviderName { get; set; }
    }

    public class AddressCommand
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighbourhood { get; set; }
        public string Uf { get; set; }
        public string City { get; set; }
    }

    public class VolunteerRegisterCommand
    {
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class NgoRegisterCommand
    {
        public string Document { get; set; }
        public DateTime? FoundingDate { get; set; }
        public string Description { get; set; }
    }

    public class CompanyRegisterCommand
    {
    }
}
